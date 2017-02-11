using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EmployeesRegister.Models.Database
{
    public class EmployeeRegisterDb : DbContext
    {
        public EmployeeRegisterDb() : base("PWDatabase")
        {
            //System.Data.Entity.Database.SetInitializer<PWDatabase>(null);
            System.Data.Entity.Database.SetInitializer<EmployeeRegisterDb>(new MigrateDatabaseToLatestVersion<EmployeeRegisterDb, Configuration>());
            this.Configuration.AutoDetectChangesEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<ProgLanguage> ProgLanguages { get; set; }

        public DbSet<Skills> Skills { get; set; }

        public DbSet<User> Users { get; set; }
    }
}