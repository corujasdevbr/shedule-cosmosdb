using System;

namespace CorujasDev.Schedule.CosmosDb.Application.ViewModel.Base
{
    public class BaseViewModel
    {
        public BaseViewModel()
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
