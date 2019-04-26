using System.Collections.ObjectModel;
using SiaConsulting.EO.Abstractions;

namespace SiaConsulting.EO
{
    public abstract class ContextLoaderBase
    {
        public abstract IContext Render<TMessage>(TMessage message, ReadOnlyCollection<IEvent> stream) where TMessage : IMessage;
    }
}