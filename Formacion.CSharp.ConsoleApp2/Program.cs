using Formacion.CSharp.ConsoleApp2.Models;

namespace Formacion.CSharp.ConsoleApp2;

class Program
{
    ///<summary>
    /// Método Main inicio del programa
    ///</summary>
    static void Main(string[] args)
    {
        // Los objetos de tipo referencia se contruyen con CLASS
        // Cuando los asignamos a una variable se guarda un puntero a la dirección
        // de memoria que contiene los datos.

        // Las variables first y second apuntan a la misma dirección de memoria,
        // cualquier cambio en una de ellas afecta ambas

        Alumno first = new Alumno() { Nombre = "Borja", Edad = 30 };
        Alumno second = first;

        second.Nombre = "Ángel";
        Console.WriteLine(first.Nombre + Environment.NewLine);


        // Los objetos de tipo valor se contruyen con STRUCT
        // Cuando los asignamos a una variable se guarda una copia de los datos en una
        // nueva dirección de memoria.

        // Las variables first y second apuntan a diferentes direcciones de memoria,
        // cualquier cambio solo afecta a la variable donde realizamos el cambio

        Alumno2 first2 = new Alumno2() { Nombre = "Borja", Edad = 30 };
        Alumno2 second2 = first2;

        second2.Nombre = "Ángel";
        Console.WriteLine(first2.Nombre + Environment.NewLine);


        // Lo mismo sucede cuando asignamos indicamos el valor para un parámetro.
        
        // Los datos tipo referencia pasan un puntero a la dirección de memoria,
        // los cambios dentro del métedo persisten en la variable externa

        Alumno alumno = new Alumno() { Nombre = "Borja", Edad = 30};

        Console.WriteLine($"Nombre: {alumno.Nombre} - Edad: {alumno.Edad}");
        Transformar(alumno);
        Console.WriteLine($"Nombre: {alumno.Nombre} - Edad: {alumno.Edad}\n");


        // Los datos tipo valor pasan una copia de los datos
        // los cambios dentro del métedo NO persisten en la variable externa

        Alumno2 alumno2 = new Alumno2() { Nombre = "Borja", Edad = 30 };

        Console.WriteLine($"Nombre: {alumno2.Nombre} - Edad: {alumno2.Edad}");
        Transformar(alumno2);
        Console.WriteLine($"Nombre: {alumno2.Nombre} - Edad: {alumno2.Edad}\n");

        // Para que datos de tipo valor se comporten como de referencia utilizamos REF

        Console.WriteLine($"Nombre: {alumno2.Nombre} - Edad: {alumno2.Edad}");
        Transformar(ref alumno2);
        Console.WriteLine($"Nombre: {alumno2.Nombre} - Edad: {alumno2.Edad}\n");

        int numero = 10;
        Transformar(ref numero);
        Console.WriteLine($"Número: {numero}\n\n");


        // Los parámetros externos se declaran con OUT
        //
        // Tiene como objetivo definir retorno de datos complementario a la salida estandar
        // de los métodos con RETURN
        //
        // El comportamiento es identico a los parámetros REF, con la pequeña diferecia que
        // los parámetros OUT obligatoriamente les tenemos que asignar un valor antes de la
        // finalización del método al menos una vez

        string num = "10";
        int num2 = 0;
        string texto = "";
        bool resultado = TryParseToInt(num, out num2, out texto);

        Console.WriteLine($"Resultado: {resultado}");
        Console.WriteLine($"Resultado: {texto}");
        Console.WriteLine($"Valor: {num2}");
    }

    /// <summary>
    /// Método prueba, parámetros externos OUT
    /// </summary>
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

    /// <summary>
    /// Método prueba, parámetro de tipo valor con REF
    /// </summary>
    static public void Transformar(ref int n)
    {
        n = 100;
        Console.WriteLine($"Número: {n}");
    }

    /// <summary>
    /// Método prueba, parámetro de tipo valor
    /// </summary>
    static void Transformar(Alumno2 alumno)
    {
        alumno.Edad = 40;
        Console.WriteLine($"Ahora la Edad es {alumno.Edad}");
    }

    /// <summary>
    /// Método prueba, parámetro de tipo valor con REF
    /// </summary>
    static void Transformar(ref Alumno2 alumno)
    {
        alumno.Edad = 40;
        Console.WriteLine($"Ahora la Edad es {alumno.Edad}");
    }

    /// <summary>
    /// Método prueba, parámetro de tipo referencia
    /// </summary>
    static void Transformar(Alumno alumno)
    {
        alumno.Edad = 40;
        Console.WriteLine($"Ahora la Edad es {alumno.Edad}");
    }
}
