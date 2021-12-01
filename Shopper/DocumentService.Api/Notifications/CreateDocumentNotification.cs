using DocumentService.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentService.Api.Notifications
{
    public record CreateDocumentNotification(Customer customer) : INotification;

    //public class CreateDocumentNotification2 : INotification
    //{
    //    public Customer Customer { get; }

    //    public CreateDocumentNotification2(Customer customer)
    //    {
    //        this.Customer = customer;
    //    }
    //}
}
