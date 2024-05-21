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
        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("".PadRight(56, '*'));
            Console.WriteLine("*  DEMO Y EJERCICIOS".PadRight(55) + "*");
            Console.WriteLine("".PadRight(56, '*'));
            Console.WriteLine("*".PadRight(55) + "*");
            Console.WriteLine("*  1. Declaración de Variables".PadRight(55) + "*");
            Console.WriteLine("*  2. Conversión de Variables".PadRight(55) + "*");
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
                    DeclaracionVariables();
                    break;
                case 2:
                    Console.Clear();
                    ConversionVariables();
                    break;                    
                case 9:
                    Console.ForegroundColor = ConsoleColor.Gray;
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
    /// Declaración de variables
    /// </summary>
    static void DeclaracionVariables()
    {
        ///////////////////////////////////////////////////////////////
        //
        //  Declaración de variables
        //  [tipo] [nombre variable] = [valor inicial (opcional)]
        //
        ///////////////////////////////////////////////////////////////

        string texto = "Hola Mundo !!!";
        string otroTexto;
        System.String texto2 = "Mi nombre es Borja";

        int numero = 10;
        int otroNumero;
        System.Int32 numero2 = 0;

        decimal a, b, c;
        

        ///////////////////////////////////////////////////////////////
        //
        //  Declaración de variables que contienen objetos
        //  [tipo] [nombre variable] = [new constructor (opcional)]
        //
        ///////////////////////////////////////////////////////////////        

        // Instanciamos un objeto Alumno y posteriormente modificamos sus propiedades o variables
        Alumno alumno = new Alumno();
        alumno.Nombre = "Ana";
        alumno.Apellidos = "Sanz";
        alumno.Edad = 33;

        // Instanciamos un objeto Alumno y al mismo tiempo asignamos valores a sus propiedades o variables
        Alumno alumno1 = new Alumno()
        {
            Nombre = "Julian",
            Apellidos = "Sánchez",
            Edad = 25
        };


        ///////////////////////////////////////////////////////////////        
        // Instaciamos un objeto Alumno usando VAR, OBJECT, DYNAMIC
        ///////////////////////////////////////////////////////////////                

        // Las variables con tipo implícito VAR siempre deben inicializar
        var alumno2 = new Alumno();
        alumno2.Nombre = "María José";

        Console.WriteLine($"Tipo 2: {alumno2.GetType()}");
        Console.WriteLine($"Nombre 2: {alumno2.Nombre}" + Environment.NewLine);


        // OBJECT tiene la capacidad de contener cualquier tipo de dato
        // Comprobaciones de código en la etapa de diseño

        // Las variables de tipo OBJECT no permite acceder a los miembros del objeto
        // para accder tenemos que aplicar la conversión
        
        Object alumno3 = new Alumno();
        ((Alumno)alumno3).Nombre = "Isabel";
        // alumno3.Nombre = "Isabel"; <- No funciona por ser un Object

        Console.WriteLine($"Tipo 3: {alumno3.GetType()}");
        Console.WriteLine($"Nombre 3: {((Alumno)alumno3).Nombre}\n");
        // Console.WriteLine($"Nombre 3: {alumno3.Nombre}"); <- No funciona por ser un Object


        // dynamic tiene la capacidad de contener cualquier tipo de datos
        // Comprobaciones de código en ejecución, NO SE COMPRUEBA CUANDO COMPILAMOS
        dynamic alumno4 = new Alumno();
        alumno4.Nombre = "Antonio José";
        alumno4.Edad = 30;

        Console.WriteLine($"Tipo 4: {alumno4.GetType()}");
        Console.WriteLine($"Nombre 4: {alumno4.Nombre}" + Environment.NewLine);


        // Alternativas de sintaxis propio de las versiones más recientes de C# y .NET Core
        Alumno alumno5 = new();
        alumno5.Nombre = "Borja";

        Console.WriteLine($"Tipo 5: {alumno5.GetType()}");
        Console.WriteLine($"Nombre 5: {alumno5.Nombre}" + Environment.NewLine);
    }

    /// <summary>
    /// Conversión de variables
    /// </summary>
    static void ConversionVariables()
    {
        byte num1 = 10;         //  8 bits
        int num2 = 32;          // 32 bits
        string num3 = "42";     // bits variable según contenido

        Console.WriteLine($"Num 1 (byte): {num1} - Num 2 (int): {num2} - Num 3 (string): {num3}" + Environment.NewLine);

        // Conversión implicita, SI posible si el receptor es de mayor tamaño en bits
        num2 = num1;

        // Conversión implicita, NO es posible si el receptor es de menor tamaño en bits
        // No se puede convertir implícitamente el tipo 'int' en 'byte'.
        // num1 = num2;

        // La opción es una conversión explicita, indicada por el programador
        // Es valida si el valor esta comprendido entre los valores de la varible receptora
        num2 = 32;
        num1 = (byte)num2;

        // Conversión explicita, utilizando los métodos del objeto CONVERT
        // Para valores fuera del rango de la variable receptora genera una excepción
        num2 = 32;
        num1 = Convert.ToByte(num2);
        num1 = Convert.ToByte(num3);

        Console.WriteLine("Después de las conversiones");
        Console.WriteLine($"Num 1 (byte): {num1} - Num 2 (int): {num2} - Num 3 (string): {num3}");

    }
} 
