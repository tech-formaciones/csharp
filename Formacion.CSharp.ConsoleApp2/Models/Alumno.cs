using System.Diagnostics;

namespace Formacion.CSharp.ConsoleApp2.Models;

public class Alumno
{
    // Miembro: Variables (suelen ser siempre privadas)
    private string nombre;
    private int edad;

    // Miembro: Propiedades que se comporta como una variable pública
    public string Apellidos { get; set; }

    public Dias DiaTutoria { get; set; }
    public Estados Estado { get; set; }

    // Miembro: Propiedades (suelen y deberían de ser públicas)
    public string Nombre
    {
        // Retornar la información después de transformarla
        get 
        { 
            return nombre.Trim().ToLower(); 
        }
        set 
        { 
            if(value.Length < 2) nombre = "";
            else nombre = value; 
        }
    }
    public int Edad
    {
        get { return edad; }
        set
        {
            if(value < 1 || value > 120) edad = 0;
            else edad = value;
        }
    }

    // Miembro: Propiedad de solo lectura, no asociada a una variable
    public string NombreCompleto
    {
        get 
        {        
            return $"{this.nombre} {this.Apellidos}";
        }
    }

    // Miembro: Propiedad de solo escritura
    public int CambiaEdad
    {
        set
        {
            if (value < 1 || value > 120) edad = 0;
            else edad = value;
        }
    }

    // Miembro: Métodos con tipo VOID, no retorna nada
    public void MetodoUno()
    {

    }

    // Miembro: Métodos con tipo de datos (Ejemplo un bool), que siempre retorna ese tipo de dato
    public bool MetodoDos()
    {
        return true;
    }

    public bool MetodoTres(int param1, string param2, float param3 = 0, string param4 = "valor por defecto")
    {
        return true;
    }


    // Miembro: Método constructor, se ejecuata cuando se instancia el objeto
    // Es públic, no tiene tipo (no retorna nada) y se llama igual que la clase
    public Alumno()
    {

    }

    // Sobrecarga del método constructor
    public Alumno(string nombre, string apellidos)
    {
        this.nombre = nombre;
        this.Apellidos = apellidos;
    }

    // Sobrecarga del método constructor
    public Alumno(string nombre, int edad)
    {
        this.nombre = nombre;
        this.edad = edad;
    }


    // Miembro: Método destructor.
    // No se le puede llamar, se ejecuta automáticamente
    // No tiene parámetros, no tiene tipo y se llama igual que la clase
    // Comienza por ~ (Alt+0126 o AltGr+4)
    ~Alumno()
    {
        Debug.WriteLine("Se ejecuto el destructor de Alumno");
        this.nombre = null;
        this.Apellidos = null;
        this.edad = 0;
    }
}