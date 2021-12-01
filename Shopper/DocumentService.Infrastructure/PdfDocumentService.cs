using DocumentService.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading;

namespace DocumentService.Infrastructure
{
    public class PdfDocumentService : IDocumentService
    {
        
        public PdfDocumentService()
        {

        }


        public Document Create(Customer customer)
        {
            Thread.Sleep(TimeSpan.FromMinutes(1));

            return new Document
            {
                Title = $"Report for {customer.FirstName} {customer.LastName}",

                Content = Encoding.ASCII.GetBytes($"{customer.FirstName} {customer.LastName}")
            };
        }
    }
}
