using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// all of the exceptions of the BL
/// </summary>
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

public class NotImplementedException : Exception
{
    public NotImplementedException(string myException)
    {
        Console.WriteLine(myException);
    }
    public NotImplementedException()
    {
        Console.WriteLine("Not Implemented");
    }
}