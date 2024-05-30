﻿using Formacion.CSharp.ConsoleAppEF.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;

namespace Formacion.CSharp.ConsoleAppEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ConsultaConADONET();
            //ConsultaConEF();
            //InsertarDatosEF();
            //ActualizarDatosEF();
            //EliminarDatosEF();
            SentenciasAvanzadas();
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

        /// <summary>
        /// Ejecutamos consultas de datos con Entity Framework Core
        /// </summary>
        static void ConsultaConEF()
        {
            // SELECT * FROM dbo.Customers

            var context = new NorthwindContext();

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

        static void InsertarDatosEF()
        {
            var context = new NorthwindContext();

            var cliente = new Customer()
            {
                CustomerID = "BCR01",
                CompanyName = "Empresa Uno, SL",
                ContactName = "Borja Cabeza",
                ContactTitle = "Generent",
                Address = "Calle Paraiso, 33",
                Region = "Madrid",
                City = "Madrid",
                PostalCode = "28013",
                Country = "España",
                Phone = "900 900 900",
                Fax = "900 900 910"
            };

            // Método 1
            // context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Added;

            // Método 2
            context.Customers.Add(cliente);
            context.SaveChanges();

            Console.WriteLine("Registro insertado correctamente.");
        }
        
        static void ActualizarDatosEF()
        {
            // Opcion A
            // Recuperamos el cliente de la base de datos, modificamos valores de las propiedades
            // y grabamos los cambios.

            var context = new NorthwindContext();

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

            // Opcion B
            // Instanciamos un objeto que representa un registro existen en la base de datos, pero
            // con valores diferentes y lo utilizamos para actualizar.

            var context2 = new NorthwindContext();

            var cliente2 = new Customer()
            {
                CustomerID = "BCR01",
                CompanyName = "Empresa Uno, SL",
                ContactName = "Borja Cabeza",
                ContactTitle = "CEO",
                Address = "Avenida Paraiso, 33",
                Region = "Madrid",
                City = "Madrid",
                PostalCode = "28013",
                Country = "España",
                Phone = "900 900 900",
                Fax = "900 900 910"
            };

            // Método 1
            //context2.Entry(cliente2).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //context2.SaveChanges();

            // Método 2
            context2.Customers.Update(cliente2);
            context2.SaveChanges();

            Console.WriteLine("Cliente actualizado correctamente.");
        }

        static void EliminarDatosEF()
        {
            var context = new NorthwindContext();

            // Opcion A
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


            // Opcion B

            var cliente2 = new Customer() { CustomerID = "BCR01" };

            context.Entry(cliente2).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChanges();

            Console.WriteLine("Cliente eliminado correctamente.");
        }

        static void SentenciasAvanzadas()
        {
            var context = new NorthwindContext();


            //USESQLSERVER
            var data = context.Customers.FromSqlRaw("SELECT * FROM dbo.Customers WHERE Country = 'USA'");

            foreach (var item in data)
                Console.WriteLine($" -> {item.CustomerID}# {item.CompanyName}");

            Console.ReadKey();

            // GROUPBY

            // SELECT OrderID, SUM(UnitPrice * Quantity) FROM dbo.OrderDetails GROUP BY OrderID

            var orders = context.Order_Details
                .AsEnumerable()
                .GroupBy(g => g.OrderID)
                .Select(g => new { OrderID = g.Key, Total = g.Sum(r => r.UnitPrice * r.Quantity) })
                .ToList();

            var orders2 = context.Order_Details
                .AsEnumerable()
                .GroupBy(g => g.OrderID)
                .Select(g => new { OrderID = g.Key, Total = g.Select(r => r.UnitPrice * r.Quantity).Sum() })
                .ToList();

            foreach (var item in orders2)
                Console.WriteLine($"{item.OrderID.ToString().PadLeft(6, ' ')} - {item.Total.ToString("N2").PadLeft(10, ' ')}");

            Console.ReadKey();

            // SELECT Country, COUNT(*) FROM db.Customers GROUP BY Country

            var clientes = context.Customers
                .AsEnumerable()
                .GroupBy(g => g.Country)        // La Key es el campo por el que agrupamos
                .Select(g => g)
                .ToList();                      // En cada posición de la lista tenemos un grupo
                                                // Los grupos son colecciones de los elementos de ese grupo

            foreach (var grupo in clientes)
            {
                Console.WriteLine($"Clave del Grupo: {grupo.Key}");
                Console.WriteLine($"Elementos del Grupo: {grupo.Count()}");

                foreach (var item in grupo)
                    Console.WriteLine($" -> {item.CustomerID}# {item.CompanyName}");

                Console.WriteLine("");
            }

            Console.ReadKey();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////


            // INTERSECT

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


            // Clientes que ha pedido el producto 57 y el producto 72 en el año 1997

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


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////



            // Partiendo de Orders->Listado de pedidos de clientes USA
            // SELECT * FROM dbo.Orders WHERE CustomerID IN (SELECT CustomerID FROM dbo.Customers WHERE Country = 'USA')

            var pedidos = context.Orders
                .Include(r => r.Customer)
                .Where(r => r.Customer.Country == "USA");


            // Listar productos de las categorías Condiments y Seafood
            // SELECT * FROM dbo.Products WHERE CategoryID IN (SELECT CategoryID FROM dbo.Categories WHERE CategoryName IN ('Condiments', 'Seafood'))

            var productos1 = context.Products
                .Include(r => r.Category)
                .Where(r => (new string[] { "Condiments", "Seafood" }).Contains(r.Category.CategoryName));

            // SELECT * FROM dbo.Products WHERE CategoryID IN (SELECT CategoryID FROM dbo.Categories WHERE CategoryName = 'Condiments' OR CategoryName = 'Seafood')

            var productos2 = context.Products
                .Include(r => r.Category)
                .Where(r => r.Category.CategoryName == "Condiments" || r.Category.CategoryName == "Seafood");




            // INCLUDE

            // Listado de Empleados (nombre y apellidos) y listado de pedidos gestionados

            // Opción A
            var empleados = context.Employees
                .Select(r => new { r.EmployeeID, r.FirstName, r.LastName });

            foreach (var item in empleados)
            {
                var pedidosCliente = context.Orders
                    .Where(r => r.EmployeeID == item.EmployeeID);
            }

            // Opción B
            var empleados2 = context.Employees
                .Select(r => new { 
                    r.EmployeeID, 
                    r.FirstName, 
                    r.LastName,
                    Pedidos = context.Orders.Where(s => s.EmployeeID == r.EmployeeID)
            });

            // Opción C con INCLUDE
            var empleados3 = context.Employees
                .Include(r => r.Orders)
                .Select(r => r);

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


            var clientes33 = context.Customers
                .Include(r => r.Orders)
                .ToList();

            foreach(var c in clientes33)
            {
                Console.WriteLine(c.CompanyName);

                foreach (var p in c.Orders)
                {
                    Console.Write($"{p.OrderID} -- ");
                }
                Console.WriteLine("");
            }
        }
    }
}
