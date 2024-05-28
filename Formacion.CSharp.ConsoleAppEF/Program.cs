using System.Data;
using System.Data.SqlClient;

namespace Formacion.CSharp.ConsoleAppEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsultaConADONET();
        }

        /// <summary>
        /// Ejecutamos una consulta de datos utilizando ADO.NET
        /// </summary>
        static void ConsultaConADONET()
        {
            // SELECT * FROM dbo.Customers

            // Creamos un objeto para crear la cadena de conexión
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder()
            {
                DataSource = "hostdb-eoi.database.windows.net",
                InitialCatalog = "Northwind",
                UserID = "Administrador",
                Password = "azurePa$$w0rd",
                IntegratedSecurity = false,
                ConnectTimeout = 60
            };

            // Mostramos la cadena de conexión resultante con los datos introducidos
            Console.WriteLine("Cadena de conexión: {0}", csb.ToString());

            // Creamos el objeto que representa la conexión con la base de datos
            SqlConnection connection2 = new SqlConnection(csb.ToString());
            //SqlTransaction transaction = connection2.BeginTransaction();

            SqlConnection connection = new SqlConnection()
            {
                ConnectionString = csb.ToString()
            };
            Console.WriteLine($"Estado de la conexión: {connection.State}");
            // Abrimos la conexión con la base de datos
            connection.Open();
            Console.WriteLine($"Estado de la conexión: {connection.State}");

            // Creamos un objeto que representa el comando que ejecutaremos en la base de datos
            SqlCommand command2 = new SqlCommand("UPDATE FROM dbo.Customers SET Country = 'Spain' WHERE Country = 'España'", connection2);

            SqlCommand command = new SqlCommand()
            {
                Connection = connection,
                CommandText = "SELECT * FROM dbo.Customers"
            };

            // Ejecución del comando
            
            // Si el comando retorna datos tenemos que crear un cursor que nos permita recorrer los
            // datos recuperados. (comandos SELECT)
            SqlDataReader cursor = command.ExecuteReader();

            // Recorremos los datos del cursor
            if (cursor.HasRows == false) Console.WriteLine("Registros no encontrados");
            else
            {
                while (cursor.Read() == true)
                {
                    Console.Write($"{cursor["CustomerID"].ToString().PadLeft(5, ' ')}# ");
                    Console.Write($"{cursor.GetValue(1).ToString().PadRight(40, ' ')} ");
                    Console.WriteLine($"{cursor["Country"]}");
                }
            }

            // Si el comando NO retorna datos recogemos en una variable INT el número de registros
            // afectados por el comando. (comandos INSERT, UPDATE y DELETE)

            // int rows = command2.ExecuteNonQuery();
            // transaction.Rollback();
            // transaction.Commit();

            // Cerramos conexiones y destruimos variables
            cursor.Close();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
    }
}
