using CustomerService.Api.Queries;
using CustomerService.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerService.Api.Handlers
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>>
    {
        private readonly ICustomerRepositoryAsync customerRepository;

        public GetCustomersHandler(ICustomerRepositoryAsync customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            return await customerRepository.GetAsync();
        }
    }
}
