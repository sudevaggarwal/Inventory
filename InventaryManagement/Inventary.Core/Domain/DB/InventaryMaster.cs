using System;
using System.Collections.Generic;
using System.Text;

namespace Inventary.Core.Domain.DB
{
    public class InventaryMaster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PricePerUnit { get; set; }
        public int? Quantity { get; set; }
        public int? TotalPrice { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
