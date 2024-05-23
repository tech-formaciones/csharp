using Formacion.CSharp.ConsoleApp2.Models;

namespace Formacion.CSharp.ConsoleApp2;

class Program
{
    static void Main(string[] args)
    {
        var alumno = new Alumno();
        // alumno.Nombre = "  Borja ";
        // alumno.Apellidos = "Cabeza";
        // alumno.Edad = 30;
        // alumno.CambiaEdad = 27;
        int n = 25;

        alumno.MetodoTres(n, "Borja", param4: "nuevo valor");

        Console.WriteLine($"Nombre: {alumno.Nombre} {alumno.Apellidos}");
        Console.WriteLine($"Edad: {alumno.Edad}");
        Console.WriteLine($"Nombre: {alumno.NombreCompleto}");
    }
}
