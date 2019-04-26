using System.Collections.ObjectModel;

namespace SiaConsulting.EO.Abstractions
{
    public interface IContext
    {
         ReadOnlyCollection<IEvent> Stream { get; }
    }
}