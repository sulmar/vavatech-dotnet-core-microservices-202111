using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace DocumentService.Api
{
    // https://khalidabuhakmeh.com/simple-redirects-with-aspnet-core-endpoint-routing
    public static class EndpointExtensions
    {
        public static IEndpointRouteBuilder Redirect(
            this IEndpointRouteBuilder endpoints,
            string from, string to)
        {
            return Redirect(endpoints,
                new Redirective(from, to));
        }

        public static IEndpointRouteBuilder RedirectPermanent(
            this IEndpointRouteBuilder endpoints,
            string from, string to)
        {
            return Redirect(endpoints,
                new Redirective(from, to, true));
        }

        public static IEndpointRouteBuilder Redirect(
            this IEndpointRouteBuilder endpoints,
            params Redirective[] paths
        )
        {
            foreach (var (from, to, permanent) in paths)
            {
                endpoints.MapGet(from, async http => { http.Response.Redirect(to, permanent); });
            }

            return endpoints;
        }
    }

    public record Redirective(string From, string To, bool Permanent = false);
}
