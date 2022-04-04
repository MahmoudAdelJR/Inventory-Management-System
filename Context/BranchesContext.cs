using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_vs_
{
    public class BranchesContext : DbContext
    {
        //public BranchesContext() : base("Data Source=.;Initial Catalog=FinalC#;Integrated Security=True")
        public BranchesContext() : base("Data Source=.;Initial Catalog=Branches_Data;Integrated Security=True")
        {

        }
        public virtual DbSet<Branch> Branches { set; get; }
        public virtual DbSet<Category> Categories { set; get; }
        public virtual DbSet<Product> Products { set; get; }
        public virtual DbSet<Supplier> Suppliers { set; get; }
        public virtual DbSet<Customer> Customers { set; get; }
        public virtual DbSet<Productes_In_Branch> Productes_In_Branches { set; get; }
        public virtual DbSet<Suppliers_Bill> Suppliers_Bills { set; get; }
        public virtual DbSet<Suppliers_B_Product> Suppliers_B_Products { set; get; }
        public virtual DbSet<Customers_Bill> Customers_Bills { set; get; }
        public virtual DbSet<Customer_B_Product> Customer_B_Products { set; get; }
    }
}
