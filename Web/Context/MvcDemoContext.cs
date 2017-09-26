using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web.Models;
using Web.Models.Access;
using Web.Models.Entities;

namespace Web.Context
{
    //https://github.com/bilal-fazlani/tracker-enabled-dbcontext/wiki
    public class MvcDemoContext : DbContext

    {
        public MvcDemoContext()
            : base("MvcDemoConn")
        {
            Database.SetInitializer<MvcDemoContext>(null);
            Configuration.LazyLoadingEnabled = false;
        }

        public static MvcDemoContext Create()
        {
            return new MvcDemoContext();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Functionality> Functionalities { get; set; }
        public DbSet<FunctionalityPermission> FunctionalityPermissions { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<UserGroupPermission> UserGroupPermissions { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public override Task<int> SaveChangesAsync()
        {
            SaveLog();

            return base.SaveChangesAsync();
        }

        public override int SaveChanges()
        {
            SaveLog();

            return base.SaveChanges();
        }

        private void SaveLog()
        {
            var now = DateTime.UtcNow;

            var entitiesToLog = ChangeTracker.Entries<IAuditedEntity>()
                 .Where(p => p.State == EntityState.Modified)
                 .ToList();

            var currentObjectValues = new List<string>();
            var originalObjectValues = new List<object>();

            foreach (var entity in entitiesToLog)
            {
                foreach (var prop in entity.OriginalValues.PropertyNames)
                {
                    var propOriginalValue = entity.OriginalValues[prop].ToString();
                    var propCurrentValue = entity.CurrentValues[prop].ToString();
                    if (propOriginalValue == propCurrentValue) continue;
                    currentObjectValues.Add(propCurrentValue);
                    originalObjectValues.Add(propOriginalValue);
                }

                if (currentObjectValues.Count <= 0 || originalObjectValues.Count <= 0) continue;

                var entityName = entity.Entity.GetType().Name;
                var primaryKey = GetPrimaryKeyValue(entity) as string;
                var currentObjectValue = JsonConvert.SerializeObject(currentObjectValues);
                var originalObjectValue = JsonConvert.SerializeObject(originalObjectValues);

                var log = new ChangeLog
                {
                    Date = now,
                    CurrentValue = currentObjectValue,
                    OriginalValue = originalObjectValue,
                    Entity = entityName,
                    PrimaryKey = primaryKey
                };

                ChangeLogs.Add(log);
            }
        }

        private object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }
    }
}