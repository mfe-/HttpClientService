using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientService
{
    public static class BaseHttpClientServiceExtensions
    {
        public static async Task<T> ToObjectAsync<T>(this HttpResponseMessage response, Action<string>? jsonResultCallBack = null)
        {
            try
            {
                if (response.Content != null)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    jsonResultCallBack?.Invoke(jsonString);
                    var model = JsonConvert.DeserializeObject<T>(jsonString);
                    return model;
                }
            }
            catch (JsonReaderException e)
            {
                jsonResultCallBack?.Invoke(e.ToString());
            }
#pragma warning disable CS8603 // Possible null reference return.
            return default;
#pragma warning restore CS8603 // Possible null reference return.
        }
        public static HttpContent ToStringContentJson<T>(this T model)
        {
            String jsonString = JsonConvert.SerializeObject(model);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
