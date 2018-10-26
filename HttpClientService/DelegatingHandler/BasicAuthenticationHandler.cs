using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientService.DelegatingHandler
{
    public class BasicAuthenticationHandler : System.Net.Http.DelegatingHandler
    {
        public BasicAuthenticationHandler(String username,String password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            byte[] UserArray = Encoding.ASCII.GetBytes($"{Username}:{Password}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(UserArray));
            return base.SendAsync(request, cancellationToken);
        }
    }
}
