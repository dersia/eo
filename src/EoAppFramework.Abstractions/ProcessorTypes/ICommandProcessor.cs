using System.Collections.Generic;

namespace SiaConsulting.EO.Abstractions
{
    public interface ICommandProcessor : IProcessor
    {
         IList<IEvent> Process<TCommand, TContext>(TCommand command, TContext context) where TCommand : ICommand where TContext : IContext;
    }
}