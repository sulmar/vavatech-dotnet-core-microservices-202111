using Microsoft.AspNetCore.Mvc;
using ShoppingCartService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartService.Api.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class ShoppingCartController : ControllerBase
    {
        // POST /api/cart/{sessionId}
        // Content-Type: application/json
        // body:
        // { "productId": 1111, "quantity": 1 }

        // DELETE /api/cart/{sessionId}/products/{productId}

        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        [HttpPost("{sessionId}")]
        public async Task<ActionResult> Post(Guid sessionId, [FromBody] Detail detail)
        {
            await shoppingCartService.Add(sessionId, detail);

            return Ok();
        }

        [HttpDelete("{sessionId}/{productId}")]
        public async Task<ActionResult> Post(Guid sessionId, int productId)
        {
            await shoppingCartService.Remove(sessionId, productId);

            return Ok();
        }

    }
}
