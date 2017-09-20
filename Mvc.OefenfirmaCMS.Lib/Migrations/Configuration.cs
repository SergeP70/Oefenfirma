namespace Mvc.OefenfirmaCMS.Lib.Migrations
{
    using Mvc.OefenfirmaCMS.Lib.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Mvc.OefenfirmaCMS.Lib.Data.OefenfirmaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Mvc.OefenfirmaCMS.Lib.Data.OefenfirmaContext";
        }

        protected override void Seed(Mvc.OefenfirmaCMS.Lib.Data.OefenfirmaContext context)
        {
            var roles = new List<Role>
            {
                new Role {RoleId=1, RoleName="Administrator" },
                new Role {RoleId=2, RoleName="Klant" },
                new Role {RoleId=3, RoleName="Leverancier" }
            };
            roles.ForEach(r => context.Roles.AddOrUpdate(r));
            context.SaveChanges();
        }




        //  This method will be called after migrating to the latest version.

        //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //  to avoid creating duplicate seed data. E.g.
        //
        //    context.People.AddOrUpdate(
        //      p => p.FullName,
        //      new Person { FullName = "Andrew Peters" },
        //      new Person { FullName = "Brice Lambson" },
        //      new Person { FullName = "Rowan Miller" }
        //    );
        //
    }
}

