using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_vs_
{
    public class Productes_In_Branch
    {
        [Key, ForeignKey("Branches")]
        [Column(Order = 1)]
        public int Branch_ID { get; set; }
        [Key, ForeignKey("Products")]
        [Column(Order = 2)]
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
        public virtual Branch Branches { get; set; }
        public virtual Product Products { get; set; }
    }
}
