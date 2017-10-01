namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AppFiles", "Size", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AppFiles", "Size");
        }
    }
}
