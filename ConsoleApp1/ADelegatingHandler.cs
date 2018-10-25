using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ADelegatingHandler : DelegatingHandler
    {
        public ADelegatingHandler()
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            System.Diagnostics.Debug.WriteLine(nameof(ADelegatingHandler));
            return base.SendAsync(request, cancellationToken);
        }
    }
}
