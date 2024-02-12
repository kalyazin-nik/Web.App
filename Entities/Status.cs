using System.Collections.Generic;
using System.Text;
using System;

namespace Entities
{
    public sealed class Status : BaseEntity
    {
        private static readonly HashSet<string> _statuses = new HashSet<string>() { "Создана", "В работе", "Завершена" };
        private string _statusName = null!;

        public int StatusId { get; set; }
        public string StatusName { get { return _statusName; } set { AssignValue(value); } }

        public Status(string statusName)
        {
            StatusName = statusName;
        }

        public Status() { }

        private void AssignValue(string value)
        {
            if (_statuses.Contains(value)) _statusName = value;
            else throw new ArgumentException(GetMessageArgumentException());
        }

        private static string GetMessageArgumentException()
        {
            var message = new StringBuilder();
            foreach (var status in _statuses) message.Append(status + ", ");
            message.Remove(message.Length - 2, 2);
            return $"Статус может содержать в себе одно из имён, такие как: {message}";
        }
    }
}
