namespace Mvc.OefenfirmaCMS.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveRolesOutSQL : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
        }
    }
}
