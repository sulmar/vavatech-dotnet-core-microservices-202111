using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerService.Domain
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> Get();
        Customer Get(int id);
        void Add(Customer customer);
        bool Exists(string pesel);
    }
}
