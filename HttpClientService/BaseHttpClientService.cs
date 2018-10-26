using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientService
{
    public class BaseHttpClientService
    {
        public HttpClient HttpClient { get; protected set; }
        public Uri BaseUri { get; protected set; }
        public BaseHttpClientService() : this(null, null) { }
        public BaseHttpClientService(IEnumerable<System.Net.Http.DelegatingHandler> handler = null) : this(null, handler) { }

        public BaseHttpClientService(Uri uri = null, IEnumerable<System.Net.Http.DelegatingHandler> handler = null)
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
        public virtual HttpClient HttpClientFactory(IEnumerable<System.Net.Http.DelegatingHandler> httpClientHandlers = null)
        {
            HttpClient httpClient = null;
            if (httpClientHandlers == null)
            {
                httpClient = new HttpClient();
            }
            else
            {
                IEnumerator<System.Net.Http.DelegatingHandler> enumeratorDelegatingHandler = httpClientHandlers.GetEnumerator();
                System.Net.Http.DelegatingHandler previousDelegatingHandler = null;
                System.Net.Http.DelegatingHandler currentDelegatingHandler = null;
                do
                {
                    enumeratorDelegatingHandler.MoveNext();
                    currentDelegatingHandler = enumeratorDelegatingHandler.Current;
                    if (currentDelegatingHandler!= null && currentDelegatingHandler.InnerHandler == null)
                    {
                        currentDelegatingHandler.InnerHandler = new HttpClientHandler();
                    }
                    //set innerhandler of previous handler
                    if (previousDelegatingHandler != null && currentDelegatingHandler != null)
                    {
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
