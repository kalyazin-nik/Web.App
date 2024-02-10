using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using Database;
using System;

namespace DataFromDatabase
{
    public sealed class GeneralData : BaseDataEntity
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string StatusName { get; }
        public DateTime CreatedAt { get; }
        public DateTime? InWorking { get; }
        public DateTime? ComplitedAt { get; }

        public GeneralData
            (int id, string name, string description, string statusName, DateTime createdAt, DateTime? inWorking, DateTime? complitedAt)
        {
            Id = id;
            Name = name;
            Description = description;
            StatusName = statusName;
            CreatedAt = createdAt;
            InWorking = inWorking;
            ComplitedAt = complitedAt;
        }

        public static List<GeneralData> GetData(DataContext context)
        {
            return context.Tasks
                .Join(context.Statuses, t => t.StatusId, s => s.StatusId,
                    (t, s) => new { t.Id, t.Name, t.Description, s.StatusId, s.StatusName })
                .Join(context.Identifiers, s => s.StatusId, i => i.StatusId,
                    (s, i) => new GeneralData(s.Id, s.Name, s.Description, s.StatusName, i.CreatedAt, i.InWorking, i.ComplitedAt))
                .ToList();
        }
    }
}
