using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

namespace BlImplementation;
/// <summary>
/// the interfaces of the Implementations of the logic entities
/// </summary>
internal class BL : IBl
{
    public IOrder Order { get; } = new BoOrder();
    public IProduct Product { get; } = new BoProduct();
    public ICart Cart { get; } = new BoCart();
}
