﻿using System;
using System.Collections.Generic;

namespace Formacion.CSharp.ConsoleAppEF.Models;

public partial class Product_Sales_for_1997
{
    public string CategoryName { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public decimal? ProductSales { get; set; }
}
