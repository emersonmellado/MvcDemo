namespace Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChangeLog",
                c => new
                    {
                        ChangeLogId = c.Int(nullable: false, identity: true),
                        RequestId = c.Guid(nullable: false),
                        PrimaryKey = c.String(),
                        Entity = c.String(),
                        OriginalValue = c.String(),
                        CurrentValue = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChangeLogId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Functionality",
                c => new
                    {
                        FunctionalityId = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 5),
                        Name = c.String(nullable: false, maxLength: 50),
                        Url = c.String(nullable: false, maxLength: 50),
                        Order = c.Int(nullable: false),
                        ModuleId = c.Long(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FunctionalityId)
                .ForeignKey("dbo.Module", t => t.ModuleId)
                .Index(t => t.Code, unique: true, name: "IX_CodeFunctionalityUnique")
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        ModuleId = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 5),
                        Name = c.String(nullable: false, maxLength: 50),
                        Icon = c.String(maxLength: 250),
                        Order = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ModuleId);
            
            CreateTable(
                "dbo.FunctionalityPermission",
                c => new
                    {
                        FunctionalityPermissionId = c.Long(nullable: false, identity: true),
                        FunctionalityId = c.Long(nullable: false),
                        PermissionGroupId = c.Long(nullable: false),
                        Read = c.Boolean(nullable: false),
                        Write = c.Boolean(nullable: false),
                        Edit = c.Boolean(nullable: false),
                        Delete = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FunctionalityPermissionId)
                .ForeignKey("dbo.Functionality", t => t.FunctionalityId)
                .ForeignKey("dbo.PermissionGroup", t => t.PermissionGroupId)
                .Index(t => t.FunctionalityId)
                .Index(t => t.PermissionGroupId);
            
            CreateTable(
                "dbo.PermissionGroup",
                c => new
                    {
                        PermissionGroupId = c.Long(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 5),
                        Name = c.String(nullable: false, maxLength: 50),
                        ProfileTypeId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PermissionGroupId)
                .Index(t => t.Code, unique: true, name: "IX_CodePermissionGroupUnique");
            
            CreateTable(
                "dbo.UserGroupPermission",
                c => new
                    {
                        UserGroupPermissionId = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        PermissionGroupId = c.Long(nullable: false),
                        Active = c.Boolean(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.UserGroupPermissionId)
                .ForeignKey("dbo.PermissionGroup", t => t.PermissionGroupId)
                .ForeignKey("dbo.User", t => t.User_UserId)
                .Index(t => t.PermissionGroupId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Login = c.String(),
                        ChangePassword = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Company", t => t.CompanyId)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.UserType",
                c => new
                    {
                        UserTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserTypeId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserType", "UserId", "dbo.User");
            DropForeignKey("dbo.UserGroupPermission", "User_UserId", "dbo.User");
            DropForeignKey("dbo.User", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.UserGroupPermission", "PermissionGroupId", "dbo.PermissionGroup");
            DropForeignKey("dbo.FunctionalityPermission", "PermissionGroupId", "dbo.PermissionGroup");
            DropForeignKey("dbo.FunctionalityPermission", "FunctionalityId", "dbo.Functionality");
            DropForeignKey("dbo.Functionality", "ModuleId", "dbo.Module");
            DropIndex("dbo.UserType", new[] { "UserId" });
            DropIndex("dbo.User", new[] { "CompanyId" });
            DropIndex("dbo.UserGroupPermission", new[] { "User_UserId" });
            DropIndex("dbo.UserGroupPermission", new[] { "PermissionGroupId" });
            DropIndex("dbo.PermissionGroup", "IX_CodePermissionGroupUnique");
            DropIndex("dbo.FunctionalityPermission", new[] { "PermissionGroupId" });
            DropIndex("dbo.FunctionalityPermission", new[] { "FunctionalityId" });
            DropIndex("dbo.Functionality", new[] { "ModuleId" });
            DropIndex("dbo.Functionality", "IX_CodeFunctionalityUnique");
            DropTable("dbo.UserType");
            DropTable("dbo.User");
            DropTable("dbo.UserGroupPermission");
            DropTable("dbo.PermissionGroup");
            DropTable("dbo.FunctionalityPermission");
            DropTable("dbo.Module");
            DropTable("dbo.Functionality");
            DropTable("dbo.Company");
            DropTable("dbo.ChangeLog");
        }
    }
}
