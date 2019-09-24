using CorujasDev.Schedule.CosmosDb.Application.ViewModel.Base;

namespace CorujasDev.Schedule.CosmosDb.Application.ViewModel.TodoItem
{
    public class TodoItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
