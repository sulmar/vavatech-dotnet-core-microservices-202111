using CustomerService.Api.Commands;
using CustomerService.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerService.Api.Handlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepositoryAsync customerRepository;

        public UpdateCustomerHandler(ICustomerRepositoryAsync customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await customerRepository.UpdateAsync(request.Customer);

            return Unit.Value;
        }
    }

    public class PatchCustomerHandler : IRequestHandler<PatchCustomerCommand>
    {
        private readonly ICustomerRepositoryAsync customerRepository;

        public PatchCustomerHandler(ICustomerRepositoryAsync customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Unit> Handle(PatchCustomerCommand request, CancellationToken cancellationToken)
        {
            await customerRepository.PatchAsync(request.customerId, request.patchCustomer);
            
            return Unit.Value;
        }
    }
}
