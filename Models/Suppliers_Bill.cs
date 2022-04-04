using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_vs_
{
    public class Suppliers_Bill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
  
        public DateTime Date { get; set; }
        [ForeignKey("Suppliers")]
        public int Supplier_ID { get; set; }
        public virtual List<Suppliers_B_Product> Suppliers_B_Products { get; set; }
        public virtual Supplier Suppliers { get; set; }

    }
}
