﻿using System;
using System.Collections.Generic;

namespace Formacion.CSharp.ConsoleAppEF.Models;

public partial class CustomerDemographic
{
    public string CustomerTypeID { get; set; } = null!;

    public string? CustomerDesc { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
