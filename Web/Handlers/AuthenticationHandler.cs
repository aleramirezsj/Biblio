using System.Net.Http.Headers;
using Web.Services;

namespace Web.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly ITokenProvider _tokenProvider;

        public AuthenticationHandler(ITokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var jwt = await _tokenProvider.GetTokenAsync(cancellationToken);
            if (!string.IsNullOrWhiteSpace(jwt))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
