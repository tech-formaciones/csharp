﻿using System;
using System.Collections.Generic;

namespace Formacion.CSharp.ConsoleAppEF.Models;

public partial class Order_Subtotal
{
    public int OrderID { get; set; }

    public decimal? Subtotal { get; set; }
}
