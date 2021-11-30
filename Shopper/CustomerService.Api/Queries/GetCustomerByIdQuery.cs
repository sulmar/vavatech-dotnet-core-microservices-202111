using CustomerService.Domain;
using MediatR;

namespace CustomerService.Api.Queries
{
    public record GetCustomerByIdQuery(int id) : IRequest<Customer>;
    
}
