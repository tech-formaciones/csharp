namespace Formacion.CSharp.ConsoleApp2.Models;

/// <summary>
/// Objeto Alumno para ejercicios y demos
/// </summary>
public struct Alumno2
{
    private string nombre;
    public string Nombre
    {
        get
        {
            return nombre.Trim().ToLower();
        }
        set
        {
            if (value.Length < 2) nombre = "";
            else nombre = value;
        }
    }
    public string Apellidos { get; set; }
    public int Edad { get; set; }
}