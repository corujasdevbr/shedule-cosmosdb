using System;

namespace CorujasDev.Schedule.CosmosDb.Domain.Entities
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            if (string.IsNullOrEmpty(this.id))
                this.id = Guid.NewGuid().ToString();

            if (this.CreatedDate == DateTime.MinValue)
                this.CreatedDate = DateTime.Now;
        }

        public string id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
