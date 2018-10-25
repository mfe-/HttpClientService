using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientService
{
    public class BaseHttpClientService
    {
        public HttpClient HttpClient { get; protected set; }
        public Uri BaseUri { get; protected set; }
        public BaseHttpClientService() : this(null, null) { }
        public BaseHttpClientService(IEnumerable<DelegatingHandler> handler = null) : this(null, handler) { }

        public BaseHttpClientService(Uri uri = null, IEnumerable<DelegatingHandler> handler = null)
        {
            BaseUri = uri;
            if (handler == null)
            {
                HttpClient = HttpClientFactory();
            }
            else
            {
                HttpClient = HttpClientFactory(handler);
            }
        }
        public virtual HttpClient HttpClientFactory(IEnumerable<DelegatingHandler> httpClientHandlers = null)
        {
            HttpClient httpClient = null;
            if (httpClientHandlers == null)
            {
                httpClient = new HttpClient();
            }
            else
            {
                IEnumerator<DelegatingHandler> enumeratorDelegatingHandler = httpClientHandlers.GetEnumerator();
                DelegatingHandler previousDelegatingHandler = null;
                DelegatingHandler currentDelegatingHandler = null;
                do
                {
                    enumeratorDelegatingHandler.MoveNext();
                    currentDelegatingHandler = enumeratorDelegatingHandler.Current;
                    //set innerhandler of previous handler
                    if (previousDelegatingHandler != null && currentDelegatingHandler != null)
                    {
                        if (currentDelegatingHandler.InnerHandler == null)
                        {
                            currentDelegatingHandler.InnerHandler = new HttpClientHandler();
                        }
                        previousDelegatingHandler.InnerHandler = currentDelegatingHandler;
                        if (previousDelegatingHandler.InnerHandler == null)
                        {
                            previousDelegatingHandler.InnerHandler = new HttpClientHandler();
                        }
                    }
                    //set previous handler
                    if (currentDelegatingHandler != null)
                    {
                        previousDelegatingHandler = currentDelegatingHandler;
                    }
                    //connect handler to httpclient
                    if (httpClient == null)
                    {
                        httpClient = new HttpClient(currentDelegatingHandler, false);
                    }
                }
                while (enumeratorDelegatingHandler.Current != null);
            }
            return httpClient;
        }

        public virtual Task<T> ToObjectAsync<T>(HttpResponseMessage response)
        {
            return response.ToObjectAsync<T>();
        }

        public virtual HttpContent ToJson<T>(T model)
        {
            return model.ToJson<T>();
        }
    }
}
