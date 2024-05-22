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
        SentenciasDeControl();
        
        // while (true)
        // {
        //     Console.Clear();
        //     Console.ForegroundColor = ConsoleColor.Yellow;
        //     Console.WriteLine("".PadRight(56, '*'));
        //     Console.WriteLine("*  DEMO Y EJERCICIOS".PadRight(55) + "*");
        //     Console.WriteLine("".PadRight(56, '*'));
        //     Console.WriteLine("*".PadRight(55) + "*");
        //     Console.WriteLine("*  1. Declaración de Variables".PadRight(55) + "*");
        //     Console.WriteLine("*  2. Conversión de Variables".PadRight(55) + "*");
        //     Console.WriteLine("*  9. Salir".PadRight(55) + "*");
        //     Console.WriteLine("*".PadRight(55) + "*");
        //     Console.WriteLine("".PadRight(56, '*'));

        //     Console.WriteLine(Environment.NewLine);
        //     Console.Write("   Opción: ");

        //     Console.ForegroundColor = ConsoleColor.Cyan;

        //     int.TryParse(Console.ReadLine(), out int opcion);
        //     switch (opcion)
        //     {
        //         case 1:
        //             Console.Clear();
        //             DeclaracionVariables();
        //             break;
        //         case 2:
        //             Console.Clear();
        //             ConversionVariables();
        //             break;                    
        //         case 9:
        //             Console.ForegroundColor = ConsoleColor.Gray;
        //             return;
        //         default:
        //             Console.ForegroundColor = ConsoleColor.Red;
        //             Console.WriteLine(Environment.NewLine + $"La opción {opcion} no es valida.");
        //             break;
        //     }

        //     Console.WriteLine(Environment.NewLine);
        //     Console.ForegroundColor = ConsoleColor.White;
        //     Console.WriteLine("Pulsa una tecla para continuar...");
        //     Console.ReadKey();
        // }
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
        Console.WriteLine($"Num 1 (byte): {num1} - Num 2 (int): {num2} - Num 3 (string): {num3}\n");


        ///////////////////////////////////////////////////////////////                
        // Transformaciones de STRING a cualquier tipo de dato númerico
        ///////////////////////////////////////////////////////////////                

        // Utilizando los métodos del objeto CONVERT
        num1 = Convert.ToByte(num3);
        num2 = Convert.ToInt32(num3);

        // Conversión explicita, utilizando el método Parse
        num1 = byte.Parse(num3);

        // Conversión explicita, utilizando el método TryParse
        byte.TryParse(num3, out num1);

        // El método .TryParse retorna True/False dependiendo de si la transformación es posible
        // El resultao de la transformación de almacena en num4, siendo 0 si la tranformación no es posible
        num3 = "102";
        int num4;
        bool result = int.TryParse(num3, out num4);

        // En este ejemplo solo comprobamos si la transformación es posible.
        // Mediante [out _] indicamos que no queremos el resultado de la transformación  
        var esEntero = int.TryParse(num3, out _);

        Console.WriteLine($"Resultado: {result} - Valor Num 4: {num4}");
        Console.WriteLine($"Resultado Num 5: {esEntero}");
    }


    static void SentenciasDeControl()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;

        // Uso de IF/ELSE
        Reserva reserva = new Reserva();
        
        Console.Write("ID de la Reserva: ");
        reserva.id = Console.ReadLine();

        Console.Write("Nombre del Cliente: ");
        reserva.cliente = Console.ReadLine();

        Console.Write("Tipo de Reserva: (100, 200, 300 o 400) ");
        // Opcion A
        string respuesta = Console.ReadLine();
        int.TryParse(respuesta, out reserva.tipo);

        // Opcion B
        //int.TryParse(Console.ReadLine(), out reserva.tipo);

        Console.Write("Es Fumador ? (Sí o No) ");
        string fumador = Console.ReadLine();

        // Opción 1, utilizando IF/ELSE
        if (fumador.ToLower().Trim() == "si" || fumador.ToLower().Trim() == "sí")
        {
            reserva.fumador = true;
        }
        else
        {
            reserva.fumador = false;
        }

        // Opción 2, utilizando IF/ELSE IF/ELSE
        if (fumador.ToLower().Trim() == "si" || fumador.ToLower().Trim() == "sí")
        {
            reserva.fumador = true;
        }
        else if (fumador.ToLower().Trim() == "no")
        {
            reserva.fumador = false;
        }
        else 
        {
            reserva.fumador = false;
            Console.WriteLine($"El valor {fumador} no es valido, pero se asigna habitación de no fumador.");
        }

        // Opción 3, utilizando IF/ELSE (sin bloques)
        if (fumador.ToLower().Trim() == "si" || fumador.ToLower().Trim() == "sí") reserva.fumador = true;        
        else reserva.fumador = false;

        // Opción 4a, asignación condicional con ? :
        // Siempre un IF/ELSE y asignación de un valor (no ejecutan sentencias)
        reserva.fumador = (fumador.ToLower().Trim() == "si" || fumador.ToLower().Trim() == "sí") ? true : false;

        // Opción 4b, asignación condicional con ? :
        // Siempre un IF/ELSE y asignación de un valor (no ejecutan sentencias)
        reserva.fumador = (fumador.ToLower().Trim() == "si" || fumador.ToLower().Trim() == "sí");

        // Opción 5, utilizando SWITCH
        switch(fumador.ToLower().Trim())
        {
            case "si":
                reserva.fumador = true;
                break;
            case "sí":
                reserva.fumador = true;
                break;
            case "no":
                reserva.fumador = false;
                break;
            default:
                reserva.fumador = false;
                Console.WriteLine($"El valor {fumador} no es valido, pero se asigna habitación de no fumador.");
                break;
        }
        
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("ID Reserva: ".PadRight(15, ' '));
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"{reserva.id}");

    }

}