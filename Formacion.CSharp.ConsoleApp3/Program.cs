using System.Collections;

namespace Formacion.CSharp.ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Lists();
        }

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
            
            int[,,] arrayTridimensional = new int[2, 5, 5];

            /*
                0 -> 1, 2, 3, 4 
                     0, 1  2, 3
                
                1 -> 9, 52, 36, 14
                     0,  1   2,  3

                0 -> 0, 0, 0, 0 
                     0, 1  2, 3
                
                1 -> 0, 0, 0, 0
                     0, 1, 2, 3

             */

            Console.WriteLine($"Valor [1,2]: {multiDimensionArray1[1, 2]}");


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

        static void ArrayLists()
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

            array.

            // Recorremos el ArrayList
            foreach (var item in array)
                Console.WriteLine($"Item: {item.GetType().ToString()}");

            // Número de elementos del ArrayList
            Console.WriteLine($"Número de elementos: {array.Count}");
        }

        static void Lists()
        { 
            // Instanciar List
            List<string> lista = new List<string>();

            List<string> lista2 = new();
            var lista3 = new List<string>();

            List<int> lista4 = new List<int>();
            List<Alumno> lista5 = new List<Alumno>();
     
        }
    }

    internal class Alumno 
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }
    }
}
