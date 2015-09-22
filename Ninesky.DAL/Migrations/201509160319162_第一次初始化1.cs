namespace Ninesky.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 第一次初始化1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "LoginIP", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "LoginIP", c => c.DateTime(nullable: false));
        }
    }
}
