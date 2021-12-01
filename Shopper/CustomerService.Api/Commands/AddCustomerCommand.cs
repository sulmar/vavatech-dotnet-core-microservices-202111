using CustomerService.Domain;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Api.Commands
{
    public record AddCustomerCommand(Customer customer) : IRequest;
    public record UpdateCustomerCommand(Customer Customer) : IRequest;
    public record PatchCustomerCommand(int customerId, JsonPatchDocument<Customer> patchCustomer) : IRequest;
}
