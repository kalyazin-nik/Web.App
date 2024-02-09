using System;

namespace Entities
{
    public sealed class Identifier : BaseEntity
    {
        private DateTime? _inWorking;
        private DateTime? _complitedAt;

        public int IdentifierId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? InWorking { get { return _inWorking; } set { AssignValue(value, ref _inWorking, "В работе"); } }
        public DateTime? ComplitedAt { get { return _complitedAt; } set { AssignValue(value, ref _complitedAt, "Завершена"); } }
        public int StatusId { get; set; }
        public Status? Status { get; set; }

        public Identifier()
        {
            CreatedAt = DateTime.Now;
        }

        private void AssignValue(DateTime? value, ref DateTime? field, string statusName)
        {
            if (value != null && Status?.StatusName == statusName && field == null)
                field = (DateTime)value;
        }
    }
}
