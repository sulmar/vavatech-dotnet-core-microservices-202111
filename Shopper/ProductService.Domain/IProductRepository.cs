﻿using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain
{
    public interface IProductRepository : IEntityRepository<Product>
    {     
        
    }
}
