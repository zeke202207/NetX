namespace NetX.SharedFramework.ChainPipeline
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ChainPipelineAttribute : Attribute
    {
        public int Order { get; set; }

        public ChainPipelineAttribute(int order)
        {
            Order = order;
        }
    }
}
