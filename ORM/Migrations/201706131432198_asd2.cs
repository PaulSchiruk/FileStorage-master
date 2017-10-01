namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppFolders", "RootFolder_Id", c => c.Int());
            CreateIndex("dbo.AppFolders", "RootFolder_Id");
            AddForeignKey("dbo.AppFolders", "RootFolder_Id", "dbo.AppFolders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppFolders", "RootFolder_Id", "dbo.AppFolders");
            DropIndex("dbo.AppFolders", new[] { "RootFolder_Id" });
            DropColumn("dbo.AppFolders", "RootFolder_Id");
        }
    }
}
