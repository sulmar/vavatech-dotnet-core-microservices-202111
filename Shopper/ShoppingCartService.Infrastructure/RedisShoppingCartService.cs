using ShoppingCartService.Domain;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace ShoppingCartService.Infrastructure
{
    // dotnet add package StackExchange.Redis
    public class RedisShoppingCartService : IShoppingCartService
    {
        private readonly IDatabase db;

        public RedisShoppingCartService(IConnectionMultiplexer connectionMultiplexer)
        {
            db = connectionMultiplexer.GetDatabase();
        }

        public async Task Add(Guid shoppingCartId, Detail detail)
        {
            // cart:{id}:{productId}

            RedisKey key = $"cart:{shoppingCartId}:{detail.ProductId}";

            // db.Execute("SAVE");

            if (await db.KeyExistsAsync(key))
            {
                await db.HashIncrementAsync(key, "Quantity", detail.Quantity);
            }
            else
            {
                HashEntry[] entries = new HashEntry[] 
                {
                    new ("Quantity", detail.Quantity.ToString()),
                    new ("UnitPrice", detail.UnitPrice.ToString()),
                };

                await db.HashSetAsync(key, entries);
            }
        }

        public async Task Remove(Guid shoppingCartId, int productId)
        {
            RedisKey key = $"cart:{shoppingCartId}:{productId}";

            var quantity = int.Parse(db.HashGet(key, "Quantity"));

            if (quantity==1)
            {
                await db.KeyDeleteAsync(key);
            }
            else
            {
                await db.HashIncrementAsync(key, "Quantity", - 1);
            }
        }
    }
}
