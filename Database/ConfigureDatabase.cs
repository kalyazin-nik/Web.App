using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Database
{
    internal static class ConfigureDatabase
    {
        internal static string Schema { get { return _schema; } }
        private readonly static string _schema = "scheduler";
        private readonly static Dictionary<string, string> _tableNames = new Dictionary<string, string>()
        {
            { "Task", "tasks" },
            { "Status", "statuses" },
            { "Identifier", "identifiers" }
        };
        private readonly static Dictionary<string, EntityInfo> _entityInfo = new Dictionary<string, EntityInfo>()
        {
            { "Id", new EntityInfo("Id", "id") },
            { "Name", new EntityInfo ("Name", "name", "varchar(30)") },
            { "Description", new EntityInfo("Description", "description", "text") },
            { "StatusId", new EntityInfo("StatusId", "status_id") },
            { "StatusName", new EntityInfo("StatusName", "status_name", "varchar(9)", "('Создана')") },
            { "IdentifierId", new EntityInfo("IdentifierId", "identifier_id") },
            { "CreatedAt", new EntityInfo("CreatedAt", "created_at", "timestamp", "CURRENT_TIMESTAMP") },
            { "InWorking", new EntityInfo("InWorking", "in_working", "timestamp") },
            { "ComplitedAt", new EntityInfo("ComplitedAt", "complited_at", "timestamp") }
        };
        private readonly static Dictionary<string, Constraint> _constraints = new Dictionary<string, Constraint>()
        {
            { "Status", new Constraint("status_name_check", "status_name IN ( 'Создана', 'В работе', 'Завершена' )") }
        };

        internal static string GetTableName<TEntity>() where TEntity : BaseEntity
        {
            return _tableNames[typeof(TEntity).Name];
        }

        internal static IEnumerable<EntityInfo> GetEntityInfo<TEntity>() where TEntity : BaseEntity
        {
            foreach (var property in typeof(TEntity).GetProperties())
                if (_entityInfo.TryGetValue(property.Name, out EntityInfo value))
                    yield return value;
        }

        internal static IEnumerable<Constraint> GetConstraint<TEntity>()
        {
            var entityName = typeof(TEntity).Name;
            if (_constraints.ContainsKey(entityName))
                foreach (var constraint in _constraints.Select(k => k).Where(k => k.Key == entityName))
                    yield return constraint.Value;
        }
    }
}
