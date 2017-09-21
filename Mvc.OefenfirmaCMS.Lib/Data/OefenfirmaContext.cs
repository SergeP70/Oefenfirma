using Mvc.OefenfirmaCMS.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.OefenfirmaCMS.Lib.Data
{
    public class OefenfirmaContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<Role> Roles { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)  => Fluent laat ik vallen voorlopig
        //{
        //    // Configure domain classes using modelbuider:
        //    //modelBuilder.Entity<Relation>()
        //    //    .HasOptional(r => r.User) // Mark User property optional in Relation entity
        //    //    .WithRequired(u => u.Relation); // mark Relation property as required in User entity. Cannot save User without Relation


        //    base.OnModelCreating(modelBuilder);
        //}
    }
    }
