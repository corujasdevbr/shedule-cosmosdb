using CorujasDev.Schedule.CosmosDb.Domain.Entities;
using CorujasDev.Schedule.CosmosDb.Domain.Interfaces.Repositories;
using CorujasDev.Schedule.CosmosDb.Infra.Data.Contexts;

namespace CorujasDev.Schedule.CosmosDb.Infra.Data.Repositories
{
    public class TodoItemRepository : RepositoryBase<TodoItem>, IRepositoryTodoItem
    {
        public TodoItemRepository(ScheduleContext context) : base(context) { }
    }
}
