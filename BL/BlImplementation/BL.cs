﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

public class BL : IBl
{
    public static IBl instance { get; } = new BL();
    public BL() { }
    public IOrder Order { get; set; } = new BoOrder();
    public IProduct Product { get; set; } = new BoProduct();
    public ICart Cart { get; set; } = new BoCart();
}
