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
    public class AppFileConfig : EntityTypeConfiguration<AppFile>
    {
        public AppFileConfig()
        {
            
            Property(f => f.Id)
                .IsRequired()
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar");

            Property(f => f.ContentType)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar");
    
            Property(f => f.Content)
                .IsRequired()
                .HasColumnType("varbinary");

            Property(f => f.Size)
                .IsRequired();

            Property(f => f.DateUploaded)
                .IsRequired()
                .HasColumnType("datetime");

        }
    }
}
