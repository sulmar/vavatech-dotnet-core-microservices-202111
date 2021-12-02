using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.Domain
{
    public interface ICustomerRepositoryAsync
    {
        Task<IEnumerable<Customer>> GetAsync();
        Task<Customer> GetAsync(int id);
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task PatchAsync(int customerId, JsonPatchDocument<Customer> patchCustomer);
        Task<bool> ExistsAsync(string pesel);
    }
}
