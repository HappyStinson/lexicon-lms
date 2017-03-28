namespace Lexicon_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activity1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 500),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ActivityTypeId = c.Int(nullable: false),
                        ModuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modules", t => t.ModuleId, cascadeDelete: true)
                .ForeignKey("dbo.ActivityTypes", t => t.ActivityTypeId, cascadeDelete: true)
                .Index(t => t.ActivityTypeId)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.ActivityTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "ActivityTypeId", "dbo.ActivityTypes");
            DropForeignKey("dbo.Activities", "ModuleId", "dbo.Modules");
            DropIndex("dbo.Activities", new[] { "ModuleId" });
            DropIndex("dbo.Activities", new[] { "ActivityTypeId" });
            DropTable("dbo.ActivityTypes");
            DropTable("dbo.Activities");
        }
    }
}
