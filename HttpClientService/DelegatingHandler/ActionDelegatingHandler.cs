using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HttpClientService.DelegatingHandler
{
    public class ActionDelegatingHandler : System.Net.Http.DelegatingHandler
    {
        private readonly Action<HttpRequestHeaders> _actionOnSendRequest;

        public ActionDelegatingHandler(Action<HttpRequestHeaders> actionOnRequestSend)
        {
            _actionOnSendRequest = actionOnRequestSend;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _actionOnSendRequest.Invoke(request.Headers);
            return base.SendAsync(request, cancellationToken);
        }
    }
}
