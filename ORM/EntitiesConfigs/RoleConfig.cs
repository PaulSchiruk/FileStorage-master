//using ORM.Models;
//using System;
//using System.Data.Entity;
//using System.Data.Entity.ModelConfiguration;

//namespace ORM.EntitiesConfigs
//{
//    public class RoleConfig : EntityTypeConfiguration<Role>
//    {
//        public RoleConfig()
//        {
//            HasMany(r => r.Users)
//                .WithRequired(r => r.Role)
//                .WillCascadeOnDelete(false);

//            Property(r => r.Name)
//                .IsRequired()
//                .HasMaxLength(50)
//                .HasColumnType("nvarchar");

//            Property(r => r.Description)
//                .IsOptional()
//                .HasMaxLength(200)
//                .HasColumnType("nvarchar");
//        }
//    }
//}
