namespace NetX.SharedFramework.ChainPipeline
{
    public class ChainPipelineExcpetion : Exception
    {
        public ChainPipelineExcpetion(Exception ex)
            : base(ex.Message, ex)
        {

        }
    }
}
