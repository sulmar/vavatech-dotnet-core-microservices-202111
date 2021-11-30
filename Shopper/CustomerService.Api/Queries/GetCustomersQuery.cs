using CustomerService.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Api.Queries
{
    // dotnet add package MediatR
    public record GetCustomersQuery : IRequest<IEnumerable<Customer>>;  // mark interface 
    
}
