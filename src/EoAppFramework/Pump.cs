using SiaConsulting.EO.Abstractions;

namespace SiaConsulting.EO
{
    public class Pump
    {
        public ContextLoaderBase ContextLoader { get; }
        public IProcessor Processor { get; }
        public Pump(ContextLoaderBase contextLoader, IProcessor processor) 
        {
            ContextLoader = contextLoader;
            Processor = processor;
        }

    }
}