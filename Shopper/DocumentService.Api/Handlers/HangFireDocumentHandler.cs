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
        private readonly IBackgroundJobClient jobClient;

        public HangFireDocumentHandler(IDocumentService documentService, IBackgroundJobClient jobClient)
        {
            this.documentService = documentService;
            this.jobClient = jobClient;
        }

        public Task Handle(CreateDocumentNotification notification, CancellationToken cancellationToken)
        {
            // dotnet add package HangFire.AspNetCore
            // BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forgot"));

            // Uwaga: nietestowalne
            // BackgroundJob.Enqueue(()=>documentService.Create(notification.customer));

                        
            jobClient.Enqueue(() => documentService.Create(notification.customer));
            
            return Task.CompletedTask;
        }
    }
}
