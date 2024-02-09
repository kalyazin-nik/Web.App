using Microsoft.EntityFrameworkCore;
using Entities;

namespace Database
{
    public class DataContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; } = null!;
        public DbSet<Status> Statuses { get; set; } = null!;
        public DbSet<Identifier> Identifiers { get; set; } = null!;

        public DataContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ChangeEntityForDatabase<Task>(modelBuilder);
            ChangeEntityForDatabase<Status>(modelBuilder);
            ChangeEntityForDatabase<Identifier>(modelBuilder);
        }

        private static void ChangeEntityForDatabase<TEntity>(ModelBuilder modelBuilder) where TEntity : BaseEntity
        {
            modelBuilder.Entity<TEntity>().ToTable(ConfigureDatabase.GetTableName<TEntity>(), schema: ConfigureDatabase.Schema);

            foreach (var entityInfo in ConfigureDatabase.GetEntityInfo<TEntity>())
            {
                modelBuilder.Entity<TEntity>().Property(entityInfo.PropertyName).HasColumnName(entityInfo.ColumnName);

                if (entityInfo.ColumnType != null)
                    modelBuilder.Entity<TEntity>().Property(entityInfo.PropertyName).HasColumnType(entityInfo.ColumnType);

                if (entityInfo.ColumnDefaultValue != null)
                    modelBuilder.Entity<TEntity>().Property(entityInfo.PropertyName).HasDefaultValueSql(entityInfo.ColumnDefaultValue);
            }

            foreach (var constraint in ConfigureDatabase.GetConstraint<TEntity>())
                modelBuilder.Entity<TEntity>().HasCheckConstraint(constraint.Name, constraint.Check);
        }
    }
}

