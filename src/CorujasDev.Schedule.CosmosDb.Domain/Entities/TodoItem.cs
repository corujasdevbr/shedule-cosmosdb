namespace CorujasDev.Schedule.CosmosDb.Domain.Entities
{
    public class TodoItem : BaseEntity
    {
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
