using CustomerService.Api.Notifications;
using CustomerService.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerService.Api.Handlers
{
    public class MessageNotificationHandler : INotificationHandler<CustomerAddedNotification>
    {
        private readonly IMessageService messageService;

        public MessageNotificationHandler(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task Handle(CustomerAddedNotification notification, CancellationToken cancellationToken)
        {
            await messageService.SendAsync(notification.customer);
        }
    }

    public class SendTweetNotificationHandler : INotificationHandler<CustomerAddedNotification>
    {
        public Task Handle(CustomerAddedNotification notification, CancellationToken cancellationToken)
        {
            Trace.WriteLine($"Send tweet to {notification.customer.Email}");

            return Task.CompletedTask;
        }
    }
}
