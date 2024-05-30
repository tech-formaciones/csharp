using Formacion.CSharp.Ejercicios.ConsoleAppEF.Models;

namespace Formacion.CSharp.Ejercicios.ConsoleAppEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NorthwindContext context = new NorthwindContext();


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


            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos entre 01/01/1997 y 15/07/1997
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE OrderDate >= '1997/01/01' AND OrderDate <= '1997/09/15'



            /////////////////////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos registrados por los empleados con identificador 1, 3, 4 y 8 en 1997
            /////////////////////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1997 AND EmployeeID IN (1, 3, 4, 8)



            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos de abril de 1996
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1996 AND MONTH(OrderDate) = 10



            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Pedidos del realizado los dia uno de cada mes del año 1998
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1998 AND DAY(OrderDate) = 1



            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Clientes que no tiene fax
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Customers WHERE Fax = NULL



            /////////////////////////////////////////////////////////////////////////////////
            // Listado de los 10 productos más baratos
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products ORDER BY UnitPrice



            /////////////////////////////////////////////////////////////////////////////////
            // Listado de los 10 productos más caros con stock
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products ORDER BY UnitPrice DESC



            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Cliente de UK y nombre de empresa que comienza por B
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Customers WHERE CompanyName LIKE 'B%' AND Country = 'Uk'



            /////////////////////////////////////////////////////////////////////////////////
            // Listado de Productos de identificador de categoria 3 y 5
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products WHERE CategoryID IN (3, 5)



            /////////////////////////////////////////////////////////////////////////////////
            // Importe total del stock
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT SUM(UnitInStock * UnitPrice) FROM Products



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
