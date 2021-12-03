using System;
using System.Collections.Generic;

namespace ShoppingCartService.Domain
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public IEnumerable<Detail> Details { get; set; }
    }

    public class Detail
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }


}
