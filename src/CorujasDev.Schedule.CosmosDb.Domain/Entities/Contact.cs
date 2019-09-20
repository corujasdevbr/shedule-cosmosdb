using System;

namespace CorujasDev.Schedule.CosmosDb.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public Contact()
        {
            if (this.ID == Guid.Empty)
                this.ID = Guid.NewGuid();

            if (this.CreatedDate == DateTime.MinValue)
                this.CreatedDate = DateTime.Now;

        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Telephone { get; set; }
    }
}
