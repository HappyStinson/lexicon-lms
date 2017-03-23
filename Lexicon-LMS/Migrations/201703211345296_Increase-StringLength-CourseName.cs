namespace Lexicon_LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IncreaseStringLengthCourseName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
