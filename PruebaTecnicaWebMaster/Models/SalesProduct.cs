using System;
using System.Collections.Generic;

namespace PruebaTecnicaWebMaster.Models
{
    public partial class SalesProduct
    {
        public int IdSp { get; set; }
        public int SalesId { get; set; }
        public int ProductsId { get; set; }

        public int? Quantity { get; set; }

        public virtual Product Products { get; set; } = null!;
        public virtual Sale Sales { get; set; } = null!;
    }
}
