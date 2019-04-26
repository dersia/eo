using System.Collections.Generic;
using System.Collections.ObjectModel;
using EventStore.ClientAPI;
using SiaConsulting.EO.Abstractions;

namespace SiaConsulting.EO
{
    public class HandleQuerys
    {
        public static IEnumerable<T> Handle<T>(IQuery query, ReadOnlyCollection<ResolvedEvent> eventStream, Microsoft.Extensions.Logging.ILogger log) 
        {
            var pumps = EoRegistry.GetQueryPumps(query);
            foreach(var pump in pumps) 
            {
                var queryPump = (Pump<IQueryProcessor>)pump;
                var context = CommonUtils.LoadContext(query, queryPump.ContextLoader, eventStream, log);
                foreach(var result in Process<T>(query, context, queryPump.Processor, log))
                {
                    yield return result;
                }
            }
        } 

        private static IEnumerable<T> Process<T>(IQuery query, IContext context, IQueryProcessor processor, Microsoft.Extensions.Logging.ILogger log) 
        {
            return processor.Process<T, IQuery, IContext>(query, context);
        }
    }
}