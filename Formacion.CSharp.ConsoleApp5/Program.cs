using System;
using System.Runtime.CompilerServices;

namespace Formacion.CSharp.ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Polimorfismo();
        }

        /// <summary>
        /// La herencia nos permite crear nuevas clases que reutilizan, extienden y
        /// modifican el comportamiento definido en otras clases.
        /// Clase Padre -> Clase Hija
        /// Clase Base -> Clase Derivada
        /// </summary>
        static void Herencia()
        { 
            // Una objeto lista, no es posible usar el método OUTPUTALL
            // ya que no esta implementado.

            var lista = new List<int>() { 1, 2, 3 };
            lista.Add(4);
            //lista.OutputAll();

            
            // Mediante la herencia el objeto tiene todos los métodos, propiedades, ...
            // de una LIST como .Add() y además los métodos implementados como OUTPUTALL

            var listaEx = new ListExtend<int>() { 1, 2, 3 };
            listaEx.Add(4);
            listaEx.OutputAll();

            var Alumnos = new ListExtend<Alumno>
            {
                new Alumno() { Nombre = "Julia", Apellidos = "Fernández", Edad = 24 },
                new Alumno() { Nombre = "Rosa", Apellidos = "Perez", Edad = 26 }
            };
            Alumnos.Add(new Alumno() { Nombre = "Ramón", Apellidos = "Sanz", Edad = 19 });
            Alumnos.OutputAll();

            Console.WriteLine($"Lista Extendida: {Alumnos.ToString()}");
        }

        static void Herencia2()
        {
            Anfibio anfibio = new Anfibio() { Nombre = "Rana" };
            anfibio.MetodoA();
            anfibio.MetodoB();
            Console.WriteLine($"{anfibio.Especie}\n");

            Reptil reptil = new Reptil() { Nombre = "Lagarto" };
            reptil.MetodoA();
            reptil.MetodoB();
            reptil.MetodoC();
            Console.WriteLine($"{reptil.Especie}\n");

            Test(anfibio);
            Test(reptil);
        }

        static void Test(Animal animal)
        {
            animal.MetodoA();
            animal.MetodoB();
            Console.WriteLine($"{animal.Nombre} - {animal.Especie}");
            Console.WriteLine($"{animal.GetType().ToString()}\n");

            if (animal.GetType() == typeof(Reptil)) ((Reptil)animal).MetodoC();
        }

        static void Polimorfismo()
        {
            Coche coche = new Coche() { Nombre = "Hunday Tucson", Ruedas = 4 };
            coche.Iniciar();
            coche.Parar();
            Console.WriteLine($"{coche.Nombre}\n");
            
            Avion avion = new Avion() { Nombre = "Airbus A320", Ruedas = 8, Potencia = 8000 };
            avion.Iniciar();
            avion.Parar();
            avion.Despegar();
            Console.WriteLine($"{avion.Nombre}\n");

            Test2(coche);
            Test2(avion);
        }

        static void Test2(IVehiculo vehiculo)
        {
            vehiculo.Iniciar();
            vehiculo.Parar();
            Console.WriteLine($"{vehiculo.Nombre}\n");

            if(vehiculo.GetType() == typeof(Avion)) ((Avion)vehiculo).Despegar();
        }

        static void ClasesGenericas()
        {
            DemoString demo1 = new DemoString("Hola Mundo !!!");
            demo1.Metodo();
            Console.WriteLine(Environment.NewLine);

            DemoGenerica<string> demo1b = new DemoGenerica<string>("Hola Mundo !!!");
            demo1b.Metodo();
            Console.WriteLine(Environment.NewLine);

            DemoInt demo2 = new DemoInt(33);
            demo2.Metodo();
            Console.WriteLine(Environment.NewLine);

            DemoGenerica<int> demo2b = new DemoGenerica<int>(33);
            demo2b.Metodo();
            Console.WriteLine(Environment.NewLine);

            DemoAlumno demo3 = new DemoAlumno(new Alumno { Nombre = "Julia", Apellidos = "Sanz" });
            demo3.Metodo();
            Console.WriteLine(Environment.NewLine);

            DemoGenerica<Alumno> demo3b = new DemoGenerica<Alumno>(new Alumno { 
                Nombre = "Julia", 
                Apellidos = "Sanz" 
            });
            demo3b.Metodo();
            Console.WriteLine(Environment.NewLine);

            DemoGenerica<decimal> demo4b = new DemoGenerica<decimal>(33.20M);
            demo4b.Metodo();
            Console.WriteLine(Environment.NewLine);
        }

        static void ExtensionTipos()
        {
            string texto = "En un lugar de la mancha de cuyo nombre ...";
            Console.WriteLine($"Texto original: {texto}");
            Console.WriteLine($"Texto formato título: {texto.ToTitle()}");
            Console.WriteLine($"Texto formato título: {ConvertToTitleCase(texto)}");

            Console.WriteLine($"Caracteres: {texto.Length}");
            Console.WriteLine($"Palabras: {texto.WordCount()}");

            int numero = 25;
            Console.WriteLine($"Caracteres: {numero.Count()}");

        }

        static string ConvertToTitleCase(string texto)
        { 
            if (string.IsNullOrEmpty(texto)) return texto;
            else return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(texto);
        }
    }
}
