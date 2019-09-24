using CorujasDev.Schedule.CosmosDb.Application.ViewModel.Base;
using Newtonsoft.Json;
using System;

namespace CorujasDev.Schedule.CosmosDb.Application.ViewModel.Contact
{
    public class ContactViewModel : BaseViewModel
    {
        [JsonRequired]
        public string FirstName { get; set; }
        [JsonRequired]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        [JsonRequired]
        public string Telephone { get; set; }
        [JsonRequired]
        public string Email { get; set; }
    }
}
