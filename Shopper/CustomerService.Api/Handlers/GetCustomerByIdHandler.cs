using CustomerService.Api.Queries;
using CustomerService.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerService.Api.Handlers
{
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly ICustomerRepositoryAsync customerRepository;

        public GetCustomerByIdHandler(ICustomerRepositoryAsync customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await customerRepository.GetAsync(request.id);
        }
    }
}
