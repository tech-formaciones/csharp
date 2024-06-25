using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Formacion.CSharp.ConsoleApp5
{
    /// <summary>
    /// Creamos un clase que hereda de LIST, y añadimos funcionalidad extras.
    /// Los objeto de ListExtend tiene la misma funcionalidad que los objetos creados
    /// a partir de LIST y además la funcionalidad extra que incorporamos.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListExtend<T> : List<T>
    {
        public void OutputAll()
        { 
            foreach (var item in this) Console.WriteLine(item.ToString());
        }

        public override string ToString()
        {
            return $"Colección con {this.Count} elementos.";
        }
    }


    /////////////////////////////////////////////////////////////////////////////////////////

    public abstract class Animal
    {
        /// <summary>
        /// Propiedad que se hereda por las clases derivadas o hijas
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Propiedad abstracta que necesita obligatoriamente ser implementas en clases derivas o hijas
        /// </summary>
        public abstract string Especie { get; set; }

        /// <summary>
        /// Método que se hereda por las clases derivadas o hijas
        /// </summary>
        public void MetodoA() => Console.WriteLine("Método A, clase Animal");

        /// <summary>
        /// Método abstracto que necesita obligatoriamente ser implementas en clases derivas o hijas
        /// </summary>
        public abstract void MetodoB();
    }

    public class Anfibio : Animal
    {
        protected string especie = "Anfibio";
        public override string Especie { get => especie; set => especie = value; }
        //public override string Especie
        //{
        //    get { return especie; }
        //    set { especie = value; }
        //}

        public override void MetodoB()
        {
            Console.WriteLine("Método B, clase Anfibio");
        }
    }

    public class Reptil : Animal
    {
        protected string especie = "Reptil";
        public override string Especie { get => especie; set => especie = value; }
        public override void MetodoB() => Console.WriteLine("Método B, clase Reptil");
        public void MetodoC() => Console.WriteLine("Método C, clase Reptil");
        
    }


    /////////////////////////////////////////////////////////////////////////////////////////

    public interface IVehiculo
    {
        public string Nombre { get; set; }   
        public int Ruedas { get; set; }
        public void Iniciar();
        public void Parar();
    }

    public class Coche : IVehiculo
    {
        public string Nombre { get ; set; }
        public int Ruedas { get; set; }
        public void Iniciar() => Console.WriteLine("Coche, Iniciar");
        public void Parar() => Console.WriteLine("Coche, Parar");
    }

    public class Avion : IVehiculo
    {
        public string Nombre { get; set; }
        public int Ruedas { get; set; }
        public void Iniciar() => Console.WriteLine("Avión, Iniciar");
        public void Parar() => Console.WriteLine("Avión, Parar");
        public void Despegar() => Console.WriteLine("Avión, Parar");
    }

    /////////////////////////////////////////////////////////////////////////////////////////

    public sealed class Persona
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }

        public void MetodoA() => Console.WriteLine("Método A, clase Persona");
    }

    // No se puede heredar de Persona porque es una clase sealed (sellada) 

    // public class Medico : Persona
    // { }

    public class Alumno
    { 
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }

        public override string ToString()
        {
            return $"{this.Nombre} {this.Apellidos}";
        }
    }
}
