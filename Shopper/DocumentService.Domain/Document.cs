using System;

namespace DocumentService.Domain
{
    public class Document
    {
        public string Title { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; } = "application/pdf";
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
