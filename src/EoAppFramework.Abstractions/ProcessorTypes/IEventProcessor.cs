using System.Collections.Generic;

namespace SiaConsulting.EO.Abstractions
{
    public interface IEventProcessor : IProcessor
    {
         IList<TResult> Process<TResult, TEvent, TContext>(TEvent @event, TContext context) where TEvent : IEvent where TContext : IContext;
    }
}