using Formacion.CSharp.ConsoleApp2.Models;

namespace Formacion.CSharp.ConsoleApp2;

class Program
{
    static void Main(string[] args)
    {
        var alumno = new Alumno() { Nombre = "Borja", Edad = 30};

        Console.WriteLine($"Nombre: {alumno.Nombre} - Edad: {alumno.Edad}");
        Transformar(alumno);
        Console.WriteLine($"Nombre: {alumno.Nombre} - Edad: {alumno.Edad}");

    }

    static void Transformar(Alumno2 alumno)
    {
        alumno.Edad = 40;
        Console.WriteLine($"Ahora la Edad es {alumno.Edad}");
    }

    static void Transformar(Alumno alumno)
    {
        alumno.Edad = 40;
        Console.WriteLine($"Ahora la Edad es {alumno.Edad}");
    }
}
