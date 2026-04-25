using Microsoft.AspNetCore.Http;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Flowboard.Infrastructure.Handlers
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthTokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null &&
                context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var token = authHeader.ToString();

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Remove("Authorization"); // avoid duplicates

                    request.Headers.TryAddWithoutValidation("Authorization", token);

                    Console.WriteLine("✅ Token forwarded to Portal:");
                    Console.WriteLine(token);
                }
            }
            else
            {
                Console.WriteLine("❌ No token found in request");
            }

            return await base.SendAsync(request, cancellationToken);
        }


    }
}