using CustomerService.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.Api.Notifications
{
    public record CustomerAddedNotification(Customer customer) : INotification;
    
}
