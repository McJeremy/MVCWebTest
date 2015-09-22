using Ninesky.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ninesky.DAL
{
    public class NineskyDbContext:DbContext
    {
        public NineskyDbContext():base("DefaultConnection")
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRoleRelation> UserRoleRelations
        {
            get;
            set;
        }
        public DbSet<UserConfig> UserConfigs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()

            .Where(type => !String.IsNullOrEmpty(type.Namespace))

            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            //modelBuilder
            //   .Configurations
            //   .Add(new UserTypeConfiguration())
            //   .Add(new UserRoleTypeConfiguration())
            //   .Add(new RoleTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
