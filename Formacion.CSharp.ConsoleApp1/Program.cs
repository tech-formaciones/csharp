using Formacion.CSharp.ConsoleApp1.Models;

namespace Formacion.CSharp.ConsoleApp1;

/// <summary>
/// La clase Program contiene el método Main, donde inicia la ejecución del programa
/// </summary>
class Program
{
    /// <summary>
    /// Método Main, inicio del programa
    /// </summary>
    static void Main(string[] args)
    {
        Console.Clear();
        DeclaracionVariables();
    }

    /// <summary>
    /// Declaración de variables
    /// </summary>
    static void DeclaracionVariables()
    {
        string texto = "Hola Mundo !!!";
        string otroTexto;
        System.String texto2 = "Mi nombre es Borja";

        int numero = 10;
        int otroNumero;
        System.Int32 numero2 = 0;

        decimal a, b, c;
        

        // Instanciamos un objeto Alumno y modificamos sus propiedades o variables
        Alumno alumno = new Alumno();
        alumno.Nombre = "Ana";
        alumno.Apellidos = "Sanz";
        alumno.Edad = 33;

        // Instanciamos un objeto Alumno y asignamos valores a sus propiedades o variables
        Alumno alumno1 = new Alumno()
        {
            Nombre = "Julian",
            Apellidos = "Sánchez",
            Edad = 25
        };

        // Instaciamos un objeto Alumno usando VAR, OBJECT, DYNAMIC

        // Las variables con tipo implícito VAR siempre deben inicializar
        var alumno2 = new Alumno();
        alumno2.Nombre = "María José";

        Console.WriteLine($"Tipo 2: {alumno2.GetType()}");
        Console.WriteLine($"Nombre 2: {alumno2.Nombre}" + Environment.NewLine);

        // Object tiene la capacidad de contener cualquier tipo de dato, se comprueba en diseño
        // No permite acceder a los miembros del objeto, para accder tenemos que aplicar la conversión
        Object alumno3 = new Alumno();
        ((Alumno)alumno3).Nombre = "Isabel";
        // alumno3.Nombre = "Isabel"; <- No funciona por ser un Object

        Console.WriteLine($"Tipo 3: {alumno3.GetType()}");
        Console.WriteLine($"Nombre 3: {((Alumno)alumno3).Nombre}\n");
        // Console.WriteLine($"Nombre 3: {alumno3.Nombre}"); <- No funciona por ser un Object

        // dynamic tiene la capacidad de contener cualquier tipo de datos, se comprueba en ejecución
        dynamic alumno4 = new Alumno();
        alumno4.Nombre = "Antonio José";
        alumno4.Edad = 30;

        Console.WriteLine($"Tipo 4: {alumno4.GetType()}");
        Console.WriteLine($"Nombre 4: {alumno4.Nombre}" + Environment.NewLine);


        // Sintaxis C# de versiones más actuales
        Alumno alumno5 = new();
        alumno5.Nombre = "Borja";

        Console.WriteLine($"Tipo 5: {alumno5.GetType()}");
        Console.WriteLine($"Nombre 5: {alumno5.Nombre}" + Environment.NewLine);
    }
    
} 
