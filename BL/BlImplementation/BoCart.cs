

using BlApi;
using BO;
using DalApi;
/// <summary>
/// the implementation of the cart
/// </summary>
public class BoCart : BlApi.ICart
{
    DalApi.IDal? dal = DalApi.Factory.Get();

    public BO.Cart Add(BO.Cart C, int id) 
    {
        try
        {
            DO.Product? Dproduct = new DO.Product?();
            Dproduct = dal?.Product.Get(id);
            if (Dproduct?.InStock <= 0)
                throw new NotInExistinStockException();
            if (C.OrderItems == null)
            {

                BO.OrderItem BorderItem1 = new BO.OrderItem();
                Random randNum1 = new Random();
                int nId1 = randNum1.Next(10000);
                BorderItem1.Id = nId1;
                BorderItem1.ProductId = id;
                BorderItem1.ItemName = Dproduct?.Name;
                BorderItem1.Price = Dproduct?.Price ?? throw new mayBeNullException();
                BorderItem1.Amount = 1;
                BorderItem1.TotalPrice = BorderItem1.Price;
                C.OrderItems = new List<OrderItem?>();
                C.OrderItems.Add(BorderItem1);
                C.TotalPrice += Dproduct?.Price ?? throw new mayBeNullException();
                return C;
            }
            foreach (var pro in C.OrderItems)
            {
                if (pro?.Id == id)
                {
                    pro.Amount++;
                    C.TotalPrice += pro.Price;
                    return C;
                }
            }
            //if it exists in the stock but not in the cart:
            BO.OrderItem BorderItem = new BO.OrderItem();
            Random randNum = new Random();
            int nId = randNum.Next(10000);
            BorderItem.Id = nId;
            BorderItem.ProductId = id;
            BorderItem.ItemName = Dproduct?.Name ?? throw new mayBeNullException();
            BorderItem.Price = Dproduct?.Price ?? throw new mayBeNullException();
            BorderItem.Amount = 1;
            BorderItem.TotalPrice = BorderItem.Price;
            C.OrderItems.Add(BorderItem);
            C.TotalPrice += Dproduct?.Price ?? throw new mayBeNullException();
            return C;
        }
        catch (DO.NotFoundException)
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
        catch (DO.DalConfigException)
        {
            throw new Exception();
        }
        catch
        {
            throw new Exception();
        }
    }

    public BO.Cart Update(BO.Cart C, int ID, int amount)
    {
        try
        {
            DO.Product Dproduct = new DO.Product();
            foreach (var pro in C.OrderItems)
            {
                if (pro?.Id == ID)
                {
                    if (amount == 0)
                    {
                        C.OrderItems.Remove(pro);
                        C.TotalPrice -= pro.Price * pro.Amount;
                        return C;
                    }
                    int diff = amount - pro.Amount;
                    if (diff > 0)
                    {
                        if (Dproduct.InStock < diff)
                            throw new NotInExistinStockException();//there are no products in the stock
                        pro.Amount = amount;
                        C.TotalPrice += pro.Price * diff;
                        return C;
                    }
                    if (diff < 0)
                    {
                        pro.Amount = amount;
                        C.TotalPrice += pro.Price * diff;
                        return C;
                    }
                }
            }
            return C;
        }
        catch (DO.NotFoundException)
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
        catch (DO.DalConfigException)
        {
            throw new Exception();
        }
        catch
        {
            throw new Exception();
        }

    }
 
    public void Confirmation(BO.Cart C)
    {
        try
        {
            if (C.CostumerName == null)
                throw new InCorrectDataException();
            if (C.CostumerEmail == null || !C.CostumerEmail.Contains('@'))
                throw new InCorrectDataException();
            if (C.CostumerAddress == null)
                throw new InCorrectDataException();

            DO.Order Dorder = new DO.Order();
            //Dorder.ID = dal.Order.GetAll().Last().ID + 1;
            Dorder.CostumerName = C.CostumerName;
            Dorder.CostumerEmail = C.CostumerEmail;
            Dorder.CostumerAddress = C.CostumerAddress;
            Dorder.OrderDate = DateTime.Now;
            Dorder.ShipDate = null;
            Dorder.DeliveryDate = null;
            Dorder.ID = dal.Order.Add(Dorder);
            List<BO.OrderItem> BorderItems = new List<BO.OrderItem>();
            foreach (BO.OrderItem item in C.OrderItems)
            {
                DO.Product? Dproduct = new DO.Product?();
                Dproduct = dal.Product.Get(item.Id);
                if (Dproduct?.InStock <= 0)
                    throw new NotvalidException();
                if (item?.Amount <= 0)
                    throw new NotvalidException();
                BorderItems.Add(item);
            }
            foreach (BO.OrderItem item in BorderItems)
            {
                DO.Product product = new DO.Product();
                product = dal.Product.Get(item.Id) ?? throw new mayBeNullException();
                DO.OrderItem tempItem = new DO.OrderItem();
                tempItem.OrderID = Dorder.ID;
                dal.OrderItem.Add(tempItem);
                product.InStock -= item.Amount;
            }
        }
        catch (DO.NotFoundException)
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
        catch (DO.DalConfigException)
        {
            throw new Exception();
        }
        catch
        {
            throw new Exception();
        }
    }
}
