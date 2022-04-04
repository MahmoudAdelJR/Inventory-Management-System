using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_vs_
{
    public class Customers_Bill
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("Customers")]

        public int Customer_ID { get; set; }
        public DateTime Date { get; set; }
        public virtual List<Customer_B_Product> Customer_B_Products { get; set; }
        public virtual Customer Customers { get; set; }

    }
}
