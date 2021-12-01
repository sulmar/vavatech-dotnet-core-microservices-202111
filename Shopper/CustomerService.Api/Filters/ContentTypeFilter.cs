using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CustomerService.Api.Filters
{
    public class ContentTypeFilterAttribute : Attribute, IResourceFilter
    {
        private readonly string contentType;

        public ContentTypeFilterAttribute(string contentType)
        {
            this.contentType = contentType;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
           
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!string.Equals(MediaTypeHeaderValue.Parse(context.HttpContext.Request.ContentType).MediaType, contentType,
              StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new UnsupportedMediaTypeResult();
            }
        }
    }
}
