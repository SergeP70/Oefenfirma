namespace Mvc.OefenfirmaCMS.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersChangeAddressName : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Users", "RelAdress", "UserAddress");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.Users", "UserAddress", "RelAdress");

        }
    }
}
