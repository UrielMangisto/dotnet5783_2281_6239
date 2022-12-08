using BlApi;
using BlImplementation;
using BO;

namespace BlTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int choice;
                int id;
                Cart cart = new Cart();
                Cart mycart = new Cart();
                Product product = new Product();

                BoCart boCart = new BoCart();
                BoOrder boOrder = new BoOrder();
                BoProduct boProduct = new BoProduct();
                do
                {
                    Console.WriteLine(
               @$"
press 0 to exit
1 to Order 
2 to  Cart
3 to Product
                                            ");
                    choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case (int)BO.Enums.MainChoise.Order:
                            Console.WriteLine(
                            @$"
press 1 to get order list
2 to Details of order for manager
3 to Details of Order for customer
4 to Shipping Update
5 to Update Delivery
6 to Track

                                      ");
                            choice = int.Parse(Console.ReadLine());
                            switch (choice)
                            {
                                case (int)BO.Enums.OrderChoice.GetOrderList:

                                    foreach (var order in boOrder.GetOrderList())
                                    {
                                        Console.WriteLine(order);
                                    }
                                    break;
                                case (int)BO.Enums.OrderChoice.DetailsOfOrderForManager:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    boOrder.DetailsOfOrderForManager(int.Parse(Console.ReadLine()));
                                    break;
                                case (int)BO.Enums.OrderChoice.DetailsOfOrderForCustomer:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    boOrder.DetailsOfOrderForCustomer(int.Parse(Console.ReadLine()));
                                    break;
                                case (int)BO.Enums.OrderChoice.ShippingUpdate:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    boOrder.ShippingUpdate(int.Parse(Console.ReadLine()));
                                    break;
                                case (int)BO.Enums.OrderChoice.UpdateDelivery:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    boOrder.UpdateDelivery(int.Parse(Console.ReadLine()));
                                    break;
                                case (int)BO.Enums.OrderChoice.Track:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    boOrder.Track(int.Parse(Console.ReadLine()));
                                    break;
                                default:
                                    throw new NotvalidException();
                                    break;
                            }
                            break;
                        case (int)BO.Enums.MainChoise.Cart:
                            Console.WriteLine(
                            @$"
press 1 to add Cart
2 to update Cart
3 to ConfirmationCart
                                      ");
                            choice = int.Parse(Console.ReadLine());
                            switch (choice)
                            {
                                case (int)BO.Enums.CartChoise.addCart:
                                    cart = cartInput(cart);
                                    Console.WriteLine(
    @$" enter the id of the product ");
                                    mycart = boCart.Add(cart, int.Parse(Console.ReadLine()));
                                    Console.WriteLine(mycart);
                                    break;
                                case (int)BO.Enums.CartChoise.updateCart:
                                    cart = cartInput(cart);
                                    Console.WriteLine(
    @$" enter the id of the product ");
                                    id = int.Parse(Console.ReadLine());
                                    Console.WriteLine(
    @$" enter the new amount");

                                    boCart.Update(cart, id, int.Parse(Console.ReadLine()));
                                    break;
                                case (int)BO.Enums.CartChoise.ConfirmationCart:
                                    cart = cartInput(cart);
                                    boCart.Confirmation(cart);
                                    break;
                                default:
                                    throw new NotvalidException();
                            }
                            break;
                        case (int)BO.Enums.MainChoise.Product:
                            Console.WriteLine(
                         @$"
press 1 to Get product list
2 to product details for manager
3 to product details for costumer
4 to Add
5 to Delete
6 to Update
7 to CatalogRequest
8 to RequestDetailsFromCostumer
                                      ");
                            choice = int.Parse(Console.ReadLine());
                            switch (choice)
                            {
                                case (int)BO.Enums.ProductChoice.GetProductList:
                                    boProduct.GetProductList();
                                    foreach (var produc in boProduct.GetProductList())
                                    {
                                        Console.WriteLine(produc);
                                    }
                                    break;
                                case (int)BO.Enums.ProductChoice.ProductDetailsForManager:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    boProduct.ProductDetailsForManager(int.Parse(Console.ReadLine()));
                                    break;
                                case (int)BO.Enums.ProductChoice.ProductDetailsForCostumer:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    id = int.Parse(Console.ReadLine());
                                    cart = cartInput(cart);
                                    boProduct.ProductDetailsForCostumer(id, cart);
                                    break;
                                case (int)BO.Enums.ProductChoice.Add:
                                    product = ProductInput(product);
                                    boProduct.Add(product);
                                    break;
                                case (int)BO.Enums.ProductChoice.Delete:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    boProduct.Delete(int.Parse(Console.ReadLine()));
                                    break;
                                case (int)BO.Enums.ProductChoice.Update:
                                    product = ProductInput(product);
                                    boProduct.Update(product);
                                    break;
                                case (int)BO.Enums.ProductChoice.CatalogRequest:
                                    boProduct.CatalogRequest();
                                    break;
                                case (int)BO.Enums.ProductChoice.RequestDetailsFromCostumer:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    boProduct.RequestDetailsFromCostumer(int.Parse(Console.ReadLine()));
                                    break;
                                default:
                                    throw new NotvalidException();
                            }
                            break;

                    }
                }
                while (choice != 0);

            }
            catch(NotFoundException)
            {

            }
            catch(AlreadyExistException)
            {

            }
            catch (NotvalidException)
            {

            }
            catch(RequestProductFaildException)
            {

            }
            catch(InCorrectDataException)
            {

            }
            catch (ProductExistInOrderException)
            {

            }
            catch(NotInExistinStockException)
            {

            }
            

        }
        //פה תוסיף פונקציות עזר
        private static Cart cartInput(Cart cart)
        {
            Console.WriteLine("enter Costumer name");
            cart.CostumerName = (Console.ReadLine());

            Console.WriteLine("enter Costumer Email");
            cart.CostumerEmail = (Console.ReadLine());

            Console.WriteLine("enter Costumer Address");
            cart.CostumerAddress = (Console.ReadLine());

            cart.TotalPrice = 0;//we change it in add
            return cart;
        }
        private static Product ProductInput(Product product)
        {
            int cat;

            Console.WriteLine("enter Product name");
            product.Name = (Console.ReadLine());

            Console.WriteLine("enter category");
            int.TryParse(Console.ReadLine(), out cat);
            product.Category = (DO.Category)(cat);

            Console.WriteLine("enter Product price");
            product.Price = int.Parse(Console.ReadLine());

            Console.WriteLine("how many products are in stock?");
            product.InStock = int.Parse(Console.ReadLine());

            return product;

        }



    }
        

}
    