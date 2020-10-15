using HttpClientService;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hello World!");

                MainAsync(args).Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
        private static async Task MainAsync(string[] args)
        {
            try
            {
                BaseHttpClientService baseHttpClientService = new BaseHttpClientService(
                    new Uri("http://google.de")
                        , new List<DelegatingHandler>()
                        {
                            new ADelegatingHandler(),
                            new BDelegatingHandler(),
                            //new CDelegatingHandler()
                        }
                );

                HttpResponseMessage message = await baseHttpClientService.HttpClient.GetAsync("http://google.de");

                Console.WriteLine(await message.Content.ReadAsStringAsync());
                Console.Clear();

                var responseMessage = await baseHttpClientService.HttpClient.GetAsync("https://jsonplaceholder.typicode.com/users");

                //[
                //{
                //                  "id": 1,
                //  "name": "Leanne Graham",
                //  "username": "Bret",
                //  "email": "Sincere@april.biz",
                //  "address": {
                //                      "street": "Kulas Light",
                //    "suite": "Apt. 556",
                //    "city": "Gwenborough",
                //    "zipcode": "92998-3874",
                //    "geo": {
                //                          "lat": "-37.3159",
                //      "lng": "81.1496"
                //    }
                //                  },

                var bla = await responseMessage.ToObjectAsync<dynamic>();


                var x = bla[0].address.street;
                Console.WriteLine(bla);
                Console.Clear();


            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }

    }
}
