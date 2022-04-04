using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_vs_
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
       [ForeignKey("Categories")]
        public int Category_Id { get; set; }
        public virtual Category Categories { get; set; }
        public virtual List<Productes_In_Branch> Productes_In_Branches { get; set; }
        public virtual List<Suppliers_B_Product> Suppliers_B_Products { get; set; }
        public virtual List<Customer_B_Product> Customer_B_Products { get; set; }

    }
}
