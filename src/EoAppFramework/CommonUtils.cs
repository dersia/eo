using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using SiaConsulting.EO.Abstractions;

namespace SiaConsulting.EO
{
    internal static class CommonUtils
    {
        internal static IEnumerable<IEvent> ConvertStream(IList<ResolvedEvent> stream) 
        {
            foreach(var streamData in stream) 
            {
                if(streamData.Event?.EventType == null)
                {
                    continue;
                }
                var @eventType = Type.GetType(streamData.Event.EventType);
                var @event = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(streamData.Event.Data), @eventType);
                if(@event is IEvent convertedEvent) 
                {
                    yield return convertedEvent;
                }
            }
        }

        internal static IContext LoadContext<TMessage>(TMessage message, ContextLoaderBase contextLoader, ReadOnlyCollection<ResolvedEvent> stream, Microsoft.Extensions.Logging.ILogger log) where TMessage : IMessage
        {
            List<IEvent> internalStream = ConvertStream(stream).ToList();
            var context = contextLoader.Render(message, internalStream.AsReadOnly());
            return context;
        }
    }
}