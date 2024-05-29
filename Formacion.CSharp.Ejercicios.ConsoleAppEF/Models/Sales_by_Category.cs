using System;
using System.Collections.Generic;

namespace Formacion.CSharp.Ejercicios.ConsoleAppEF.Models;

public partial class Sales_by_Category
{
    public int CategoryID { get; set; }

    public string CategoryName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal? ProductSales { get; set; }
}
