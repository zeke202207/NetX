using NetX.Ddd.Domain;

namespace NetX.TaskScheduling.Domain
{
    public record StateJobCommand : DomainCommand
    {
        public string Id { get; set; }

        public int State { get; set; }

        public StateJobCommand(string id, int state)
        {
            Id = id;
            State = state;
        }
    }
}
