using CustomerService.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerService.Intrastructure
{
    public class DbCustomerRepository : ICustomerRepositoryAsync
    {
        private readonly CustomerContext context;

        public DbCustomerRepository(CustomerContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await context.Customers.AsNoTracking().ToListAsync();
        }

        public async Task<Customer> GetAsync(int id)
        {
            return await context.Customers.FindAsync(id);
        }
    }
}
