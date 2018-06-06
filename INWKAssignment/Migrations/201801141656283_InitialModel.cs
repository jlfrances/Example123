namespace INWKAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemTypeId = c.Byte(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsExempt = c.Boolean(nullable: false),
                        Job_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ItemTypes", t => t.ItemTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Jobs", t => t.Job_Id)
                .Index(t => t.ItemTypeId)
                .Index(t => t.Job_Id);
            
            CreateTable(
                "dbo.ItemTypes",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HasExtraMargin = c.Boolean(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "Job_Id", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Items", "ItemTypeId", "dbo.ItemTypes");
            DropIndex("dbo.Jobs", new[] { "Customer_Id" });
            DropIndex("dbo.Items", new[] { "Job_Id" });
            DropIndex("dbo.Items", new[] { "ItemTypeId" });
            DropTable("dbo.Jobs");
            DropTable("dbo.ItemTypes");
            DropTable("dbo.Items");
            DropTable("dbo.Customers");
        }
    }
}
