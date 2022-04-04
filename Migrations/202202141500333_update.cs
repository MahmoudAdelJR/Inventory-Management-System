namespace Project_vs_.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Productes_In_Branch",
                c => new
                    {
                        Branch_ID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Branch_ID, t.Product_ID })
                .ForeignKey("dbo.Branches", t => t.Branch_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Branch_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Category_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customer_B_Product",
                c => new
                    {
                        Product_ID = c.Int(nullable: false),
                        Costomer_Bill_ID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ID, t.Costomer_Bill_ID })
                .ForeignKey("dbo.Customers_Bill", t => t.Costomer_Bill_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Product_ID)
                .Index(t => t.Costomer_Bill_ID);
            
            CreateTable(
                "dbo.Customers_Bill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Customer_ID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.Customer_ID, cascadeDelete: true)
                .Index(t => t.Customer_ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Suppliers_B_Product",
                c => new
                    {
                        Product_ID = c.Int(nullable: false),
                        Supplier_Bill_ID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_ID, t.Supplier_Bill_ID })
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers_Bill", t => t.Supplier_Bill_ID, cascadeDelete: true)
                .Index(t => t.Product_ID)
                .Index(t => t.Supplier_Bill_ID);
            
            CreateTable(
                "dbo.Suppliers_Bill",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Supplier_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Suppliers", t => t.Supplier_ID, cascadeDelete: true)
                .Index(t => t.Supplier_ID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productes_In_Branch", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Suppliers_B_Product", "Supplier_Bill_ID", "dbo.Suppliers_Bill");
            DropForeignKey("dbo.Suppliers_Bill", "Supplier_ID", "dbo.Suppliers");
            DropForeignKey("dbo.Suppliers_B_Product", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Customer_B_Product", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.Customer_B_Product", "Costomer_Bill_ID", "dbo.Customers_Bill");
            DropForeignKey("dbo.Customers_Bill", "Customer_ID", "dbo.Customers");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Productes_In_Branch", "Branch_ID", "dbo.Branches");
            DropIndex("dbo.Suppliers_Bill", new[] { "Supplier_ID" });
            DropIndex("dbo.Suppliers_B_Product", new[] { "Supplier_Bill_ID" });
            DropIndex("dbo.Suppliers_B_Product", new[] { "Product_ID" });
            DropIndex("dbo.Customers_Bill", new[] { "Customer_ID" });
            DropIndex("dbo.Customer_B_Product", new[] { "Costomer_Bill_ID" });
            DropIndex("dbo.Customer_B_Product", new[] { "Product_ID" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.Productes_In_Branch", new[] { "Product_ID" });
            DropIndex("dbo.Productes_In_Branch", new[] { "Branch_ID" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Suppliers_Bill");
            DropTable("dbo.Suppliers_B_Product");
            DropTable("dbo.Customers");
            DropTable("dbo.Customers_Bill");
            DropTable("dbo.Customer_B_Product");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Productes_In_Branch");
            DropTable("dbo.Branches");
        }
    }
}
