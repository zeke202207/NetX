﻿using NetX.Ddd.Domain;

namespace NetX.TaskScheduling.Domain.Commands
{
    public record EnabledJobCommand : DomainCommand
    {
        public string Id { get; set; }

        public bool Enabled { get; set; }

        public EnabledJobCommand(string id, bool enabled)
        {
            Id = id;
            Enabled = enabled;
        }
    }
}
