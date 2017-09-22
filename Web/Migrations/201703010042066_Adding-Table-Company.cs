namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTableCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Company", "Email", c => c.String());
            AddColumn("dbo.Company", "Address", c => c.String());
            AddColumn("dbo.Company", "Phone", c => c.String());
            AlterColumn("dbo.Company", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Company", "Name", c => c.Int(nullable: false));
            DropColumn("dbo.Company", "Phone");
            DropColumn("dbo.Company", "Address");
            DropColumn("dbo.Company", "Email");
        }
    }
}
