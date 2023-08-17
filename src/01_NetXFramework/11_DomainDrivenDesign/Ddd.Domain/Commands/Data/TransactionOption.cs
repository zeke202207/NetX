namespace NetX.Ddd.Domain
{
    public class TransactionOption
    {
        public Type EntityType { get; private set; }

        public Action<object> DataBaseInvoke { get; set; }

        public TransactionOption(Type entityType)
        {
            EntityType = entityType;
        }
    }
}
