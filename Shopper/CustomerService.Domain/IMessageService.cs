using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Domain
{
    public interface IMessageService
    {
        void Send(Customer customer);
        Task SendAsync(Customer customer);
    }
}
