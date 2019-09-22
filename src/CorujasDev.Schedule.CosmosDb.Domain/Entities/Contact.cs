using System;

namespace CorujasDev.Schedule.CosmosDb.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Telephone { get; set; }
    }
}
