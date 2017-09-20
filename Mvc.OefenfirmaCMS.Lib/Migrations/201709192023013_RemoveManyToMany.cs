namespace Mvc.OefenfirmaCMS.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoleUsers", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleUsers", "User_UserId", "dbo.Users");
            DropIndex("dbo.RoleUsers", new[] { "Role_RoleId" });
            DropIndex("dbo.RoleUsers", new[] { "User_UserId" });
            DropTable("dbo.RoleUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_RoleId = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleId, t.User_UserId });
            
            CreateIndex("dbo.RoleUsers", "User_UserId");
            CreateIndex("dbo.RoleUsers", "Role_RoleId");
            AddForeignKey("dbo.RoleUsers", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.RoleUsers", "Role_RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
        }
    }
}
