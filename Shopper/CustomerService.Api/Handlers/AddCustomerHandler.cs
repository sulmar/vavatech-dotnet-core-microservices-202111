using CustomerService.Api.Commands;
using CustomerService.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerService.Api.Handlers
{
    public class AddCustomerHandler : IRequestHandler<AddCustomerCommand>
    {
        private readonly ICustomerRepositoryAsync customerRepository;

        public AddCustomerHandler(ICustomerRepositoryAsync customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            await customerRepository.AddAsync(request.customer);

            return Unit.Value;
        }
    }
}
