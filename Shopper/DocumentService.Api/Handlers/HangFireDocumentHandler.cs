using DocumentService.Api.Notifications;
using DocumentService.Domain;
using Hangfire;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocumentService.Api.Handlers
{
    public class HangFireDocumentHandler : INotificationHandler<CreateDocumentNotification>
    {
        private readonly IDocumentService documentService;

        public HangFireDocumentHandler(IDocumentService documentService)
        {
            this.documentService = documentService;
        }

        public Task Handle(CreateDocumentNotification notification, CancellationToken cancellationToken)
        {
            // dotnet add package HangFire.AspNetCore
            // BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forgot"));

            BackgroundJob.Enqueue(()=>documentService.Create(notification.customer));
            
            return Task.CompletedTask;
        }
    }
}
