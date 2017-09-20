using Mvc.OefenfirmaCMS.Lib.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.OefenfirmaCMS.Lib.Data
{
    public class OefenfirmaDropCreateAndSeed :
        MigrateDatabaseToLatestVersion<OefenfirmaContext, Migrations.Configuration>
    {
    }



    // De oorspronkelijke seed methode: ik laat ze staan ter referentie
    //public class OefenfirmaDropCreateAndSeed : DropCreateDatabaseAlways<OefenfirmaContext>
    //{
    //    protected override void Seed(OefenfirmaContext context)
    //    {
    //        //Opvullen categorieën

    //        List<Category> categorieën = new List<Category>
    //        {
    //            new Category { CatName = "Componenten", ParentCat=0 },
    //            new Category { CatName = "Laptop & PC", ParentCat=0 },
    //            new Category { CatName = "Netwerk", ParentCat=0 },
    //            new Category { CatName = "Printen/Scannen", ParentCat=0 },
    //            new Category { CatName = "Randapparatuur", ParentCat=0 },
    //            new Category { CatName = "Servers", ParentCat=0 },
    //            new Category { CatName = "Software", ParentCat=0 },
    //            new Category { CatName = "Behuizingen", ParentCat=1 },
    //            new Category { CatName = "DVD/Blu Ray", ParentCat=1 },
    //            new Category { CatName = "Geheugen", ParentCat=1 },
    //            new Category { CatName = "Geluidskaarten", ParentCat=1 },
    //            new Category { CatName = "Harde schijven", ParentCat=1 },
    //            new Category { CatName = "Moederborden", ParentCat=1 },
    //            new Category { CatName = "Processoren", ParentCat=1 },
    //            new Category { CatName = "Video & TV-kaarten", ParentCat=1 },
    //            new Category { CatName = "Voedingen", ParentCat=1 },
    //            new Category { CatName = "Tablets", ParentCat=2 },
    //            new Category { CatName = "Apple", ParentCat=2 },
    //            new Category { CatName = "Laptops", ParentCat=2 },
    //            new Category { CatName = "Desktop PC’s", ParentCat=2 },
    //            new Category { CatName = "Routers", ParentCat=3 },
    //            new Category { CatName = "Switchen", ParentCat=3 },
    //            new Category { CatName = "Netwerkkaarten", ParentCat=3 },
    //            new Category { CatName = "Inkjet printers", ParentCat=4 },
    //            new Category { CatName = "Laserprinters", ParentCat=4 },
    //            new Category { CatName = "Scanners", ParentCat=4 },
    //            new Category { CatName = "Externe dataopslag", ParentCat=5 },
    //            new Category { CatName = "Monitoren / Touchscreens", ParentCat=5 },
    //            new Category { CatName = "Speakers / hoofdtelefoons", ParentCat=5 },
    //            new Category { CatName = "Bedieningsapparatuur", ParentCat=5 },
    //            new Category { CatName = "Hewlet Packard", ParentCat=6 },
    //            new Category { CatName = "Supermicro", ParentCat=6 },
    //            new Category { CatName = "Netwerk opslag (NAS)", ParentCat=6 },
    //            new Category { CatName = "Adobe", ParentCat=7 },
    //            new Category { CatName = "Microsoft", ParentCat=7 },
    //            new Category { CatName = "Antivirus", ParentCat=7 }
    //        };

    //        categorieën.ForEach(c => context.Categories.Add(c));

    //        // Opvullen Users
    //        List<User> users = new List<User>
    //        {
    //            new User
    //            {
    //                UserName= "Serge",
    //                UserPassword= "pasw123",
    //                UserEmail="serge.pille@telenet.be"
    //            },
    //            new User
    //            {
    //                UserName= "Siegfried",
    //                UserPassword= "pasw123",
    //                UserEmail="Siegfried@telenet.be"
    //            }
    //        };
    //        users.ForEach(u => context.Users.Add(u));

    //        // Opvullen Relationships
    //        List<Relationship> relationships = new List<Relationship>
    //        {
    //            new Relationship
    //            {
    //                RelationshipId="K",
    //                Description="Klant"
    //            },
    //            new Relationship
    //            {
    //                RelationshipId="L",
    //                Description="Leverancier"
    //            }
    //        };
    //        relationships.ForEach(s => context.Relationships.Add(s));

    //        // Opvullen Relations
    //        List<Relation> relations = new List<Relation>
    //        {
    //            new Relation
    //            {
    //                RelName="Pille",
    //                RelFirstName="Serge",
    //                RelEmail="serge.pille@telenet.be",
    //                RelationshipId="K"
    //            },
    //            new Relation
    //            {
    //                RelName="Derdeyn",
    //                RelFirstName="Siegfried",
    //                RelEmail="siegfried.derdeyn@telenet.be",
    //                RelationshipId="L"
    //            },
    //            new Relation
    //            {
    //                RelName="Peeters",
    //                RelFirstName="Peter",
    //                RelEmail="peter.peeters@telenet.be",
    //                RelationshipId="K"
    //            },
    //            new Relation
    //            {
    //                RelName="Janssens",
    //                RelFirstName="Jan",
    //                RelEmail="jan.janssens@telenet.be",
    //                RelationshipId="K"
    //            },
    //            new Relation
    //            {
    //                RelName="Wouters",
    //                RelFirstName="Koen",
    //                RelEmail="koen.wouters@telenet.be",
    //                RelationshipId="L"
    //            }
    //        };
    //        relations.ForEach(r => context.Relations.Add(r));


    //        //// Opvullen van Producten rechtstreeks vanuit de excel-file
    //        string path = @"D:\prodlist.xlsx";
    //        var excelData = new ExcelData(path);
    //        var artikelen = excelData.getData("Productenlijst");

    //        // Vul artikellijst met gegevens uit de excel-file
    //        List<Product> artikellijst = excelData.fillList(artikelen);
    //        artikellijst.ForEach(a => context.Products.Add(a));

    //        context.SaveChanges();
    //    }
    //}
}
