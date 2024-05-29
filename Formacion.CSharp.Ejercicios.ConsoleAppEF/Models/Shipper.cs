using System;
using System.Collections.Generic;

namespace Formacion.CSharp.Ejercicios.ConsoleAppEF.Models;

public partial class Shipper
{
    public int ShipperID { get; set; }

    public string CompanyName { get; set; } = null!;

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
