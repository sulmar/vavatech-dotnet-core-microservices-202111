using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentService.Domain
{
    public interface IDocumentService
    {
        Document Create(Customer customer);
    }
}
