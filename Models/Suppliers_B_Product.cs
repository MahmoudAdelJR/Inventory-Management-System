using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_vs_
{
    public class Suppliers_B_Product
    {
        [Key, ForeignKey("Products")]
        [Column(Order = 1)]
        public int Product_ID { get; set; }
        [Key, ForeignKey("Supplier_Bills")]
        [Column(Order = 2)]
        public int Supplier_Bill_ID { get; set; }
        public int Quantity { get; set; }
        public virtual Product Products { get; set; }
        public virtual Suppliers_Bill Supplier_Bills { get; set; }

    }
}
