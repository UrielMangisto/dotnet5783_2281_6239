using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;
public class NotFoundException : Exception
{
    public NotFoundException()
    {
        Console.WriteLine("NOT FOUND");
    }
}
public class alreadyexist : Exception
{
    public alreadyexist()
    {
        Console.WriteLine("ALREADY EXIST");
    }
}

public class NotvalidException : Exception
{
    public NotvalidException(string myException)
    {
        Console.WriteLine(myException);
    }
    public NotvalidException()
    {
        Console.WriteLine("Not valid");
    }
}