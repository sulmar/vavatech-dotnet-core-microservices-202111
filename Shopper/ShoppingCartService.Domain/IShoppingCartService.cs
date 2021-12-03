using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartService.Domain
{
    public interface IShoppingCartService
    {
        Task Add(Guid shoppingCartId, Detail detail);
        Task Remove(Guid shoppingCartId, int productId);
    }
}
