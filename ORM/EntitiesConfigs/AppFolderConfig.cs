using ORM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.EntitiesConfigs
{
    public class AppFolderConfig : EntityTypeConfiguration<AppFolder>
    {
        public AppFolderConfig()
        {
            Property(f => f.Id)
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar");
            
            Property(f => f.DateUploaded)
                .IsRequired()
                .HasColumnType("datetime");


            HasMany(f => f.Files)
                .WithOptional(f => f.Folder)
                .WillCascadeOnDelete(true);

            HasMany(f => f.InternalFolders)
                .WithOptional(f => f.RootFolder)
                .WillCascadeOnDelete(false);
        }
    }
}
