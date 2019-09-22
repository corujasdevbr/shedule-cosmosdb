using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;

namespace CorujasDev.Schedule.CosmosDb.Infra.Data.Repositories
{
    public class TodoItemRepository : RepositoryBaseDocumentClient<TodoItem>, IRepositoryTodoItem
    {
        public TodoItemRepository() {
        }
    }
}
