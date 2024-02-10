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
        public DateTime CreatedAt { get; set; }
        public DateTime? InWorking { get; set; }
        public DateTime? ComplitedAt { get; set; }

        //public TaskTableModel
        //    (int id, int statusId, string name, string description, string statusName, DateTime createdAt, DateTime? inWorking, DateTime? complitedAt)
        //{
        //    Id = id;
        //    StatusId = statusId;
        //    Name = name;
        //    Description = description;
        //    StatusName = statusName;
        //    CreatedAt = createdAt;
        //    InWorking = inWorking;
        //    ComplitedAt = complitedAt;
        //}

        //public TaskTableModel() { }
    }
}
