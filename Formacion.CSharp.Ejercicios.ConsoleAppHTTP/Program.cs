using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace Formacion.CSharp.Ejercicios.ConsoleAppHTTP
{
    internal class Program
    {
        static HttpClient http;

        static void Main(string[] args)
        {
            http = new HttpClient();

            ConsultarCodigoPostal3();
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


        // Método GET
        // URL: https://api.zippopotam.us/es/11540
        static void ConsultarCodigoPostal2()
        {
            Console.Clear();
            Console.Write("Escribe un Código Postal: ");
            string code = Console.ReadLine();

            var response = http.GetAsync($"https://api.zippopotam.us/es/{code}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<PostalCodeInfo>(response.Content.ReadAsStringAsync().Result);

                Console.WriteLine($"Código Postal: {data.PostCode}");

                foreach (var place in data.Places)
                    Console.WriteLine($" -> {place.PlaceName} ({place.State})");
            }
            else Console.WriteLine($"Error: {response.StatusCode}");

        }

        // Método GET
        // URL: https://api.zippopotam.us/es/11540
        static void ConsultarCodigoPostal3()
        {
            Console.Clear();
            Console.Write("Escribe un Código Postal: ");
            string code = Console.ReadLine();

            try
            {
                var data = http.GetFromJsonAsync<PostalCodeInfo>($"https://api.zippopotam.us/es/{code}").Result;
                Console.WriteLine($"Código Postal: {data.PostCode}");

                foreach (var place in data.Places)
                    Console.WriteLine($" -> {place.PlaceName} ({place.State})");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }


    public class PostalCodeInfo
    {
        [JsonProperty("post code")]
        [JsonPropertyName("post code")]
        public string PostCode {get; set;}
        public string Country {get; set;}

        [JsonProperty("country abbreviation")]
        [JsonPropertyName("country abbreviation")]
        public string CountryAbbreviation { get; set;}
        public List<PostalCodePlace> Places {get; set;}
    }

    public class PostalCodePlace
    {
        [JsonProperty("place name")]
        [JsonPropertyName("place name")]
        public string PlaceName { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string State { get; set; }

        [JsonProperty("state abbreviation")]
        [JsonPropertyName("state abbreviation")]
        public string StateCode { get; set; }
    }

}
