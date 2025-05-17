using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SalesHomes.Services
{
    public class JwtBearerAuthenticationRepository : DelegatingHandler
    {
        private readonly JwtAuthManagerService _authManager;

        public JwtBearerAuthenticationRepository(JwtAuthManagerService authManager)
        {
            _authManager = authManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = request.Headers.Authorization;
            if (authorizationHeader != null && authorizationHeader.Scheme == "Bearer")
            {
                var token = authorizationHeader.Parameter;
                var principal = _authManager.ValidateToken(token);

                if (principal != null)
                {
                    Thread.CurrentPrincipal = principal;
                    request.GetRequestContext().Principal = principal;
                }
                else
                {
                    return request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Token inválido o expirado");
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}