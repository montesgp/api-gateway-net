using apex_apigateway_ocelot.Security;
using Google.Apis.Auth.OAuth2;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace apex_apigateway_ocelot.Handlers
{
    public class RemoveEncodingDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.AcceptEncoding.Clear();
            IAPClient iap = new IAPClient();
            SecretManagerService secretManagerService = new SecretManagerService();
            //string secret_token = secretManagerService.GetSecretKey();
            // ID Cliente Lab Pocs for Service Account ***REMOVED_SERVICE_ACCOUNT***
            //var response = iap.GetOidcTokenAsync("***REMOVED_OIDC_CLIENT_ID***", default);
            // ID Cliente Lab Martin Leal
            OidcToken oidcToken = await iap.GetOidcTokenAsync("***REMOVED_OIDC_CLIENT_ID***", cancellationToken);

            //***REMOVED_OIDC_CLIENT_ID***
            //OidcToken oidcToken = await iap.GetOidcTokenAsync("***REMOVED_OIDC_CLIENT_ID***", cancellationToken);
            string token = await oidcToken.GetAccessTokenAsync(cancellationToken);
            //Console.Write("Bearer Token:" + token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
