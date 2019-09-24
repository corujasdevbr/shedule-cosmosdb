using CorujasDev.Schedule.CosmosDb.Application.ViewModel.Base;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.Contact;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.TodoItem;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CorujasDev.Schedule.CosmosDb.Application.ViewModel.User
{
    public class UserViewModel : BaseViewModel
    {

        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public List<ContactViewModel> Contacts { get; set; }
        public List<TodoItemViewModel> TodoItems { get; set; }

        public UserViewModel()
        {
            Contacts = new List<ContactViewModel>();
            TodoItems = new List<TodoItemViewModel>();
        }

        public UserViewModel(string Name, string Email, string Password)
        {
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
        }
    }
}
