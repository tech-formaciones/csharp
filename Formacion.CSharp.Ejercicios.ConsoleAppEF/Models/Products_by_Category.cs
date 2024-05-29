using System;
using System.Collections.Generic;

namespace Formacion.CSharp.Ejercicios.ConsoleAppEF.Models;

public partial class Products_by_Category
{
    public string CategoryName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? QuantityPerUnit { get; set; }

    public short? UnitsInStock { get; set; }

    public bool Discontinued { get; set; }
}
