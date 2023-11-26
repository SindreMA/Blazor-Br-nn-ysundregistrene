using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace BrregFrontend
{
    public class CookieHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            return await base.SendAsync(request, cancellationToken);
        }
    }

}
