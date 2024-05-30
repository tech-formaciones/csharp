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
        }

        static void Get()
        {
            // get

            HttpResponseMessage response = http.GetAsync("get").Result;
        
        }
    }
}
