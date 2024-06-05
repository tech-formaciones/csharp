using Formacion.CSharp.Ejercicios.ConsoleAppEF.Models;

namespace Formacion.CSharp.Ejercicios.ConsoleAppEF
{
    /// <summary>
    /// Inicio de la aplicación
    /// </summary>
    internal class Program
    {
        static NorthwindContext context;

        static void Main(string[] args)
        {
            context = new NorthwindContext();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("".PadRight(81, '*'));
                Console.WriteLine("*  EJERCICIOS CON EF + LINQ".PadRight(80) + "*");
                Console.WriteLine("".PadRight(81, '*'));
                Console.WriteLine("*".PadRight(80) + "*");
                Console.WriteLine("*  1. Listado de Clientes que residen en USA".PadRight(80) + "*");
                Console.WriteLine("*  2. Listado de Proveedores (Suppliers) de Berlin".PadRight(80) + "*");
                Console.WriteLine("*  3. Listado de Empleados con identificadores 3, 5 y 8".PadRight(80) + "*");
                Console.WriteLine("*  4. Listado de Productos con stock mayor de cero".PadRight(80) + "*");
                Console.WriteLine("*  5. Listado de Productos con stock mayor de cero de los proveedores con id. 1, 3 y 5".PadRight(80) + "*");
                Console.WriteLine("*  6. Listado de Productos con precio mayor de 20 y menor 90".PadRight(80) + "*");
                Console.WriteLine("*  7. Listado de Pedidos entre 01/01/1997 y 15/07/1997".PadRight(80) + "*");
                Console.WriteLine("*  8. Listado de Pedidos registrados por los empleados con identificador 1, 3, 4 y 8 en 1997".PadRight(80) + "*");
                Console.WriteLine("*  9. Listado de Pedidos de abril de 1996".PadRight(80) + "*");
                Console.WriteLine("* 10. Listado de Pedidos del realizado los dia uno de cada mes del año 1998".PadRight(80) + "*");
                Console.WriteLine("* 11. Listado de Clientes que no tiene fax".PadRight(80) + "*");
                Console.WriteLine("* 12. Listado de los 10 productos más baratos".PadRight(80) + "*");
                Console.WriteLine("* 13. Listado de los 10 productos más caros con stock".PadRight(80) + "*");
                Console.WriteLine("* 14. Listado de Cliente de UK y nombre de empresa que comienza por B".PadRight(80) + "*");
                Console.WriteLine("* 15. Listado de Productos de identificador de categoria 3 y 5".PadRight(80) + "*");
                Console.WriteLine("* 16. Importe total del stock".PadRight(80) + "*");
                Console.WriteLine("* 17. Listado de Pedidos de los clientes de Argentina".PadRight(80) + "*");
                Console.WriteLine("* 99. Salir".PadRight(80) + "*");
                Console.WriteLine("*".PadRight(80) + "*");
                Console.WriteLine("".PadRight(81, '*'));

                Console.WriteLine(Environment.NewLine);
                Console.Write("   Opción: ");

                Console.ForegroundColor = ConsoleColor.Cyan;

                int.TryParse(Console.ReadLine(), out int opcion);
                switch (opcion)
                {
                    case 1:
                        Ejercicio1();
                        break;
                    case 2:
                        Ejercicio2();
                        break;
                    case 3:
                        Ejercicio3();
                        break;
                    case 4:
                        Ejercicio4();
                        break;
                    case 5:
                        Ejercicio5();
                        break;
                    case 6:
                        Ejercicio6();
                        break;
                    case 7:
                        Ejercicio7();
                        break;
                    case 8:
                        Ejercicio8();
                        break;
                    case 9:
                        Ejercicio9();
                        break;
                    case 10:
                        Ejercicio10();
                        break;
                    case 11:
                        Ejercicio11();
                        break;
                    case 12:
                        Ejercicio12();
                        break;
                    case 13:
                        Ejercicio13();
                        break;
                    case 14:
                        Ejercicio14();
                        break;
                    case 15:
                        Ejercicio15();
                        break;
                    case 16:
                        Ejercicio16();
                        break;
                    case 17:
                        Ejercicio17();
                        break;
                    case 99:
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
        /// Listado de Clientes que residen en USA
        /// </summary>
        static void Ejercicio1()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Clientes que residen en USA
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Customers WHERE Country = 'USA'

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * FROM dbo.Customers WHERE Country = 'USA'");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Customers.Where(r => r.Country == "USA"))
                Console.WriteLine($"{item.CustomerID.PadLeft(5, ' ')}# " +
                    $"{item.CompanyName.PadRight(40, ' ')} " +
                    $"{item.City} ({item.Country})");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de Proveedores (Suppliers) de Berlin
        /// </summary>
        static void Ejercicio2()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Proveedores (Suppliers) de Berlin
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Suppliers WHERE City = 'Berlin'

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * FROM dbo.Suppliers WHERE City = 'Berlin'");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Suppliers.Where(r => r.City == "Berlin"))
                Console.WriteLine($"{item.SupplierID.ToString().PadLeft(5, ' ')}# " +
                    $"{item.CompanyName.PadRight(20, ' ')} " +
                    $"{item.City} ({item.Country})");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de Empleados con identificadores 3, 5 y 8
        /// </summary>
        static void Ejercicio3()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Empleados con identificadores 3, 5 y 8
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Employees WHERE EmployeeID IN (3, 5, 8)

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * FROM dbo.Employees WHERE EmployeeID IN (3, 5, 8)");
            Console.WriteLine("=====================================================================================");

            var ids = new List<int> { 3, 5, 8 };
            var r1 = context.Employees.Where(r => ids.Contains(r.EmployeeID));


            // SELECT * FROM dbo.Employees WHERE EmployeeID == 3 OR EmployeeID == 5 OR EmployeeID == 8
            var r2 = context.Employees.Where(r => r.EmployeeID == 3 || r.EmployeeID == 5 || r.EmployeeID == 8);


            foreach (var item in context.Employees.Where(r => (new int[] { 3, 5, 8 }).Contains(r.EmployeeID)))
                Console.WriteLine($"{item.EmployeeID.ToString().PadLeft(5, ' ')}# {(item.FirstName + " " + item.LastName).PadRight(30, ' ')} {item.City} ({item.Country})");

            Console.ReadKey();

        }

        /// <summary>
        /// Listado de Productos con stock mayor de cero
        /// </summary>
        static void Ejercicio4()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Productos con stock mayor de cero
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE UnitsInStock > 0

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * FROM dbo.Products WHERE UnitsInStock > 0");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Products.Where(r => r.UnitsInStock > 0))
                Console.WriteLine($"{item.ProductID.ToString().PadLeft(5, ' ')}# " +
                    $"{item.ProductName.PadRight(40, ' ')} " +
                    $"{(item.UnitsInStock.HasValue ? item.UnitsInStock.Value.ToString("N0") : "0").PadLeft(3, ' ')} " +
                    $"{(item.UnitPrice.HasValue ? item.UnitPrice.Value.ToString("N2") : "0").PadLeft(7, ' ')} Euros");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de Productos con stock mayor de cero de los proveedores con identificadores 1, 3 y 5
        /// </summary>
        static void Ejercicio5()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////
            // Listado de Productos con stock mayor de cero de los proveedores con identificadores 1, 3 y 5
            /////////////////////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE SupplierID IN (1, 3, 5) 

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * FROM dbo.Products WHERE SupplierID IN (1, 3, 5)");
            Console.WriteLine("=====================================================================================");

            var r4 = context.Products
                .Where(r => r.UnitsInStock > 0 && (r.SupplierID == 1 || r.SupplierID == 3 || r.SupplierID == 5));

            foreach (var item in r4)
                Console.WriteLine($"{item.ProductID.ToString().PadLeft(5, ' ')}# " +
                    $"{item.ProductName.PadRight(40, ' ')} " +
                    $"{(item.UnitsInStock.HasValue ? item.UnitsInStock.Value.ToString("N0") : "0").PadLeft(3, ' ')} " +
                    $"{(item.UnitPrice.HasValue ? item.UnitPrice.Value.ToString("N2") : "0").PadLeft(7, ' ')} Euros");

            Console.ReadKey();

            var r3 = context.Products
                .Where(r => r.UnitsInStock > 0 &&
                (new int[] { 1, 5, 6 }).Contains(r.SupplierID.HasValue ? r.SupplierID.Value : -1));

            foreach (var item in context.Products.Where(r => r.UnitsInStock > 0 && (new int[] { 1, 5, 6 }).Contains(r.SupplierID.HasValue ? r.SupplierID.Value : -1)))
                Console.WriteLine($"{item.ProductID.ToString().PadLeft(5, ' ')}# " +
                    $"{item.ProductName.PadRight(40, ' ')} " +
                    $"{(item.UnitsInStock.HasValue ? item.UnitsInStock.Value.ToString("N0") : "0").PadLeft(3, ' ')} " +
                    $"{(item.UnitPrice.HasValue ? item.UnitPrice.Value.ToString("N2") : "0").PadLeft(7, ' ')} Euros");

            foreach (var item in context.Products.Where(r => r.UnitsInStock > 0 && (new int[] { 1, 5, 6 }).Contains(r.SupplierID ?? -1)))
                Console.WriteLine($"{item.ProductID.ToString().PadLeft(5, ' ')}# " +
                    $"{item.ProductName.PadRight(40, ' ')} " +
                    $"{(item.UnitsInStock.HasValue ? item.UnitsInStock.Value.ToString("N0") : "0").PadLeft(3, ' ')} " +
                    $"{(item.UnitPrice.HasValue ? item.UnitPrice.Value.ToString("N2") : "0").PadLeft(7, ' ')} Euros");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de Productos con precio mayor de 20 y menor 90
        /// </summary>
        static void Ejercicio6()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Productos con precio mayor de 20 y menor 90
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE UnitPrice > 20 AND UnitPrice < 90

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * FROM dbo.Products WHERE UnitPrice > 20 AND UnitPrice < 90");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Products.Where(r => r.UnitPrice > 0 && r.UnitPrice < 90))
                Console.WriteLine($"{item.ProductID.ToString().PadLeft(5, ' ')}# " +
                    $"{item.ProductName.PadRight(40, ' ')} " +
                    $"{(item.UnitsInStock.HasValue ? item.UnitsInStock.Value.ToString("N0") : "0").PadLeft(3, ' ')} " +
                    $"{(item.UnitPrice.HasValue ? item.UnitPrice.Value.ToString("N2") : "0").PadLeft(7, ' ')} Euros");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de Pedidos entre 01/01/1997 y 15/07/1997
        /// </summary>
        static void Ejercicio7()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos entre 01/01/1997 y 15/07/1997
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE OrderDate >= '1997/01/01' AND OrderDate <= '1997/09/15'

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * dbo.Orders WHERE OrderDate >= '1997/01/01' AND OrderDate <= '1997/09/15'");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Orders.Where(r => r.OrderDate.Value >= new DateTime(1997, 1, 1) && r.OrderDate.Value <= new DateTime(1997, 9, 15)))
                Console.WriteLine($"{item.OrderID.ToString().PadLeft(8, ' ')}# " +
                    $"{item.OrderDate.ToString().PadRight(12, ' ')} ");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de Pedidos registrados por los empleados con identificador 1, 3, 4 y 8 en 1997
        /// </summary>
        static void Ejercicio8()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos registrados por los empleados con identificador 1, 3, 4 y 8 en 1997
            /////////////////////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1997 AND EmployeeID IN (1, 3, 4, 8)

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1997 AND EmployeeID IN (1, 3, 4, 8)");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Orders.Where(r => r.OrderDate.Value.Year == 1997 && (new int?[] { 1, 3, 4, 8 }).Contains(r.EmployeeID)))
                Console.WriteLine($"{item.OrderID.ToString().PadLeft(8, ' ')}# " +
                    $"{item.OrderDate.ToString().PadRight(12, ' ')} " +
                    $"- Empleado: {item.EmployeeID.ToString().PadRight(8, ' ')} ");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de Pedidos de abril de 1996
        /// </summary>
        static void Ejercicio9()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos de abril de 1996
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1996 AND MONTH(OrderDate) = 10

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1996 AND MONTH(OrderDate) = 10");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Orders.Where(r => r.OrderDate.Value.Year == 1997 && r.OrderDate.Value.Month == 10))
                Console.WriteLine($"{item.OrderID.ToString().PadLeft(8, ' ')}# " +
                    $"{item.OrderDate.ToString().PadRight(12, ' ')} ");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de Pedidos del realizado los dia uno de cada mes del año 1998
        /// </summary>
        static void Ejercicio10()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos del realizado los dia uno de cada mes del año 1998
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1998 AND DAY(OrderDate) = 1

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1998 AND DAY(OrderDate) = 1");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Orders.Where(r => r.OrderDate.Value.Year == 1998 && r.OrderDate.Value.Day == 1))
                Console.WriteLine($"{item.OrderID.ToString().PadLeft(8, ' ')}# " +
                    $"{item.OrderDate.ToString().PadRight(12, ' ')} ");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de Clientes que no tiene fax
        /// </summary>
        static void Ejercicio11()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Clientes que no tiene fax
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Customers WHERE Fax = NULL

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * FROM dbo.Customers WHERE Fax = NULL");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Customers.Where(r => r.Fax == null))
                Console.WriteLine($"{item.CustomerID.PadLeft(5, ' ')}# " +
                    $"{item.CompanyName.PadRight(40, ' ')} " +
                    $"{item.City} ({item.Country}) " +
                    $"- Fax: {item.Fax}");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de los 10 productos más baratos
        /// </summary>
        static void Ejercicio12()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de los 10 productos más baratos
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products ORDER BY UnitPrice

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT TOP(10) * FROM dbo.Products ORDER BY UnitPrice");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Products.OrderBy(r => r.UnitPrice).Take(10))
                Console.WriteLine($"{item.ProductID.ToString().PadLeft(5, ' ')}# " +
                    $"{item.ProductName.PadRight(40, ' ')} " +
                    $"{(item.UnitsInStock.HasValue ? item.UnitsInStock.Value.ToString("N0") : "0").PadLeft(3, ' ')} " +
                    $"{(item.UnitPrice.HasValue ? item.UnitPrice.Value.ToString("N2") : "0").PadLeft(7, ' ')} Euros");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de los 10 productos más caros con stock
        /// </summary>
        static void Ejercicio13()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de los 10 productos más caros con stock
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products ORDER BY UnitPrice DESC

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT TOP(10) * FROM dbo.Products ORDER BY UnitPrice DESC");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Products.Where(r => r.UnitsInStock > 0).OrderByDescending(r => r.UnitPrice).Take(10))
                Console.WriteLine($"{item.ProductID.ToString().PadLeft(5, ' ')}# " +
                    $"{item.ProductName.PadRight(40, ' ')} " +
                    $"{(item.UnitsInStock.HasValue ? item.UnitsInStock.Value.ToString("N0") : "0").PadLeft(3, ' ')} " +
                    $"{(item.UnitPrice.HasValue ? item.UnitPrice.Value.ToString("N2") : "0").PadLeft(7, ' ')} Euros");

            Console.ReadKey();
        }

        /// <summary>
        /// Listado de Cliente de UK y nombre de empresa que comienza por B
        /// </summary>
        static void Ejercicio14()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Cliente de UK y nombre de empresa que comienza por B
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Customers WHERE CompanyName LIKE 'B%' AND Country = 'UK'
        }

        /// <summary>
        /// Listado de Productos de identificador de categoria 3 y 5
        /// </summary>
        static void Ejercicio15()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Productos de identificador de categoria 3 y 5
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE CategoryID IN (3, 5)

            Console.Clear();
            Console.WriteLine("=====================================================================================");
            Console.WriteLine(" SELECT * FROM dbo.Products WHERE CategoryID IN (3, 5)");
            Console.WriteLine("=====================================================================================");

            foreach (var item in context.Products.Where(r => (new int?[] {3, 5}).Contains(r.CategoryID)))
                Console.WriteLine($"{item.ProductID.ToString().PadLeft(5, ' ')}# " +
                    $"{item.ProductName.PadRight(40, ' ')} " +
                    $"- Categoria: {item.CategoryID.ToString().PadLeft(3, ' ')} " +
                    $"- Stock: {(item.UnitsInStock.HasValue ? item.UnitsInStock.Value.ToString("N0") : "0").PadLeft(3, ' ')} " +
                    $"{(item.UnitPrice.HasValue ? item.UnitPrice.Value.ToString("N2") : "0").PadLeft(7, ' ')} Euros");

            Console.ReadKey();
        }

        /// <summary>
        /// Importe total del stock
        /// </summary>
        static void Ejercicio16()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Importe total del stock
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT SUM(UnitInStock * UnitPrice) FROM Products

            Console.WriteLine($"Importe del Stock: {context.Products.Sum(r => r.UnitPrice * r.UnitsInStock)}");
        }

        /// <summary>
        /// Listado de Pedidos de los clientes de Argentina
        /// </summary>
        static void Ejercicio17()
        {
            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos de los clientes de Argentina
            /////////////////////////////////////////////////////////////////////////////////            

            // SELECT CustomerID FROM dbo.Customers WHERE Country = 'Argentina'
            // SELECT * FROM dbo.Orders WHERE CustomerID IN ('CACTU', 'OCEAN', 'RANCH')

            var argentinaIds = context.Customers
                .Where(r => r.Country == "Argentina")
                .Select(r => r.CustomerID);

            var pedidos = context.Orders
                .Where(r => argentinaIds.Contains(r.CustomerID))
                .Select(r => r);


            // SELECT * FROM dbo.Orders WHERE CustomerID IN (SELECT CustomerID FROM dbo.Customers WHERE Country = 'Argentina')

            var pedidos2 = context.Orders
                .Where(s => context.Customers
                    .Where(r => r.Country == "Argentina")
                    .Select(r => r.CustomerID)
                    .ToList().Contains(s.CustomerID))
                .ToList();
        }

        static void Fechas()
        {
            DateTime fecha = DateTime.Now;
            DateTime? fecha2 = DateTime.Now;

            fecha = (DateTime)fecha2;
            fecha = Convert.ToDateTime(fecha2);

            int año = fecha.Year;
            int año2 = fecha2.GetValueOrDefault().Year;
            int año4 = fecha2.Value.Year;

            int año3 = fecha2.HasValue == true ? fecha2.GetValueOrDefault().Year : -1;

            ///////////////////////////////////////////////////////////////////////////

            int num = 0;
            int? num2 = 0;

            //num = null;
            num2 = null;

            num = (int)num2;
            num = Convert.ToInt32(num2);

            num = num2.HasValue ? num2.Value : -1;

        }
    }
}

