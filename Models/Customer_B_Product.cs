using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_vs_
{
    public class Customer_B_Product
    {
        [Key, ForeignKey("Products")]
        [Column(Order = 1)]
        public int Product_ID { get; set; }
        [Key, ForeignKey("Costomer_Bills")]
        [Column(Order = 2)]
        public int Costomer_Bill_ID { get; set; }
        public int Quantity { get; set; }
        public virtual Product Products { get; set; }
        public virtual Customers_Bill Costomer_Bills { get; set; }

    }
}
