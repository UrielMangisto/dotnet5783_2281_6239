using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;
public class NotFoundException : Exception
{
    public NotFoundException(string myException)
    {
        Console.WriteLine(myException);
    }
}
public class alreadyexist : Exception
{
    public alreadyexist(string myException)
    {
        Console.WriteLine(myException);
    }
}