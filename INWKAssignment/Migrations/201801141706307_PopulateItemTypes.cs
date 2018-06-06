namespace INWKAssignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateItemTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO ItemTypes (Id, Name, IsExempt) VALUES (1, 'Envelope', 'FALSE')");
            Sql("INSERT INTO ItemTypes (Id, Name, IsExempt) VALUES (2, 'Letterhead', 'TRUE')");
            Sql("INSERT INTO ItemTypes (Id, Name, IsExempt) VALUES (3, 'T-Shirt', 'FALSE')");
            Sql("INSERT INTO ItemTypes (Id, Name, IsExempt) VALUES (4, 'Frisbee', 'TRUE')");
            Sql("INSERT INTO ItemTypes (Id, Name, IsExempt) VALUES (5, 'Yo-yo', 'TRUE')");

        }
        
        public override void Down()
        {
        }
    }
}
