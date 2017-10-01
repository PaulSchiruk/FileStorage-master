namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppFiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        ContentType = c.String(nullable: false, maxLength: 100, unicode: false),
                        DateUploaded = c.DateTime(nullable: false),
                        Content = c.Binary(nullable: false, maxLength: 8000),
                        Folder_Id = c.Int(),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppFolders", t => t.Folder_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Folder_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AppFolders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        DateUploaded = c.DateTime(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppFolders", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AppFiles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AppFiles", "Folder_Id", "dbo.AppFolders");
            DropIndex("dbo.AppFolders", new[] { "User_Id" });
            DropIndex("dbo.AppFiles", new[] { "User_Id" });
            DropIndex("dbo.AppFiles", new[] { "Folder_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.AppFolders");
            DropTable("dbo.AppFiles");
        }
    }
}
