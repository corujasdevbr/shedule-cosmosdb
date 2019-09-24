using Newtonsoft.Json;
using System.Collections.Generic;

namespace CorujasDev.Schedule.CosmosDb.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<ContactEntity> Contacts { get; set; }
        public List<TodoItemEntity> TodoItems { get; set; }

        public UserEntity()
        {
            Contacts = new List<ContactEntity>();
            TodoItems = new List<TodoItemEntity>();
        }

        public UserEntity(string Name, string Email, string Password)
        {
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
        }
    }
}
