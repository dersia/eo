using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EventStore.ClientAPI;
using SiaConsulting.EO.Abstractions;

namespace SiaConsulting.EO
{
    public class HandleEvents
    {
        public static IEnumerable<T> Handle<T>(IEvent @event, ReadOnlyCollection<ResolvedEvent> eventStream, Microsoft.Extensions.Logging.ILogger log) 
        {
            var pumps = EoRegistry.GetEventPumps(@event);
            foreach(var pump in pumps) 
            {
                var eventPump = (Pump<IEventProcessor>)pump;
                var context = CommonUtils.LoadContext(@event, eventPump.ContextLoader, eventStream, log);
                foreach(var result in Process<T>(@event, context, eventPump.Processor, log))
                {
                    yield return result;
                }
            }
        } 

        private static IEnumerable<T> Process<T>(IEvent @event, IContext context, IEventProcessor processor, Microsoft.Extensions.Logging.ILogger log) 
        {
            return processor.Process<T, IEvent, IContext>(@event, context);
        }
    }
}