using Azure;
using Formacion.CSharp.Database.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace Formacion.CSharp.WebApiClient
{
    internal class Program
    {
        static HttpClient http;

        static void Main(string[] args)
        {
            http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:7082/api/");
            http.DefaultRequestHeaders.Add("APIKey", "jP8pz9YDl999yNJSdsz8Jdl2cs>y");

            Console.Clear();

            try
            {
                Get();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

            Console.ReadKey();
        }

        static void Get()
        {
            Console.Write("Referencia del producto: ");
            string id = Console.ReadLine();

            try
            {
                var producto = http.GetFromJsonAsync<Product>($"productos/{id}").Result;
                Console.WriteLine($"Descripción: {producto.ProductName}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        static void Post()
        {
            var producto = new Product();

            Console.Write("Descripción: ");
            producto.ProductName = Console.ReadLine();
            Console.Write("Unidad: ");
            producto.UnitsInStock = Convert.ToInt16(Console.ReadLine());
            Console.Write("Precio: ");
            producto.UnitPrice = Convert.ToDecimal(Console.ReadLine());
            producto.CategoryID = 1;

            var response = http.PostAsJsonAsync<Product>("productos", producto).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                string contenido = response.Content.ReadAsStringAsync().Result;
                producto = JsonConvert.DeserializeObject<Product>(contenido);

                Console.WriteLine($"Producto insertado con el identificador {producto.ProductID}");
            }
            else Console.WriteLine($"Error: {response.StatusCode}");
        }

        static void Post2()
        {
            var producto = new Product();

            Console.Write("Descripción: ");
            producto.ProductName = Console.ReadLine();
            Console.Write("Unidad: ");
            producto.UnitsInStock = Convert.ToInt16(Console.ReadLine());
            Console.Write("Precio: ");
            producto.UnitPrice = Convert.ToDecimal(Console.ReadLine());
            producto.CategoryID = 1;

            string productoJSON = JsonConvert.SerializeObject(producto);
            StringContent requestContent = new StringContent(productoJSON, Encoding.UTF8, "application/json");

            var response = http.PostAsync("productos", requestContent).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                string contenido = response.Content.ReadAsStringAsync().Result;
                producto = JsonConvert.DeserializeObject<Product>(contenido);

                Console.WriteLine($"Producto insertado con el identificador {producto.ProductID}");
            }
            else Console.WriteLine($"Error: {response.StatusCode}");
        }

        static void Put()
        {
            Console.Write("Referencia del producto: ");
            string id = Console.ReadLine();

            try
            {
                // Consulta del producto utilizando el API
                var producto = http.GetFromJsonAsync<Product>($"productos/{id}").Result;
                Console.WriteLine($"Descripción: {producto.ProductName}");

                // Modificamos
                Console.Write("Nueva descripción: ");
                producto.ProductName = Console.ReadLine();

                var response = http.PutAsJsonAsync<Product>($"productos/{id}", producto).Result;
                
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    Console.WriteLine("Producto modificado correctamente.");
                else 
                    Console.WriteLine($"Error: {response.StatusCode}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        static void Delete()
        {
            Console.Write("Referencia del producto: ");
            string id = Console.ReadLine();

            try
            {
                var response = http.DeleteAsync($"productos/{id}").Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    Console.WriteLine("Producto eliminado correctamente.");
                else
                    Console.WriteLine($"Error: {response.StatusCode}");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
