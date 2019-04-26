using SiaConsulting.EO.Abstractions;

namespace SiaConsulting.EO
{
    public class Pump {}
    public class Pump<T> : Pump where T : IProcessor
    {
        public ContextLoaderBase ContextLoader { get; }
        public T Processor { get; }
        public Pump(ContextLoaderBase contextLoader, T processor) 
        {
            ContextLoader = contextLoader;
            Processor = processor;
        }

    }
}