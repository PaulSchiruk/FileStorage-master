using ORM.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace ORM.EntitiesConfigs
{
    public class UserConfig: EntityTypeConfiguration<User>
    {
        public UserConfig()
        {
            Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar");

            Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar");

            HasMany(u => u.Files)
                .WithRequired(f => f.User)
                .WillCascadeOnDelete(false);

            HasMany(u => u.Folders)
                .WithRequired(f => f.User)
                .WillCascadeOnDelete(false);
        }
    }
}
