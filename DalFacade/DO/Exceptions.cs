using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DO;
public class NotFoundException : Exception
{
    public NotFoundException()
    {
        Console.WriteLine("NOT FOUND");
    }
    public NotFoundException(int id, string massge) : base($"Bo_Exception:  the id {id} not found in the database of {massge}.")
    {

    }
    public NotFoundException(string massge) : base(massge)
    {

    }
    public NotFoundException(string massge, Exception inner) : base(massge, inner)
    {

    }
    public NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}

[Serializable]
public class AlreadyExistException : Exception
{
    public AlreadyExistException()
    {
        Console.WriteLine("ALREADY EXIST");
    }
    public AlreadyExistException(int id, string massge) : base($"Bo_Exception:  the id {id} already exists in the database of {massge}.")
    {

    }
    public AlreadyExistException(string massge) : base(massge)
    {

    }
    public AlreadyExistException(string massge, Exception inner) : base(massge, inner)
    {

    }
    public AlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}

[Serializable]
public class NotvalidException : Exception
{
    public NotvalidException()
    {
        Console.WriteLine("Not valid");
    }
    public NotvalidException(string massge) : base(massge)
    {

    }
    public NotvalidException(string massge, Exception inner) : base(massge, inner)
    {

    }
    public NotvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}

[Serializable]
public class RequestProductFaildException : Exception
{
    public RequestProductFaildException()
    {
        Console.WriteLine("request product faild");
    }
    public RequestProductFaildException(string massge) : base(massge)
    {

    }
    public RequestProductFaildException(string massge, Exception inner) : base(massge, inner)
    {

    }
    public RequestProductFaildException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}

[Serializable]
public class InCorrectDataException : Exception
{
    public InCorrectDataException()
    {
        Console.WriteLine("Incorrect Data");
    }
    public InCorrectDataException(string massge) : base(massge)
    {

    }
    public InCorrectDataException(string massge, Exception inner) : base(massge, inner)
    {

    }
    public InCorrectDataException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}

[Serializable]
public class ProductExistInOrderException : Exception
{
    public ProductExistInOrderException()
    {
        Console.WriteLine("Product Exist In Order");
    }
    public ProductExistInOrderException(string massge) : base(massge)
    {

    }
    public ProductExistInOrderException(string massge, Exception inner) : base(massge, inner)
    {

    }
    public ProductExistInOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}

[Serializable]
public class NotExistInStockException : Exception
{
    public NotExistInStockException()
    {
        Console.WriteLine("Not In Exist in Stock Exception");
    }
    public NotExistInStockException(string massge) : base(massge)
    {

    }
    public NotExistInStockException(string massge, Exception inner) : base(massge, inner)
    {

    }
    public NotExistInStockException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}

[Serializable]
public class mayBeNullException : Exception
{
    public mayBeNullException()
    {
        Console.WriteLine("may Be Null");
    }
    public mayBeNullException(string massge) : base(massge)
    {

    }
    public mayBeNullException(string massge, Exception inner) : base(massge, inner)
    {

    }
    public mayBeNullException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}
/*

catch(DO.NotFoundException)
{
    throw new BO.NotFoundException();
}
catch (DO.AlreadyExistException)
{
    throw new BO.AlreadyExistException();
}
catch (DO.NotvalidException)
{
    throw new BO.NotvalidException();
}
catch (DO.RequestProductFaildException)
{
    throw new BO.RequestProductFaildException();
}
catch (DO.InCorrectDataException)
{
    throw new BO.InCorrectDataException();
}
catch (DO.ProductExistInOrderException)
{
    throw new BO.ProductExistInOrderException();
}
catch (DO.NotInExistinStockException)
{
    throw new BO.NotInExistinStockException();
}
catch (DO.mayBeNullException)
{
    throw new BO.mayBeNullException();
}
catch(DO.DalConfigException)
{
    throw new Exception();
}
catch
{
    throw new Exception();
}
*/