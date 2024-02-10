namespace Entities
{
    public sealed class Task : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int StatusId { get; set; }
        public Status? Status { get; set; }

        public Task(string name, string description) 
        {
            Name = name;
            Description = description;
        }

        public Task() { }
    }
}
