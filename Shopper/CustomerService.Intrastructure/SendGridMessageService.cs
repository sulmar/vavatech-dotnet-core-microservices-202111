using CustomerService.Domain;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Intrastructure
{
    public class SendGridOptions
    {
        public string Url { get; set; }
        public string ApiKey { get; set; }        
    }

    public class SendGridMessageService : IMessageService
    {
        private readonly SendGridOptions options;

        public SendGridMessageService(IOptions<SendGridOptions> options)
        {
            this.options = options.Value;
        }

        public void Send(Customer customer)
        {
            Trace.WriteLine($"Send to <{customer.Email}> via {options.Url} with {options.ApiKey} Welcome {customer.FirstName} {customer.LastName}");
        }

        public Task SendAsync(Customer customer)
        {
            Send(customer);

            return Task.CompletedTask;
        }
    }
}
