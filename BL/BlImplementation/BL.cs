using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation;

public class BL : IBl
{
    public static IBl instance { get; } = new BL();
    public IOrder Order { get; }
    public IProduct Product { get; }
    public ICart Cart { get; }
    public BL()
    {
        Order = new BoOrder();
        Product = new BoProduct();
        Cart = new BoCart();
    }
}
