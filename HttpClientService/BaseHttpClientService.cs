using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace HttpClientService
{
    public class BaseHttpClientService
    {
        public HttpClient HttpClient { get; protected set; }
        public Uri BaseUri { get; protected set; }
        public BaseHttpClientService(Uri uri, HttpClientHandler handler = null)
        {
            BaseUri = uri;
            if (handler == null)
            {
                HttpClient = new HttpClient();
            }
            else
            {
                HttpClient = new HttpClient(handler);
            }
        }

        public virtual T ToObject<T>(HttpResponseMessage response)
        {
            var jsonString = response.Content.ReadAsStringAsync().Result;

            var model = JsonConvert.DeserializeObject<T>(jsonString);

            return model;
        }

        public virtual HttpContent ToJson<T>(T model)
        {
            String jsonString = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonString, Encoding.UTF8, "text/plain");
            return content;
        }
    }
}
