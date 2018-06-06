namespace INWKAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetIsExemptPropToItemType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemTypes", "IsExempt", c => c.Boolean(nullable: false));
            DropColumn("dbo.Items", "IsExempt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "IsExempt", c => c.Boolean(nullable: false));
            DropColumn("dbo.ItemTypes", "IsExempt");
        }
    }
}
