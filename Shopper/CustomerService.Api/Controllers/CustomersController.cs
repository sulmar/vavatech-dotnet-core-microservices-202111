using CustomerService.Api.Commands;
using CustomerService.Api.Notifications;
using CustomerService.Api.Queries;
using CustomerService.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Api.Controllers
{
    // REST API
    // Swagger / OpenApi 

    // GET /api/customers?fields=firstname,lastname
    // GET /api/customers/10/orders
    // GET /api/orders?customerId = 10 - zła praktyka

    // GET - pobranie 200 OK, 404 Not Found
    // POST - utwórz 201 Created + Location: ... + entity
    // PUT - zamień  204 NoContent
    // PATCH - zmień (częściowo) json+patch 204 NoContent
    // DELETE - usuń - 200 OK
    // HEAD - sprawdź czy istnieje - 200 OK, 404 Not Found
    // OPTIONS - powiedz mi co potrafisz (jakie operacje wspiera danych zasób) - 200 OK + GET, POST

    // OData  = url w sql'u ;-)

    // GraphQL
    // tylko 1 endpoint!
    // POST /graphl

    // Unauthorized - niezalogowany użytkownik - 401
    // Forbid - nie masz praw dostępu do tego zasobu - 403

    [Route("v1/[controller]")]
    public class CustomersControllerV1 : ControllerBase
    {
    }

    [Route("[controller]")]    
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            var customers = await mediator.Send(new GetCustomersQuery());

            return Ok(customers);            
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            var customer = await mediator.Send(new GetCustomerByIdQuery(id));

            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Add([FromBody] Customer customer)
        {
            await mediator.Send(new AddCustomerCommand(customer));

            await mediator.Publish(new CustomerAddedNotification(customer));

            return CreatedAtRoute(new { id = customer.Id }, customer);
        }

        
    }
}
