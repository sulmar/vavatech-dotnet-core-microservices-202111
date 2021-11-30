using CustomerService.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Intrastructure
{
    public class SendGridMessageService : IMessageService
    {
        public void Send(Customer customer)
        {
            Trace.WriteLine($"Send to <{customer.Email}> Welcome {customer.FirstName} {customer.LastName}");
        }

        public Task SendAsync(Customer customer)
        {
            Send(customer);

            return Task.CompletedTask;
        }
    }
}
