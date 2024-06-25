namespace Formacion.CSharp.ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Herencia2();
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
    }
}
