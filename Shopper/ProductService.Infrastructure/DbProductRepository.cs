using Core.Infrastructure;
using Domain;
using ProductService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Infrastructure
{
    public class DbProductRepository : DbEntityRepository<Product, ProductContext>, IProductRepository
    {
        public DbProductRepository(ProductContext context) : base(context)
        {
        }
    }


}
