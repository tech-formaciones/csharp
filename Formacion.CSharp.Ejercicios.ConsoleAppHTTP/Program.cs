using Newtonsoft.Json;

namespace Formacion.CSharp.Ejercicios.ConsoleAppHTTP
{
    internal class Program
    {
        static HttpClient http;

        static void Main(string[] args)
        {
            http = new HttpClient();

            ConsultarCodigoPostal();
        }

        // Método GET
        // URL: https://api.zippopotam.us/es/11540
        static void ConsultarCodigoPostal()
        { 
            Console.Clear();
            Console.Write("Escribe un Código Postal: ");
            string code = Console.ReadLine();

            var response = http.GetAsync($"https://api.zippopotam.us/es/{code}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);

                Console.WriteLine($"Código Postal: {data["post code"]}");

                foreach (var place in data["places"])
                    Console.WriteLine($" -> {place["place name"]} ({place["state"]})");
            }
            else Console.WriteLine($"Error: {response.StatusCode}");

        }
    }
}
