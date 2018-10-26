using HttpClientService;
using HttpClientService.DelegatingHandler;
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
            Console.WriteLine("Hello World!");
            Task.Run(async () =>
            {
                try
                {
                    BaseHttpClientService baseHttpClientService = new BaseHttpClientService(
                        new List<DelegatingHandler>() { new ADelegatingHandler(), new BDelegatingHandler(), new CDelegatingHandler() }
                    );

                    HttpResponseMessage message = await baseHttpClientService.HttpClient.GetAsync("http://google.de");

                    Console.WriteLine(await message.Content.ReadAsStringAsync());
                    Console.Clear();
                    dynamic bla = await (await baseHttpClientService.HttpClient.GetAsync("https://jsonplaceholder.typicode.com/todos/1")).ToObjectAsync<dynamic>();
                    Console.WriteLine(bla);
                    Console.Clear();


                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e);
                }
            });
            Console.ReadLine();
        }

    }
}
