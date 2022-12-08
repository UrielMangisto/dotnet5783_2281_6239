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
    public NotFoundException()
    {
        Console.WriteLine("NOT FOUND");
    }
}
public class AlreadyExistException : Exception
{
    public AlreadyExistException()
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
public class RequestProductFaildException : Exception
{
    public RequestProductFaildException()
    {
        Console.WriteLine("request product faild");
    }
}
public class InCorrectDataException : Exception
{
    public InCorrectDataException()
    {
        Console.WriteLine("Incorrect Data");
    }
}
public class ProductExistInOrderException : Exception
{
    public ProductExistInOrderException()
    {
        Console.WriteLine("Product Exist In Order");
    }
}
public class NotInExistinStockException : Exception
{
    public NotInExistinStockException()
    {
        Console.WriteLine("Not In Exist in Stock Exception");
    }
}