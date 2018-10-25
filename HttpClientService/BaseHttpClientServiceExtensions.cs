using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientService
{
    public static class BaseHttpClientServiceExtensions
    {
        public static async Task<T> ToObjectAsync<T>(this HttpResponseMessage response)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<T>(jsonString);
            return model;
        }
        public static HttpContent ToJson<T>(this T model)
        {
            String jsonString = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonString, Encoding.UTF8, "text/plain");
            return content;
        }
    }
}
