using Formacion.CSharp.ConsoleAppEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Formacion.CSharp.ConsoleAppEF
{
    internal class Program
    {
        /// <summary>
        /// Inicio de la aplicación
        /// </summary>
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("".PadRight(56, '*'));
                Console.WriteLine("*  DEMOS CON EF + LINQ".PadRight(55) + "*");
                Console.WriteLine("".PadRight(56, '*'));
                Console.WriteLine("*".PadRight(55) + "*");
                Console.WriteLine("*  1. Consultar datos con ADO.NET".PadRight(55) + "*");
                Console.WriteLine("*  2. Consultar datos con EntityFramework".PadRight(55) + "*");
                Console.WriteLine("*  3. Insertar datos con EntityFramework".PadRight(55) + "*");
                Console.WriteLine("*  4. Actualizar datos con EntityFramework".PadRight(55) + "*");
                Console.WriteLine("*  5. Eliminar datos con EntityFramework".PadRight(55) + "*");
                Console.WriteLine("*  6. Comandos avanzados con EntityFramework".PadRight(55) + "*");
                Console.WriteLine("*  9. Salir".PadRight(55) + "*");
                Console.WriteLine("*".PadRight(55) + "*");
                Console.WriteLine("".PadRight(56, '*'));

                Console.WriteLine(Environment.NewLine);
                Console.Write("   Opción: ");

                Console.ForegroundColor = ConsoleColor.Cyan;

                int.TryParse(Console.ReadLine(), out int opcion);
                switch (opcion)
                {
                    case 1:
                        ConsultaConADONET();
                        break;
                    case 2:
                        ConsultaConEF();
                        break;
                    case 3:
                        InsertarDatosEF();
                        break;
                    case 4:
                        ActualizarDatosEF();
                        break;
                    case 5:
                        EliminarDatosEF();
                        break;
                    case 6:
                        ComandosAvanzadosEF();
                        break;
                    case 9:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(Environment.NewLine + $"La opción {opcion} no es valida.");
                        break;
                }

                Console.WriteLine(Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Pulsa una tecla para continuar...");
                Console.ReadKey();
            }
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


        ////////////////////////////////////////////////////////////////////////////////
        // EntityFramework (manejamos las base de datos como colecciones)
        /////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Consultar datos con Entity Framework Core
        /// </summary>
        static void ConsultaConEF()
        {         
            // Declaración de la variable de contexto
            var context = new NorthwindContext();


            /////////////////////////////////////////////////////////////////////////////////
            // Consulta de Datos - SELECT
            // Equivalente a: SELECT * FROM Customers
            /////////////////////////////////////////////////////////////////////////////////
            
            var clientes = context.Customers
                .ToList();

            var clientes2 = from r in context.Customers
                            select r;

            foreach (var cliente in clientes2)
            {
                Console.Write($"{cliente.CustomerID.PadLeft(5, ' ')}# ");
                Console.Write($"{cliente.CompanyName.PadRight(40, ' ')} ");
                Console.WriteLine($"{cliente.Country}");
            }
        }

        /// <summary>
        /// Insertar datos con Entity Framework Core
        /// </summary>
        static void InsertarDatosEF()
        {
            // Declaración de la variable de contexto
            var context = new NorthwindContext();

            /////////////////////////////////////////////////////////////////////////////////
            // Insertar Datos - INSERT
            // Equivalente a: INSERT INTO Customers VALUES(..., ..., )
            /////////////////////////////////////////////////////////////////////////////////

            // Creamos un objeto cliente, para insertar en la base de datos
            var cliente = new Customer()
            {
                CustomerID = "BCR01",
                CompanyName = "Empresa Uno, SL",
                ContactName = "Borja Cabeza",
                ContactTitle = "Generente",
                Address = "Calle Paraiso, 33",
                Region = "Madrid",
                City = "Madrid",
                PostalCode = "28013",
                Country = "España",
                Phone = "900 900 900",
                Fax = "900 900 910"
            };


            // Opción 1 - Insertamos el objeto con el método Entry y modificando la propiedad State
            context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Added;


            // Creamos un objeto cliente, para insertar en la base de datos
            var cliente2 = new Customer()
            {
                CustomerID = "BCR02",
                CompanyName = "Empresa Dos, SL",
                ContactName = "Julian Cabeza",
                ContactTitle = "CEO",
                Address = "Calle Paraiso, 33",
                Region = "Madrid",
                City = "Madrid",
                PostalCode = "28013",
                Country = "España",
                Phone = "900 900 900",
                Fax = "900 900 910"
            };

            // Opción 2 - Añadimos el cliente a la colección Customer
            context.Customers.Add(cliente2);


            // Confirmamos la transación y los nuevo datos aparecen en la base de datos
            // Los dos clientes se insertan en la base de datos
            // Si no ejecutamos .SaveChanges() las inserciones se retroceden y los datos no se insertan en la base de datps
            context.SaveChanges();

            Console.WriteLine("Registros insertado correctamente.");
        }

        /// <summary>
        /// Actualizar datos con Entity Framework Core
        /// </summary>
        static void ActualizarDatosEF()
        {           
            // Declaración de la variable de contexto
            var context = new NorthwindContext();

            // Opcion 1
            // Recuperamos el cliente de la base de datos, modificamos valores de las propiedades
            // y grabamos los cambios.

            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Modificar Datos - UPDATE
            // Equivalente a: UPDATE Customers SET ContactName = 'Carlos Sanz', PostalCode = '28013' WHERE CustomerID = 'BCR01'
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            var cliente = context.Customers
                .Where(r => r.CustomerID == "BCR01")
                .FirstOrDefault();

            if (cliente == null) Console.WriteLine("NO existe el cliente.");
            else
            {
                cliente.ContactName = "Carlos Sanz";
                cliente.PostalCode = "28013";

                context.SaveChanges();

                Console.WriteLine("Cliente actualizado correctamente.");
            }            

            // Declaración de la variable de contexto
            var context2 = new NorthwindContext();


            // Opcion 2
            // Instanciamos un objeto que representa un registro existen en la base de datos, pero
            // con valores diferentes y lo utilizamos para actualizar

            var cliente2 = new Customer()
            {
                CustomerID = "BCR01",
                CompanyName = "Empresa Uno, SL",
                ContactName = "Carlos Sanz",
                ContactTitle = "Gerente",
                Address = "Avenida Paraiso, 33",
                Region = "Madrid",
                City = "Madrid",
                PostalCode = "28013",
                Country = "España",
                Phone = "900 900 900",
                Fax = "900 900 910"
            };

            // Opcion 2a, utilizando el método Entry y modificando la propiedad State
            context2.Entry(cliente2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context2.SaveChanges();

            // Método 2b, mediante el método UPDATE de la colección
            context2.Customers.Update(cliente2);
            context2.SaveChanges();

            Console.WriteLine("Cliente actualizado correctamente.");
        }

        /// <summary>
        /// Eliminar datos con Entity Framework Core
        /// </summary>
        static void EliminarDatosEF()
        {
            // Declaración de la variable de contexto
            var context = new NorthwindContext();

            /////////////////////////////////////////////////////////////////////////////////
            // Eliminar Datos - DELETE
            // Equivalente a: DELETE Customers WHERE CustomerID = 'BCR01'
            /////////////////////////////////////////////////////////////////////////////////

            // Opcion 1, utilizando el método REMOVE de la colección
            var cliente = context.Customers
                .Where(r => r.CustomerID == "BCR01")
                .FirstOrDefault();

            if (cliente == null) Console.WriteLine("NO existe el cliente.");
            else
            {
                context.Customers.Remove(cliente);
                context.SaveChanges();

                Console.WriteLine("Cliente eliminado correctamente.");
            }


            // Opcion 2, utilizando el método Entry y modificando la propiedad State

            var cliente2 = new Customer() { CustomerID = "BCR01" };

            context.Entry(cliente2).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();

            Console.WriteLine("Cliente eliminado correctamente.");
        }

        /// <summary>
        /// Comandos avanzados con Entity Framework Core
        /// </summary>
        static void ComandosAvanzadosEF()
        {
            // Declaración de la variable de contexto
            var context = new NorthwindContext();

            /********************************************
             *  Sentecias de LINQ que utilizan INCLUDE  * 
             ********************************************/

            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Empleados, nombre, apellidos, número de pedidos gestionado
            /////////////////////////////////////////////////////////////////////////////////

            // Opción 1, mediante subconsultas dentro de un FOREACH
            var empleados = context.Employees
                .Select(r => new { r.EmployeeID, r.FirstName, r.LastName });

            foreach (var item in empleados)
            {
                var pedidosCliente = context.Orders
                    .Where(r => r.EmployeeID == item.EmployeeID);
            }

            // Opción 2, una subconsulta dentro del método SELECT
            var empleados2 = context.Employees
                .Select(r => new {
                    r.EmployeeID,
                    r.FirstName,
                    r.LastName,
                    Pedidos = context.Orders.Where(s => s.EmployeeID == r.EmployeeID)
                });

            // Opción 3a, utilizando INCLUDE (recomendo)
            var empleados3 = context.Employees
                .Include(r => r.Orders)
                .Select(r => r);

            // Opción 3b, utilizando INCLUDE (recomendo)
            var empleados4 = context.Employees
                .Include(r => r.Orders)
                .Select(r => new {
                    r.EmployeeID,
                    r.FirstName,
                    r.LastName,
                    r.Orders
                });

            foreach (var empleado in empleados4)
            {
                Console.WriteLine($"{empleado.FirstName} {empleado.LastName} - {empleado.Orders.Count} pedidos");
            }

            Console.ReadKey();


            /////////////////////////////////////////////////////////////////////////////////
            // Partiendo de Orders -> Listado de pedidos de clientes USA
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Orders WHERE CustomerID IN (SELECT CustomerID FROM dbo.Customers WHERE Country = 'USA')

            var pedidos = context.Orders
                .Include(r => r.Customer)
                .Where(r => r.Customer.Country == "USA");


            /////////////////////////////////////////////////////////////////////////////////
            // Listar productos de las categorías Condiments y Seafood, Opción 1
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE CategoryID IN (SELECT CategoryID FROM dbo.Categories WHERE CategoryName IN ('Condiments', 'Seafood'))

            var productos1 = context.Products
                .Include(r => r.Category)
                .Where(r => (new string[] { "Condiments", "Seafood" }).Contains(r.Category.CategoryName));


            /////////////////////////////////////////////////////////////////////////////////
            // Listar productos de las categorías Condiments y Seafood, Opción 2
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE CategoryID IN (SELECT CategoryID FROM dbo.Categories WHERE CategoryName = 'Condiments' OR CategoryName = 'Seafood')

            var productos2 = context.Products
                .Include(r => r.Category)
                .Where(r => r.Category.CategoryName == "Condiments" || r.Category.CategoryName == "Seafood");



            /**********************************************
             *  Sentecias de LINQ que utilizan INTERSECT  * 
             **********************************************/

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            // Listado de IDs Clientes que ha pedido el producto 57 y el producto 72 en el año 1997
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Ejemplo ejecutando dos consultas por separado y luego utilizando INTERSECT

            var c1 = context.Order_Details
                .Include(r => r.Order)
                .Where(r => r.ProductID == 57)
                .Select(r => r.Order.CustomerID)
                .ToList();


            var c2 = context.Order_Details
                .Include(r => r.Order)
                .Where(r => r.ProductID == 72 && r.Order.OrderDate.Value.Year == 1997)
                .Select(r => r.Order.CustomerID)
                .ToList();

            var c3 = c1.Intersect(c2);


            // El mismo ejemplo que el anterior pero ejecutado todo en una misma consulta

            var customers = context.Order_Details
                .Include(r => r.Order)
                .Where(r => r.ProductID == 57)
                .Select(r => r.Order.CustomerID)
                .Intersect(context.Order_Details
                    .Include(r => r.Order)
                    .Where(r => r.ProductID == 72 && r.Order.OrderDate.Value.Year == 1997)
                    .Select(r => r.Order.CustomerID));

            foreach (var id in customers)
                Console.WriteLine($"{id}");

            Console.ReadKey();



            /********************************************
             *  Sentecias de LINQ que utilizan GROUPBY  * 
             ********************************************/

            /////////////////////////////////////////////////////////////////////////////////
            // Listado de clientes agrupados por país
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT Country, COUNT(*) FROM db.Customers GROUP BY Country

            var clientes = context.Customers
                .AsEnumerable()
                .GroupBy(g => g.Country)        // Key o clave, es el campo por el que agrupamos, en el ejemplo Country
                .Select(g => g)
                .ToList();                      // En cada posición de la lista tenemos un grupo
                                                // Los grupos son colecciones de los elementos de ese grupo, en el ejemplo clientes


            foreach (var grupo in clientes)
            {
                Console.WriteLine($"Clave del Grupo: {grupo.Key}");
                Console.WriteLine($"Elementos del Grupo: {grupo.Count()}");

                foreach (var item in grupo)
                    Console.WriteLine($" -> {item.CustomerID}# {item.CompanyName}");

                Console.WriteLine("");
            }

            Console.ReadKey();


            /////////////////////////////////////////////////////////////////////////////////////////////
            // Listado de pedidos con el importe total, agrupamos las líneas de pedidos por pedido
            /////////////////////////////////////////////////////////////////////////////////////////////

            // SELECT OrderID, SUM(UnitPrice * Quantity) FROM dbo.OrderDetails GROUP BY OrderID

            // Opción 1, con SUM
            var orders = context.Order_Details
                .AsEnumerable()
                .GroupBy(g => g.OrderID)
                .Select(g => new { OrderID = g.Key, Total = g.Sum(r => r.UnitPrice * r.Quantity) })
                .ToList();

            // Opción 2, con SELECT
            var orders2 = context.Order_Details
                .AsEnumerable()
                .GroupBy(g => g.OrderID)
                .Select(g => new { OrderID = g.Key, Total = g.Select(r => r.UnitPrice * r.Quantity).Sum() })
                .ToList();

            foreach (var item in orders2)
                Console.WriteLine($"{item.OrderID.ToString().PadLeft(6, ' ')} - {item.Total.ToString("N2").PadLeft(10, ' ')}");

            Console.ReadKey();




            /***********************************************
             *  Sentecias de LINQ que utilizan FROMSQLRAW  * 
             ***********************************************/

            // Permite escribir directamente las sentencias Trnsact-SQL 

            var data = context.Customers.FromSqlRaw("SELECT * FROM dbo.Customers WHERE Country = 'USA'");

            foreach (var item in data)
                Console.WriteLine($" -> {item.CustomerID}# {item.CompanyName}");

            Console.ReadKey();
        }
    }
}
