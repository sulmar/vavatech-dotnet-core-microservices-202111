using CustomerService.Api.Commands;
using CustomerService.Api.Filters;
using CustomerService.Api.Notifications;
using CustomerService.Api.Queries;
using CustomerService.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

    //[Authorize(Roles = "Trainer, Member")]
    [Route("v1/[controller]")]
    public class CustomersControllerV1 : ControllerBase
    {
    }

  
    [Route("[controller]")]    
    // [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<CustomersController> logger;

        public CustomersController(IMediator mediator, ILogger<CustomersController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
      //  [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Customer>>> Get()
        {
            //if (!this.User.Identity.IsAuthenticated)
            //{
            //    return Unauthorized();
            //}

            
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
           // ModelState.AddModelError("Pesel", "Taki pesel już istnieje.");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await mediator.Send(new AddCustomerCommand(customer));

            await mediator.Publish(new CustomerAddedNotification(customer));

            return CreatedAtRoute(new { id = customer.Id }, customer);
        }


        // PUT /customers/10 { "Id": 11, "FirstName":"", "LastName":"" }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            await mediator.Send(new UpdateCustomerCommand(customer));

            return NoContent();
        }

        // PATCH /customers/10 { "LastName":"Kowalski", "Age": null }
        // https://www.rfc-editor.org/rfc/rfc6902
        // http://jsonpatch.com/

        // dotnet add package Microsoft.AspNetCore.JsonPatch
        // Content-Type: application/json-patch+json
        // .AddNewtonsoftJson()

        [HttpPatch("{id}")]    
        [ContentTypeFilter("application/json-patch+json")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Customer> patchCustomer)
        {
            logger.LogInformation("Patch Customer Id = {0}", id);

            await mediator.Send(new PatchCustomerCommand(id, patchCustomer));

            return NoContent();
        }
    }
}
