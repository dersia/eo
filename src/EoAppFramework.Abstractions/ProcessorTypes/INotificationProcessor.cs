using System.Collections.Generic;

namespace SiaConsulting.EO.Abstractions
{
    public interface INotificationProcessor : IProcessor
    {
         IList<ICommand> Process<TNotification, TContext>(TNotification notification, TContext context) where TNotification : INotification where TContext : IContext;
    }
}