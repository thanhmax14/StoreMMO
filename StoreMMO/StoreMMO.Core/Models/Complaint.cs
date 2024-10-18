using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.Models
{
    public class Complaint
    {
        [Key]
        public string ID { get; set; }
        public string OrderDetailID { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Reply { get; set; }
        public string? Status { get; set; }
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
