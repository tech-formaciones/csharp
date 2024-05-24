using Formacion.CSharp.ConsoleApp2.Models;

namespace Formacion.CSharp.ConsoleApp2;

class Program
{
    static void Main(string[] args)
    {

        Alumno2 alumno = new Alumno2() { Nombre = "Borja", Edad = 30};

        Console.WriteLine($"Nombre: {alumno.Nombre} - Edad: {alumno.Edad}");
        Transformar(ref alumno);
        Console.WriteLine($"Nombre: {alumno.Nombre} - Edad: {alumno.Edad}");

        int numero = 10;
        Transformar(ref numero);
        Console.WriteLine($"Número: {numero}");

        string num = "DDD10";
        int num2 = 0;
        string texto = "";
        bool resultado = TryParseToInt(num, out num2, out texto);

        Console.WriteLine($"Resultado: {resultado}");
        Console.WriteLine($"Resultado: {texto}");
        Console.WriteLine($"Valor: {num2}");
    }

    static public bool TryParseToInt(string num, out int result, out string demo) 
    {
        try
        {
            int resultado = Convert.ToInt32(num);
            result = resultado;
            demo = "OK";

            return true;
        }
        catch (Exception)
        {
            result = 0;
            demo = "NOK";

            return false;
        }
    }


    static public void Transformar(ref int n)
    {
        n = 100;
        Console.WriteLine($"Número: {n}");
    }
    static void Transformar(Alumno2 alumno)
    {
        alumno.Edad = 40;
        Console.WriteLine($"Ahora la Edad es {alumno.Edad}");
    }
    static void Transformar(ref Alumno2 alumno)
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
