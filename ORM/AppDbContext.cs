using ORM.EntitiesConfigs;
using ORM.Models;
using System;
using System.Data.Entity;

namespace ORM
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(new DbInitializer());
        }

        public AppDbContext()
            : base("name=AppDbContext")
        {

        }

        public virtual DbSet<AppFile> Files { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<AppFolder> Folders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          //  modelBuilder.Configurations.Add(new RoleConfig());
            modelBuilder.Configurations.Add(new UserConfig());
            modelBuilder.Configurations.Add(new AppFileConfig());
            modelBuilder.Configurations.Add(new AppFolderConfig());
            
 	        base.OnModelCreating(modelBuilder);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

    }
}
