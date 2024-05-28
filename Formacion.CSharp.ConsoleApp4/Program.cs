using System.Linq;

namespace Formacion.CSharp.ConsoleApp4
{
    internal class Program
    {
        /// <summary>
        ///  Método Main inicio del programa
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true)

            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("".PadRight(56, '*'));
                Console.WriteLine("*  DEMO Y EJERCICIOS".PadRight(55) + "*");
                Console.WriteLine("".PadRight(56, '*'));
                Console.WriteLine("*".PadRight(55) + "*");
                Console.WriteLine("*  1. Consultas Básicas".PadRight(55) + "*");
                Console.WriteLine("*  2. Consultas Avanzadas".PadRight(55) + "*");
                Console.WriteLine("*  3. Cliente mayores de 40 años".PadRight(55) + "*");
                Console.WriteLine("*  4. Productos que comienza C ordenador por precio".PadRight(55) + "*");
                Console.WriteLine("*  5. Listar detalle de un pedido".PadRight(55) + "*");
                Console.WriteLine("*  6. Listar detalle de un pedido (con subconsultas)".PadRight(55) + "*");
                Console.WriteLine("*  7. Mostrar el importe total de un pedido".PadRight(55) + "*");
                Console.WriteLine("*  8. Mostrar pedidos con Lapicero".PadRight(55) + "*");
                Console.WriteLine("*  9. Número de pedidos con Cuaderno Grande".PadRight(55) + "*");
                Console.WriteLine("* 10. Unidades vendidas de Cuaderno Pequeño".PadRight(55) + "*");
                Console.WriteLine("* 11. El pedido con más unidades".PadRight(55) + "*");
                Console.WriteLine("* 12. Listado de pedidos ordernados por fecha ".PadRight(55) + "*");
                Console.WriteLine("* 90. Salir".PadRight(55) + "*");
                Console.WriteLine("*".PadRight(55) + "*");
                Console.WriteLine("".PadRight(56, '*'));

                Console.WriteLine(Environment.NewLine);
                Console.Write("   Opción: ");

                Console.ForegroundColor = ConsoleColor.Cyan;

                int.TryParse(Console.ReadLine(), out int opcion);
                switch (opcion)
                {
                    case 1:
                        ConsultasBasicas();
                        break;
                    case 2:
                        ConsultasAvanzadas();
                        break;
                    case 3:
                        Ejercicio1();
                        break;
                    case 4:
                        Ejercicio2();
                        break;
                    case 5:
                        Ejercicio3();
                        break;
                    case 6:
                        Ejercicio3B();
                        break;
                    case 7:
                        Ejercicio4B();
                        break;
                    case 8:
                        Ejercicio5();
                        break;
                    case 9:
                        Ejercicio6();
                        break;
                    case 10:
                        Ejercicio7();
                        break;
                    case 11:
                        Ejercicio8();
                        break;
                    case 12:
                        Ejercicio9();
                        break;
                    case 90:
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

        static void Fechas()
        {

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
        }

        /// <summary>
        /// Consultas avanzadas de LINQ con DataLists
        /// </summary>
        static void ConsultasAvanzadas()
        {
            ////////////////////////////////////
            // CONTIENE, COMIENZA Y FINALIZA
            ////////////////////////////////////

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


            ////////////////////////////////////
            // AGREGACIÓN
            ////////////////////////////////////

            // Count()          -> Cuenta el número de elementos
            // Distinct()       -> Retorna valores distintos, eliminando los repetidos de la colección
            // Max()            -> Valor máximo, se aplica también a los alfanúmericos
            // Min()            -> Valor mínimo, se aplica también a los alfanúmericos
            // Sum()            -> Suma valores númericos
            // Average()        -> Calcula la media de valores númericos
            // Aggregate()      -> Aplica una formula o método de agregación

            var demo1 = DataLists.ListaProductos
                .Where(r => r.Precio > 0.90)
                .Count();

            var demo2 = DataLists.ListaProductos
                .Count(r => r.Precio > 0.90);

            var demo3 = (from r in DataLists.ListaProductos
                         where r.Precio > 0.90
                         select r).Count();


            ////////////////////////////////////
            // PAGINACIÓN
            ////////////////////////////////////
            
            // Ordenación en la base de datos

            var pag2a = DataLists.ListaProductos
                .OrderBy(r => r.Descripcion)
                .Skip(5)
                .Take(5)
                .Select(r => r)
                .ToList();


            // Ordenación en la base de PC
            var pag2b = DataLists.ListaProductos
                .OrderBy(r => r.Descripcion)
                .Select(r => r)
                .ToList()
                .Skip(5)
                .Take(5);

            var data = DataLists.ListaProductos
                .OrderBy(r => r.Descripcion)
                .Select(r => r)
                .ToList();

            var p1 = data.Skip(0).Take(5);
            var p2 = data.Skip(5).Take(5);
            var p3 = data.Skip(10).Take(5);

        }

        /// <summary>
        /// Clientes mayores de 40 años
        /// </summary>
        static void Ejercicio1()
        {
            var clientes = DataLists.ListaClientes
                .Where(r => DateTime.Now.Year - r.FechaNac.Year > 40)
                .ToList();

            var clientes2 = DataLists.ListaClientes
                .Where(r => DateTime.Now.Subtract(r.FechaNac).TotalDays / 365 > 40)
                .ToList();

            var clientes3 = DataLists.ListaClientes
                .Where(r => r.FechaNac < DateTime.Now.AddYears(-40))
                .ToList();

            var clientes4 = DataLists.ListaClientes
                .Where(r => r.FechaNac.AddYears(40) <= DateTime.Now)
                .ToList();

            Console.Clear();
            Console.WriteLine("===============================================");
            Console.WriteLine(" LISTADO DE CLIENTES CON MÁS DE 40 AÑOS");
            Console.WriteLine("===============================================");

            foreach (var item in clientes4)
                Console.WriteLine($"{item.Id}# {item.Nombre}");

            Console.WriteLine("===============================================");
        }

        /// <summary>
        /// Productos que comiencen por C ordenados por precio
        /// </summary>
        static void Ejercicio2()
        {
            var productos = DataLists.ListaProductos
                .Where(r => r.Descripcion.StartsWith("C"))
                .OrderBy(r => r.Precio)
                .Select(r => r)
                .ToList();

            Console.Clear();
            Console.WriteLine("=====================================================");
            Console.WriteLine(" PRODUCTOS QUE COMIENZA POR C ORDENADOR POR PRECIO");
            Console.WriteLine("=====================================================");

            foreach (var item in productos)
                Console.WriteLine($"{item.Id.ToString().PadLeft(3, ' ')}# {item.Descripcion.PadRight(20, ' ')} {item.Precio.ToString("N2").PadLeft(5, ' ')} Euros/unidad");

            Console.WriteLine("=====================================================");
        }

        /// <summary>
        /// Listar un detalle de todos los pedidos
        /// </summary>
        static void Ejercicio3()
        {
            Console.Clear();
            Console.Write("Número de Pedido: ");
            int numPedido = Convert.ToInt32(Console.ReadLine());

            var lineas = DataLists.ListaLineasPedido
                .Where(r => r.IdPedido == numPedido)
                .Select(r => r)
                .ToList();

            float total = 0;
            Console.WriteLine("===============================================");

            foreach (var linea in lineas)
            {
                var producto = DataLists.ListaProductos
                    .Where(r => r.Id == linea.IdProducto)
                    .FirstOrDefault();

                Console.Write($"{linea.IdProducto.ToString().PadLeft(3, '0')}# ");
                Console.Write($"{producto.Descripcion.PadRight(20, ' ')} ");
                Console.Write($"{linea.Cantidad.ToString("N0").PadLeft(5, ' ')} x {producto.Precio.ToString("N2").PadLeft(5, ' ')}   ");
                Console.WriteLine($"{(producto.Precio * linea.Cantidad).ToString("N2").PadLeft(5, ' ')}");

                total += (linea.Cantidad * producto.Precio);
            }
            Console.WriteLine("===============================================");
            Console.WriteLine("TOTAL".PadLeft(39, ' ') + $"   {total.ToString("N2").PadLeft(5, ' ')}");
            Console.WriteLine("===============================================");
        }

        /// <summary>
        /// Listar un detalle de todos los pedidos, utilizando subconsultas de LINQ
        /// </summary>
        static void Ejercicio3B()
        {
            Console.Clear();
            Console.Write("Número de Pedido: ");
            int numPedido = Convert.ToInt32(Console.ReadLine());

            var lineas = DataLists.ListaLineasPedido
                .Where(r => r.IdPedido == numPedido)
                .Select(r => new { 
                    r.IdProducto,                  
                    Descripcion = DataLists.ListaProductos
                        .Where(s => s.Id == r.IdProducto)
                        .Select(r => r.Descripcion)
                        .FirstOrDefault(),
                    Precio = DataLists.ListaProductos
                        .Where(s => s.Id == r.IdProducto)
                        .Select(r => r.Precio)
                        .FirstOrDefault(),
                    r.Cantidad
                })
                .ToList();


            var lineas2 = from r in DataLists.ListaLineasPedido
                          where r.IdPedido == numPedido
                          select new
                          {
                              r.IdProducto,
                              Descripcion = (from s in DataLists.ListaProductos
                                            where s.Id == r.IdProducto
                                            select s.Descripcion).FirstOrDefault(),
                              Precio = (from s in DataLists.ListaProductos
                                       where s.Id == r.IdProducto
                                       select s.Precio).FirstOrDefault(),
                              r.Cantidad
                          };


            float total = 0;

            Console.WriteLine("===============================================");
            foreach (var linea in lineas)
            {
                Console.Write($"{linea.IdProducto.ToString().PadLeft(3, '0')}# ");
                Console.Write($"{linea.Descripcion.PadRight(20, ' ')} ");
                Console.Write($"{linea.Cantidad.ToString("N0").PadLeft(5, ' ')} x {linea.Precio.ToString("N2").PadLeft(5, ' ')}   ");
                Console.WriteLine($"{(linea.Precio * linea.Cantidad).ToString("N2").PadLeft(5, ' ')}");

                total += (linea.Cantidad * linea.Precio);
            }
            Console.WriteLine("===============================================");
            Console.WriteLine("TOTAL".PadLeft(39, ' ') + $"   {total.ToString("N2").PadLeft(5, ' ')}");
            Console.WriteLine("===============================================");

        }

        /// <summary>
        /// Mostrar el importe total de un pedido
        /// </summary>
        static void Ejercicio4()
        {
            Console.Clear();
            Console.Write("Número de Pedido: ");
            int numPedido = Convert.ToInt32(Console.ReadLine());

            var lineas = DataLists.ListaLineasPedido
                .Where(r => r.IdPedido == numPedido)
                .Select(r => new { r.IdProducto, r.Cantidad })
                .ToList();

            float total = 0;
            

            foreach (var linea in lineas)
            {
                var precio = DataLists.ListaProductos
                    .Where(r => r.Id == linea.IdProducto)
                    .Select(r => r.Precio)
                    .FirstOrDefault();

                total += (linea.Cantidad * precio);
            }
            Console.WriteLine("===============================================");
            Console.WriteLine("TOTAL".PadLeft(39, ' ') + $"   {total.ToString("N2").PadLeft(5, ' ')}");
            Console.WriteLine("===============================================");
        }

        /// <summary>
        /// Mostrar el importe total de un pedido, calculado con LINQ
        /// </summary>
        static void Ejercicio4B()
        {
            Console.Clear();
            Console.Write("Número de Pedido: ");
            int numPedido = Convert.ToInt32(Console.ReadLine());

            var total = DataLists.ListaLineasPedido
                .Where(r => r.IdPedido == numPedido)
                .Sum(r => r.Cantidad * DataLists.ListaProductos
                                            .Where(s => s.Id == r.IdProducto)
                                            .Select(s => s.Precio)
                                            .FirstOrDefault());

            var total2 = (from r in DataLists.ListaLineasPedido
                         where r.IdPedido == numPedido
                         select (r.Cantidad * (from s in DataLists.ListaProductos
                                              where s.Id == r.IdProducto
                                              select s.Precio).FirstOrDefault())).Sum();
            

            Console.WriteLine("===============================================");
            Console.WriteLine("TOTAL".PadLeft(39, ' ') + $"   {total.ToString("N2").PadLeft(5, ' ')}");
            Console.WriteLine("===============================================");
        }

        /// <summary>
        /// Mostrar los pedidos con lapiceros
        /// </summary>
        static void Ejercicio5()
        {
            var ids = DataLists.ListaProductos
                .Where(r => r.Descripcion.ToLower().Contains("lapicero"))
                .Select(r => r.Id)
                .ToList();    

            var pedidos = DataLists.ListaLineasPedido
                .Where(r => ids.Contains(r.IdProducto))
                .Select(r => r.IdPedido)
                .Distinct()
                .ToList();

            var ids2 = (from r in DataLists.ListaProductos
                       where r.Descripcion.ToLower().Contains("lapicero")
                       select r.Id).ToList();

            var pedidos2 = (from r in DataLists.ListaLineasPedido
                            where ids2.Contains(r.IdProducto)
                            select r.IdPedido).Distinct();

            Console.Clear();
            foreach (var pedido in pedidos)
                Console.WriteLine($"ID Pedido: {pedido.ToString().PadLeft(4, ' ')}  (contitne lapiceros)");
        }

        /// <summary>
        /// Número de pedidos con cuaderno grande
        /// </summary>
        static void Ejercicio6()
        {
            var pedidos = DataLists.ListaLineasPedido
                .Where(r => r.IdProducto == 2)
                .Count();

            var pedidos2 = DataLists.ListaLineasPedido
                .Count(r => r.IdProducto == 2);

            var pedidos3 = DataLists.ListaLineasPedido
                .Where(r => r.IdProducto == 2)
                .Select(r => r.IdPedido)
                .Distinct()
                .Count();

            var pedidos4 = (from r in DataLists.ListaLineasPedido
                        where r.IdProducto == 2
                        select r.Id).Count();


            Console.Clear();
            Console.WriteLine($"{pedidos} pedidos con cuadernos grandes.");

        }

        /// <summary>
        /// Unidades vendidas de cuaderno pequeño
        /// </summary>
        static void Ejercicio7()
        {
            var unidades = DataLists.ListaLineasPedido
                .Where(r => r.IdProducto == 3)
                .Sum(r => r.Cantidad);

            var unidades2 = DataLists.ListaLineasPedido
                .Where(r => r.IdProducto == DataLists.ListaProductos
                                                .Where(s => s.Descripcion.ToLower().Contains("cuaderno pequeño"))
                                                .Select(s => s.Id)
                                                .FirstOrDefault())
                .Sum(r => r.Cantidad);

            var unidades3 = (from r in DataLists.ListaLineasPedido
                             where r.IdProducto == 3
                             select r.Cantidad).Sum();

            var lista = from r in DataLists.ListaLineasPedido
                            where r.IdProducto == 3
                            select new { r.IdPedido, r.Cantidad };

            var unid = lista.Sum(r => r.Cantidad);

            Console.Clear();
            Console.WriteLine($"{unidades} unidades vendidas de cuaderno pequeño.");
        }

        /// <summary>
        /// El pedido con más unidades
        /// </summary>
        static void Ejercicio8()
        { }

        /// <summary>
        /// Listado de pedidos ordernados por fecha
        /// </summary>
        static void Ejercicio9()
        { }

    }
}
