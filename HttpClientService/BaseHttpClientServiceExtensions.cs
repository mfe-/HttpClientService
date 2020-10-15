using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
                    var options = new JsonSerializerOptions
                    {
                        AllowTrailingCommas = true
                    };
                    var jsonString = await response.Content.ReadAsStringAsync();
                    jsonResultCallBack?.Invoke(jsonString);
                    var model = JsonSerializer.Deserialize<T>(jsonString, options);
                    return model;
                }
            }
            catch (JsonException e)
            {
                jsonResultCallBack?.Invoke(e.ToString());
            }
#pragma warning disable CS8603 // Possible null reference return.
            return default;
#pragma warning restore CS8603 // Possible null reference return.
        }
        public static HttpContent ToStringContentJson<T>(this T model)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = false
            };
            String jsonString = JsonSerializer.Serialize(model, options);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
