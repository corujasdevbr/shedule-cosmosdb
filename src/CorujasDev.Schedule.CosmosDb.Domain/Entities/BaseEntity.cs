using System;

namespace CorujasDev.Schedule.CosmosDb.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid ID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
