namespace Ninesky.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 第一次初始化 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        Type = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.UserConfigs",
                c => new
                    {
                        ConfigID = c.Int(nullable: false, identity: true),
                        Enabled = c.Boolean(nullable: false),
                        ProhibitUserName = c.String(),
                        EnableAdminVerify = c.Boolean(nullable: false),
                        EnableEmailVerify = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ConfigID);
            
            CreateTable(
                "dbo.UserRoleRelations",
                c => new
                    {
                        RelationID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RelationID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 20),
                        DisplayName = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Status = c.Int(nullable: false),
                        RegistrationTime = c.DateTime(nullable: false),
                        LoginTime = c.DateTime(nullable: false),
                        LoginIP = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.UserRoleRelations");
            DropTable("dbo.UserConfigs");
            DropTable("dbo.Roles");
        }
    }
}
