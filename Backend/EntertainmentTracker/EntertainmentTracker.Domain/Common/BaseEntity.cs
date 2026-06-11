using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentTracker.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.CreateVersion7();
        public DateTime CreatedAtUtc { get; protected set; } = DateTime.UtcNow;
        public DateTime UpdatedAtUtc { get; protected set; } = DateTime.UtcNow;
    }
}
