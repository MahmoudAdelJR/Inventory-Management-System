using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_vs_
{
    public class Admin_Bill
    {
        [Key,ForeignKey("Admin")]
        [Column(Order=1)]
        public int Admin_ID { get; set; }
        [Key, ForeignKey("Bill")]
        [Column(Order = 1)]
        public int Bill_ID { get; set; }

        public virtual Admin Admins { get; set; }
        public virtual Bill Bills { get; set; }
    }
}
