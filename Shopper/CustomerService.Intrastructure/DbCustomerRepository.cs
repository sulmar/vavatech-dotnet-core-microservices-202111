using CustomerService.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.Intrastructure
{
    public class DbCustomerRepository : ICustomerRepositoryAsync
    {
        public Task AddAsync(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
