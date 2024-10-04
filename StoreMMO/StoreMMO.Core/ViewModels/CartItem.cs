using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMMO.Core.ViewModels
{
    public class CartItem
    {
        public string productID { get; set; }
        public string proName { get; set; }
        [NotMapped]
        public string? quantity { get; set; }
        public string img { get; set; }
        public double price { get; set; }
        [NotMapped]
        public string? subtotal { get; set; }

    }
}
