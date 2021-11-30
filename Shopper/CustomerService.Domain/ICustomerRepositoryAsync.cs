using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.Domain
{
    public interface ICustomerRepositoryAsync
    {
        Task<IEnumerable<Customer>> GetAsync();
        Task<Customer> GetAsync(int id);
        Task AddAsync(Customer customer);
    }
}
