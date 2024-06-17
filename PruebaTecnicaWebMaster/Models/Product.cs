using System;
using System.Collections.Generic;

namespace PruebaTecnicaWebMaster.Models
{
    public partial class Product
    {
        public Product()
        {
            SalesProducts = new HashSet<SalesProduct>();
        }

        public int IdProducts { get; set; }
        public string NameProducts { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<SalesProduct> SalesProducts { get; set; }
    }
}
