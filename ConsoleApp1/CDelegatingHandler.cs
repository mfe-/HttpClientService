using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class CDelegatingHandler : DelegatingHandler
    {
        public CDelegatingHandler()
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine(nameof(CDelegatingHandler));
            Task<HttpResponseMessage> httpResponseMessage = base.SendAsync(request, cancellationToken);
            httpResponseMessage.
                ContinueWith(response => System.Diagnostics.Debug.WriteLine(response.Result.RequestMessage.RequestUri)).
                ContinueWith(response => System.Diagnostics.Debug.WriteLine(httpResponseMessage.Result.Content.ReadAsStringAsync()));
            return httpResponseMessage;
        }
    }
}
