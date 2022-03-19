using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Extensions
{
    public static class EndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapHead(this IEndpointRouteBuilder endpoints, string pattern, RequestDelegate requestDelegate) 
            => endpoints.MapMethods(pattern, new[] { "HEAD" }, requestDelegate);
    }
}
