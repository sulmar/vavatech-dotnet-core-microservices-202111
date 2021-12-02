using FluentValidation;

namespace CustomerService.Domain
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private readonly ICustomerRepositoryAsync customerRepository;

        public CustomerValidator(ICustomerRepositoryAsync customerRepository)
        {
            this.customerRepository = customerRepository;

            RuleFor(p => p.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Password).Equal(p => p.ConfirmPassword).WithMessage("Passwords do not match");
            RuleFor(p => p.Pesel).MustAsync(async (pesel, token)=> !await customerRepository.ExistsAsync(pesel)).WithMessage("Pesel już istnieje");
        }
    }
    

}
