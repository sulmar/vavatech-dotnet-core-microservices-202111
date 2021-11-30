using Core.Domain;
using System;

namespace ProductService.Domain
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string BarCode { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
