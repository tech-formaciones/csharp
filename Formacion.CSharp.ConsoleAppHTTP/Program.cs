﻿using Newtonsoft.Json;
using System.Net;

namespace Formacion.CSharp.ConsoleAppHTTP
{
    internal class Program
    {
        private static HttpClient http = new HttpClient();

        static void Main(string[] args)
        {
            // Opcionalmente espificamos la dirección basa para las URLs de consulta.
            
            http.BaseAddress = new Uri("https://postman-echo.com/");


            // Headers opcionales del objeto HttpClient
            // Se envian en todas las peteciones lanzandas con este objeto
            // Son utililes para espeficiar token o claves (APIKey) de accceso

            http.DefaultRequestHeaders.Clear();
            http.DefaultRequestHeaders.Add("User-Agent", "HttpCliente .NET Core Demo");
            http.DefaultRequestHeaders.Add("X-Data-1", "Demo");
            http.DefaultRequestHeaders.Add("X-Data-2", "1234567890");


            // Llamada a GET
            Get();
        }

        static void Get()
        {
            // get

            HttpResponseMessage response = http.GetAsync("get?param1=hola").Result;

            // Opción 1, preguntamos por un código de estado concreto.

            if (response.StatusCode == HttpStatusCode.OK)
            {
                // Lectura del cuerpo del mensaje de respuesta como STRING
                // El texto obtenido normalmente estara en JSON

                string responseBodyText = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine("Body:");
                Console.WriteLine("=============================================");
                Console.WriteLine($"{responseBodyText}");


                // Convertir el JSON en Objeto, indicado el tipo
                // Utilizamos DYNAMIC cuando no tenemos una clase que represente el objeto retornado

                var obj = JsonConvert.DeserializeObject<dynamic>(responseBodyText);

                Console.WriteLine("");
                Console.WriteLine("Datos del Body:");
                Console.WriteLine("=============================================");
                Console.WriteLine($"URL: {obj["url"]}");
                Console.WriteLine($"Param 1: {obj["args"]["param1"]}");
                Console.WriteLine($"Data 1: {obj["headers"]["x-data-1"]}");

                Console.WriteLine($"URL: {obj.url}");
                Console.WriteLine($"Param 1: {obj.args.param1}");

                Console.WriteLine("");
                Console.WriteLine("Headers:");
                Console.WriteLine("=============================================");
                // También tenemos acceso a las cabeceras del mensaje de respuesta
                foreach (var header in response.Headers)
                {
                    Console.WriteLine($"{header.Key}: {header.Value.FirstOrDefault()}");
                }

                Console.WriteLine($"Date: {response.Headers.GetValues("Date").FirstOrDefault()}");

                IEnumerable<string> valor;
                response.Headers.TryGetValues("Content-Type", out valor);

                Console.WriteLine($"Content-Type: {(valor == null ? "" : valor.FirstOrDefault())}");

            }
            else Console.WriteLine($"Error: {response.StatusCode}");


            // Opción 2, preguntamos si finalizada con cualquier códgo 2XX

            if (response.IsSuccessStatusCode)
            {
                // El mismo código que la opción 1
            }
            else Console.WriteLine($"Error: {response.StatusCode}");
        }
    }


}
