using CustomerService.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Api.Commands
{
    public record AddCustomerCommand(Customer customer) : IRequest;
}
