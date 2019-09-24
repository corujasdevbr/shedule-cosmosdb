using System;

namespace CorujasDev.Schedule.CosmosDb.Domain.Entities
{
    public class ContactEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
