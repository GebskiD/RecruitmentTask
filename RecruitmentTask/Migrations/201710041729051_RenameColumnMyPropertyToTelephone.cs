namespace RecruitmentTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumnMyPropertyToTelephone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Telephone", c => c.String());
            DropColumn("dbo.Customers", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "MyProperty", c => c.String());
            DropColumn("dbo.Customers", "Telephone");
        }
    }
}
