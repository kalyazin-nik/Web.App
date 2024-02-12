using System;

namespace App.Models
{
    public class TaskTableModel
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string StatusName { get; set; } = null!;
    }
}
