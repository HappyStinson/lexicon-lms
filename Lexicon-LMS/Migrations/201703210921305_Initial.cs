namespace Lexicon_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Courses", "Description", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Description", c => c.String());
            AlterColumn("dbo.Courses", "Name", c => c.String());
        }
    }
}
