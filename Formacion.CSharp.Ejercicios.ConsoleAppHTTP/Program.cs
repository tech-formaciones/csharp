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

            ConsultarIP();
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


        // Método GET
        // URL: http://ip-api.com/json/193.146.141.207
        static void ConsultarIP()
        {
            Console.Clear();
            Console.Write("Escribe una dirección IPv4: ");
            string code = Console.ReadLine();

            // Comprobar que es una dirección IP mediante un expresión regular
            // 0-255.0-255.0-255.0-255


            var response = http.GetAsync($"http://ip-api.com/json/{code}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<IPInfo>(response.Content.ReadAsStringAsync().Result);

                Console.WriteLine($"Dirección IP: {data.Query}");
                Console.WriteLine($"Región: {data.RegionName}");
                Console.WriteLine($"Time Zone: {data.TimeZone}");
                Console.WriteLine($"ISP: {data.Isp}");
                Console.WriteLine($"Organización: {data.Org}");    
            }
            else Console.WriteLine($"Error: {response.StatusCode}");

        }

        // Método GET
        // URL: http://ip-api.com/json/193.146.141.207
        static void ConsultarIP2()
        {
            Console.Clear();
            Console.Write("Escribe una dirección IPv4: ");
            string code = Console.ReadLine();

            try
            {
                var data = http.GetFromJsonAsync<IPInfo>($"http://ip-api.com/json/{code}").Result;

                Console.WriteLine($"Dirección IP: {data.Query}");
                Console.WriteLine($"Región: {data.RegionName}");
                Console.WriteLine($"ISP: {data.Isp}");
                Console.WriteLine($"Organización: {data.Org}");
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

    public class IPInfo
    { 
        public string Status { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public string TimeZone { get; set; }
        public string Isp { get; set; }
        public string Org { get; set; }
        public string As { get; set; }
        public string Query { get; set; }
    }

}
