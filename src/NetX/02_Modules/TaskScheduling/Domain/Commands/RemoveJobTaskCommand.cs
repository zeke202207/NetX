using NetX.Ddd.Domain;

namespace NetX.TaskScheduling.Domain
{
    public record RemoveJobTaskCommand : DomainCommand
    {
        public string Id { get; set; }

        public RemoveJobTaskCommand(string id)
            : base(Guid.NewGuid(), DateTime.Now)
        {
            Id = id;
        }
    }
}
