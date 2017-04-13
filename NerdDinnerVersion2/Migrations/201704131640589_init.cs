namespace NerdDinnerVersion2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dinners",
                c => new
                    {
                        DinnerId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        EventDate = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 30),
                        HostedBy = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DinnerId);
            
            CreateTable(
                "dbo.RSVPs",
                c => new
                    {
                        RsvpId = c.Int(nullable: false, identity: true),
                        DinnerId = c.Int(nullable: false),
                        AttendeeEmail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RsvpId)
                .ForeignKey("dbo.Dinners", t => t.DinnerId, cascadeDelete: true)
                .Index(t => t.DinnerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RSVPs", "DinnerId", "dbo.Dinners");
            DropIndex("dbo.RSVPs", new[] { "DinnerId" });
            DropTable("dbo.RSVPs");
            DropTable("dbo.Dinners");
        }
    }
}
