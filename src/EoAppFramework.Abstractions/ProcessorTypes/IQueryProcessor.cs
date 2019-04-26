using System.Collections.Generic;

namespace SiaConsulting.EO.Abstractions
{
    public interface IQueryProcessor : IProcessor
    {
        IList<TResult> Process<TResult, TQuery, TContext>(TQuery query, TContext context) where TQuery: IQuery where TContext: IContext;
    }
}