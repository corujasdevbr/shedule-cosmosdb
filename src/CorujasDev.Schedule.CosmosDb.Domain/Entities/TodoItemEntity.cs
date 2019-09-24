namespace CorujasDev.Schedule.CosmosDb.Domain.Entities
{
    public class TodoItemEntity : BaseEntity
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
