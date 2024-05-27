namespace Formacion.CSharp.ConsoleApp4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsultasBasicas();

            DateTime fecha = DateTime.Now;
            DateTime fecha2 = new DateTime(1990, 5, 27);            // 27-05-1990  0:00:00
            DateTime fecha3 = new DateTime(1990, 5, 27, 10, 10, 0); // 27-05-1990 10:10:00

            fecha2.AddDays(-4);  // 23-05-1990  0:00:00
            fecha2.AddDays(4);   // 01-06-1990  0:00:00

            var resta = fecha - fecha2;
            var resta2 = fecha.Subtract(fecha2);

            Console.WriteLine(fecha2.ToString());
            Console.WriteLine(fecha2.ToLongDateString());
            Console.WriteLine(fecha2.Year);

        }

        /// <summary>
        /// Consultas básicas de LINQ con DataLists
        /// </summary>
        static void ConsultasBasicas()
        {
            // Transat-SQL -> SELECT * FROM ListaProductos
            // Listado de Productos completo

            // Métodos de LINQ
            var productos1a = DataLists.ListaProductos
                .ToList();

            // Expresiones de LINQ
            var productos1b = from r in DataLists.ListaProductos
                              select r;

            ////////////////////////////////////////////////////////////////////////////////////////

            // Transat-SQL -> SELECT * FROM ListaProductos WHERE precio > 2
            // Listado de Productos con precio mayor de 2

            // Métodos de LINQ
            var productos2a = DataLists.ListaProductos
                .Where(r => r.Precio > 2)
                .ToList();

            // Expresiones de LINQ
            var productos2b = from r in DataLists.ListaProductos
                              where r.Precio > 2
                              select r;

            ////////////////////////////////////////////////////////////////////////////////////////

            // Transat-SQL -> SELECT * FROM ListaProductos WHERE precio > 2 ORDER BY descripcion DESC
            // Listado de Productos con precio mayor de 2 ordenados por descripción DESC

            // Métodos de LINQ
            var productos3a = DataLists.ListaProductos
                .Where(r => r.Precio > 2)
                .OrderByDescending(r => r.Descripcion)
                .ToList();

            // Expresiones de LINQ
            var productos3b = from r in DataLists.ListaProductos
                              where r.Precio > 2
                              orderby r.Descripcion descending
                              select r;

            ////////////////////////////////////////////////////////////////////////////////////////

            // Transat-SQL -> SELECT Descripcion, Precio FROM ListaProductos WHERE precio > 2 ORDER BY descripcion DESC
            // Listado de Descripción y Precio para productos con precio mayor de 2,5 ordenados por precio ASC

            // Métodos de LINQ
            var productos4a = DataLists.ListaProductos
                .Where(r => r.Precio > 2.5)
                .OrderBy(r => r.Precio)
                .Select(r => new { r.Descripcion, r.Precio })
                .ToList();

            // Expresiones de LINQ
            var productos4b = from r in DataLists.ListaProductos
                              where r.Precio > 2.5
                              orderby r.Precio
                              select new { r.Descripcion, r.Precio };

            ////////////////////////////////////////////////////////////////////////////////////////

            // Transat-SQL -> SELECT id AS Code, descripcion FROM ListaProductos
            // Listado de Id y Descripción de Productos

            // Métodos de LINQ
            var productos5a = DataLists.ListaProductos
                .Select(r => new { Code = r.Id, r.Descripcion })
                .ToList();

            // Expresiones de LINQ
            var productos5b = from r in DataLists.ListaProductos
                              select new { Code = r.Id, r.Descripcion };

            ////////////////////////////////////////////////////////////////////////////////////////

            // Transat-SQL -> SELECT descripcion FROM ListaProductos
            // Listado de Descripción de Productos

            // Métodos de LINQ
            var productos6a = DataLists.ListaProductos
                .Select(r => r.Descripcion)
                .ToList();

            // Expresiones de LINQ
            var productos6b = from r in DataLists.ListaProductos
                              select r.Descripcion;

            ////////////////////////////////////////////////////////////////////////////////////////

            // Ordena en la base de datos
            var order1a = DataLists.ListaProductos
                .Where(r => r.Precio > 0.90 && r.Precio < 2)
                .OrderBy(r => r.Descripcion)
                .Select(r => r.Descripcion);

            // Ordena en la base de datos
            var order1b = from r in DataLists.ListaProductos
                          where r.Precio > 0.90 && r.Precio < 2
                          orderby r.Descripcion
                          select r.Descripcion;

            // Ordena en el ordenador, puede ofrecer menos rendimiento
            var order2a = DataLists.ListaProductos
                .Where(r => r.Precio > 0.90 && r.Precio < 2)                
                .Select(r => r.Descripcion)
                .OrderBy(r => r);

            // Ordena en el ordenador, puede ofrecer menos rendimiento
            var order2b = (from r in DataLists.ListaProductos
                          where r.Precio > 0.90 && r.Precio < 2                          
                          select r.Descripcion).ToList().OrderBy(r => r);

            ////////////////////////////////////////////////////////////////////////////////////////

            // Contains -> Contiene; 
            // Transat-SQL -> SELECT * FROM ListaProductos  WHERE descripcion LIKE '%es%'

            var w1a = DataLists.ListaProductos
                .Where(r => r.Descripcion.Contains("es"))
                .Select(r => r)
                .ToList();

            var w1b = from r in DataLists.ListaProductos
                      where r.Descripcion.Contains("es")
                      select r.Descripcion;


            // StartsWith-> Comienza; 
            // Transat-SQL -> SELECT * FROM ListaProductos  WHERE descripcion LIKE 'es%'

            var w2a = DataLists.ListaProductos
                .Where(r => r.Descripcion.StartsWith("es"))
                .Select(r => r)
                .ToList();

            var w2b = from r in DataLists.ListaProductos
                      where r.Descripcion.StartsWith("es")
                      select r.Descripcion;


            // EndsWith-> Finaliza
            // Transat-SQL -> SELECT * FROM ListaProductos  WHERE descripcion LIKE '%es'

            var w3a = DataLists.ListaProductos
                .Where(r => r.Descripcion.EndsWith("es"))
                .Select(r => r)
                .ToList();

            var w3b = from r in DataLists.ListaProductos
                      where r.Descripcion.EndsWith("es")
                      select r.Descripcion;


            foreach (var producto in w1a) 
                Console.WriteLine($"{producto.Descripcion} - Precio: {producto.Precio.ToString("N2")}");

        }

        /// <summary>
        /// Clientes mayores de 40 años
        /// </summary>
        static void Ejercicio1()
        { }

        /// <summary>
        /// Productos que comiencen por C ordenados por precio
        /// </summary>
        static void Ejercicio2()
        { }

        /// <summary>
        /// Listar un detalle de todos los pedidos
        /// </summary>
        static void Ejercicio3()
        { }

        /// <summary>
        /// Mostrar el importe total de un pedido
        /// </summary>
        static void Ejercicio4()
        { }

        /// <summary>
        /// Mostrar los pedidos con lapiceros
        /// </summary>
        static void Ejercicio5()
        { }

        /// <summary>
        /// Número de pedidos con cuaderno grande
        /// </summary>
        static void Ejercicio6()
        { }

        /// <summary>
        /// Unidades vendidas de cuaderno pequeño
        /// </summary>
        static void Ejercicio7()
        { }

    }
}
