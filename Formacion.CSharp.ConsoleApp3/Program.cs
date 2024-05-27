using System.Collections;
using Formacion.CSharp.ConsoleApp3.Models;

namespace Formacion.CSharp.ConsoleApp3
{
    class Program
    {
        ///<summary>
        ///Método Main inicio del programa
        ///</summary>
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
                Console.WriteLine("*  1. Uso Arrays".PadRight(55) + "*");
                Console.WriteLine("*  2. Uso ArrayList".PadRight(55) + "*");
                Console.WriteLine("*  3. Uso Hashtable".PadRight(55) + "*");
                Console.WriteLine("*  4. Uso List".PadRight(55) + "*");
                Console.WriteLine("*  5. Uso Dictionary".PadRight(55) + "*");
                Console.WriteLine("*  6. Items como Referencia".PadRight(55) + "*");
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
                        Console.Clear();
                        Arrays();
                        break;
                    case 2:
                        Console.Clear();
                        ArrayList();
                        break;
                    case 3:
                        Console.Clear();
                        Hashtable();
                        break;
                    case 4:
                        Console.Clear();
                        List();
                        break;
                    case 5:
                        Console.Clear();
                        Dictionary();
                        break;
                    case 6:
                        Console.Clear();
                        AddItemReference();
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
        /// Uso de Arrays
        /// </summary>
        static void Arrays()
        {
            // Instanciar un array de una dimensión
            int[] array1 = new int[5];

            int[] array2a = new int[] { 1, 8, 19, 45 };
            int[] array2b = { 1, 8, 19, 45 };
            
            Alumno[] array3 = new Alumno[3];

            Alumno[] array4a = new Alumno[] { new Alumno(), new Alumno(), new Alumno() };
            Alumno[] array4b = { new Alumno(), new Alumno(), new Alumno() };

            // Modificar valores
            array1[0] = 30;
            array2a[0] = 30;

            array3[0] = new Alumno();
            array3[0].Nombre = "Borja";

            array4a[0].Nombre = "Borja";

            // Leer el contenido de las posiciones
            Console.WriteLine($"Valor posición 0: {array1[0]}");
            Console.WriteLine($"Nombre Alumno posición 0: {array3[0].Nombre}");

            // Instanciar un array de multiples dimensiones
            int[,] multiDimensionArray1 = new int[2, 4];
            int[,] multiDimensionArray2 = { { 1, 2, 3, 4 }, { 9, 52, 36, 14 } };

            /*
                0 -> 1, 2, 3, 4 
                     0, 1  2, 3
                
                1 -> 9, 52, 36, 14
                     0,  1,   2,  3

                0 -> 0, 0, 0, 0 
                     0, 1  2, 3
                
                1 -> 0, 0, 0, 0
                     0, 1, 2, 3

             */

            Console.WriteLine($"Valor [1,2]: {multiDimensionArray1[1, 2]}");

            int[,,] arrayTridimensional = new int[2, 5, 5];

            // Instanciamos un array denominado jagged
            int[][] jaggedArray = new int[6][];
            
            jaggedArray[0] = new int[] { 1, 2, 3, 4 };
            jaggedArray[1] = new int[] { 1, 2, 3, 4, 5, 6, 7, 9 };
            jaggedArray[2] = new int[] { 1000, 2 };

            Console.WriteLine($"Valor [1][2]: {jaggedArray[1][2]}");

            // Asignar valores por defecto
            int[] numeros = { 15, -3, 577, 82, 19, 33, 78, 1000, 63 };
            Console.WriteLine($"Número de Elementos: {numeros.Length}");
            foreach (int i in numeros) Console.Write($"{i} - ");
            Console.WriteLine("");

            Array.Clear(numeros, 2, 2);
            Console.WriteLine($"Número de Elementos: {numeros.Length}");
            foreach (int i in numeros) Console.Write($"{i} - ");
            Console.WriteLine("");

            Array.Clear(numeros);
            Array.Clear(numeros, 0, numeros.Length);
            Console.WriteLine($"Número de Elementos: {numeros.Length}");
            foreach (int i in numeros) Console.Write($"{i} - ");
            Console.WriteLine("");

            // Añadir posiciones al Array
            string[] frutas = { "naranja", "limón", "pomelo", "líma" };
            Array.Resize(ref frutas, 15);
            frutas[10] = "manzana";
            foreach (string i in frutas) Console.Write($"{i} - ");

        }

        /// <summary>
        /// Uso de ArrayList
        /// </summary>
        static void ArrayList()
        {
            // Instanciar
            ArrayList array = new ArrayList();

            // Eliminar todos los elementos del Array
            array.Clear();

            // Añadir nuevo elementos
            array.Add(1);
            array.Add("borja");
            array.Add(new Alumno() { Nombre = "Borja", Edad = 49 });
            array.Add(new { Nombre = "Borja", Apellidos = "Cabeza" });

            array.AddRange(new string[] { "manzana", "pera", "melocoton" });

            ArrayList array2 = new ArrayList();
            array2.Add(30);
            array2.Add(15);
            array.AddRange(array2);

            // Añadir un elemento en una posición del array
            array.Insert(2, "blanco");
            array.InsertRange(2, new string[] { "rojo", "verde", "azul" });


            // Eliminar elementos del ArrayList
            array.Remove("blanco");
            array.RemoveAt(4);
            array.RemoveRange(2, 2);

            // Saber si un elemento esta contenido en el ArrayList
            Console.WriteLine($"Contiene el item pera: {array.Contains("pera")}");

            // Ordenar elementos
            array.Sort();

            // Invertir los elementos
            array.Reverse();

            // Convertir el ArrayList en un array -> object[] nuevoArray
            object[] nuevoArray = array.ToArray();

            // Recorremos el ArrayList
            foreach (var item in array)
                Console.WriteLine($"Item: {item.GetType().ToString()}");

            // Número de elementos del ArrayList
            Console.WriteLine($"Número de elementos: {array.Count}");
        }

        /// <summary>
        /// Uso de Hashtable
        /// </summary>
        public static void Hashtable()
        {
            // Instanciar
            Hashtable ht = new Hashtable();

            // Eliminar todos los elementos
            ht.Clear();

            // Añadir elementos
            ht.Add(1200, "Borja Cabeza");
            ht.Add("ANATR", "Ana Trujillo");
            ht.Add(412, new Alumno());


            // Número de elementos
            Console.WriteLine($"Número de elementos: {ht.Count}");

            // Eliminar un elemento, mediante la clave
            ht.Remove(1200);
            
            // Recorrer el HashTable
            foreach (var clave in ht.Keys)
                Console.WriteLine($"Clave = {clave} - Valor = {ht[clave]}");
        }

        /// <summary>
        /// Uso de Listas
        /// </summary>
        public static void List()
        {
            // Instanciar
            List<string> list = new List<string>();

            List<string> lista = new List<string>();
            List<string> lista2 = new();
            var lista3 = new List<string>();

            List<int> lista4 = new List<int>();
            List<Alumno> lista5 = new List<Alumno>();

            // Eliminar los elementos
            list.Clear();

            // Añadir elementos
            list.Add("azul");
            list.Add("verde");
            list.Add("rosa");
            list.Add("amarillo");

            // Añadir elementos en una posición
            list.Insert(4, "blanco");

            // Añadir todos los elementos de otra colección
            var colores = new string[] { "marron", "naranja", "negro", "violeta" };
            list.AddRange(colores);

            // Número de elementos del List
            Console.WriteLine($"Número de elementos: {list.Count}");

            // Eliminar elementos
            list.Remove("azul");
            list.RemoveAt(4);
            list.RemoveRange(2, 2);

            // Saber si un elemento esta contenido
            Console.WriteLine($"Contiene el item rojo: {list.Contains("rojo")}");

            // Orderna los elementos
            list.Sort();

            // Invertir el orden
            list.Reverse();

            // Convertir en un array de object -> object[] array = new array[10]
            var nuevoArray = list.ToArray();

            // Recorrer el List
            foreach (var item in list)
                Console.WriteLine($"{list.IndexOf(item)}# Item: {item}");

            for (var i = 0; i < list.Count; i++)
                Console.WriteLine($"{i}# {list[i]}");
        }

        /// <summary>
        /// Uso de Diccionarios
        /// </summary>
        public static void Dictionary()
        {
            // Instanciar
            Dictionary<int, string> dic = new Dictionary<int, string>();

            Dictionary<int, string> dic1 = new();
            var dic2 = new Dictionary<int, string>();

            // Eliminar todos los elementos
            dic.Clear();

            // Añadir elementos
            dic.Add(1200, "Borja Cabeza");
            dic.Add(1300, "Ana Trujillo");
            dic.Add(1412, "José Guzman");

            // Modificar un valor
            dic[1300] = "Antonio Trujillo";

            // Número de elementos
            Console.WriteLine($"Número de elementos: {dic.Count}");

            // Eliminar un elemento
            dic.Remove(1200);

            // Recorrer el diccionario
            foreach (var clave in dic.Keys)
                Console.WriteLine($"Clave = {clave} - Valor = {dic[clave]}");
        }

        /// <summary>
        /// Añadir elementos de tipo referencia a una colección
        /// </summary>
        public static void AddItemReference()
        {
            // Opcion A - NO VALIDA
            //
            // Comprobamos que al añadir un elemento de tipo referencia varias veces, tenemos
            // el mismo elemnto en todas las posiciones.
            // 
            // Solo sería valido cuando trabajamos con objetos de tipo valor

            var alumnos = new List<Alumno>();

            var alumno = new Alumno() { Nombre = "Borja", Edad = 49 };            
            alumnos.Add(alumno);

            alumnos.Add(alumno);
            alumnos[1].Nombre = "Ana";

            alumnos.Add(alumno);
            alumnos[2].Nombre = "José";

            alumnos.Add(alumno);
            alumnos[3].Nombre = "Bea";

            alumnos.Add(alumno);
            alumnos[4].Nombre = "Silvia";


            foreach (var item in alumnos) 
                Console.WriteLine($"Nombre: {item.Nombre} - Edad: {item.Edad}");

            Console.ReadKey();

            // Opcion B - VALIDA
            //
            // Para tener un objeto distinto en cada posición, necesitamos instanciar cada objeto
            // antes de añadirlo a la lista

            var alumnos2a = new List<Alumno>();

            alumnos2a.Add(new Alumno() { Nombre = "Borja", Edad = 49 });
            alumnos2a.Add(new Alumno() { Nombre = "José", Edad = 33 });
            alumnos2a.Add(new Alumno() { Nombre = "Bea", Edad = 27 });
            alumnos2a.Add(new Alumno() { Nombre = "Silvia", Edad = 34 });

            ///////////////////////////////////////////////////////////////////////////////////

            var alumnos2b = new List<Alumno>
            {
                new Alumno() { Nombre = "Borja", Edad = 49 },
                new Alumno() { Nombre = "José", Edad = 33 },
                new Alumno() { Nombre = "Bea", Edad = 27 },
                new Alumno() { Nombre = "Silvia", Edad = 34 }
            };

            ///////////////////////////////////////////////////////////////////////////////////

            var alumnos2c = new List<Alumno>();

            var item1 = new Alumno() { Nombre = "Borja", Edad = 49 };
            alumnos2c.Add(item1);

            var item2 = new Alumno() { Nombre = "José", Edad = 33 };
            alumnos2c.Add(item2);

            var item3 = new Alumno() { Nombre = "Bea", Edad = 27 };
            alumnos2c.Add(item3);

            var item4 = new Alumno() { Nombre = "Silvia", Edad = 34 };
            alumnos2c.Add(item4);

            ///////////////////////////////////////////////////////////////////////////////////

            foreach (var item in alumnos2a)
                Console.WriteLine($"Nombre: {item.Nombre} - Edad: {item.Edad}");
        }
    }
}
