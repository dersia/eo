using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EventStore.ClientAPI;
using SiaConsulting.EO.Abstractions;

namespace SiaConsulting.EO
{
    public class HandleCommands
    {
        public static IEnumerable<IEvent> Handle(ICommand command, ReadOnlyCollection<ResolvedEvent> eventStream, Microsoft.Extensions.Logging.ILogger log) 
        {
            var pumps = EoRegistry.GetCommandPumps(command);
            foreach(var pump in pumps) 
            {
                var commandPump = pump;
                var context = CommonUtils.LoadContext(command, commandPump.ContextLoader, eventStream, log);
                foreach(var result in Process(command, context, (ICommandProcessor)commandPump.Processor, log))
                {
                    yield return result;
                }
            }
        } 

        private static IEnumerable<IEvent> Process(ICommand command, IContext context, ICommandProcessor processor, Microsoft.Extensions.Logging.ILogger log) 
        {
            return processor.Process(command, context);
        }
    }
}