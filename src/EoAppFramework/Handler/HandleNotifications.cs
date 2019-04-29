using System.Collections.Generic;
using System.Collections.ObjectModel;
using EventStore.ClientAPI;
using SiaConsulting.EO.Abstractions;

namespace SiaConsulting.EO
{
    public class HandleNotifications
    {
        public static IEnumerable<ICommand> Handle(INotification notification, ReadOnlyCollection<ResolvedEvent> eventStream, Microsoft.Extensions.Logging.ILogger log) 
        {
            var pumps = EoRegistry.GetNotificationPumps(notification);
            foreach(var pump in pumps) 
            {
                var notificationPump = pump;
                var context = CommonUtils.LoadContext(notification, notificationPump.ContextLoader, eventStream, log);
                foreach(var result in Process(notification, context, (INotificationProcessor)notificationPump.Processor, log))
                {
                    yield return result;
                }
            }
        } 

        private static IEnumerable<ICommand> Process(INotification notification, IContext context, INotificationProcessor processor, Microsoft.Extensions.Logging.ILogger log) 
        {
            return processor.Process(notification, context);
        }
    }
}