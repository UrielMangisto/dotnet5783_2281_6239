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
                bool tryparse;
                int choice;
                int input;
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
                    tryparse = int.TryParse(Console.ReadLine(), out choice);                 

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
                            tryparse = int.TryParse(Console.ReadLine(), out choice);
                            
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
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    boOrder.DetailsOfOrderForManager(id);
                                    break;
                                case (int)BO.Enums.OrderChoice.DetailsOfOrderForCustomer:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    boOrder.DetailsOfOrderForCustomer(id);
                                    break;
                                case (int)BO.Enums.OrderChoice.ShippingUpdate:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    boOrder.ShippingUpdate(id);
                                    break;
                                case (int)BO.Enums.OrderChoice.UpdateDelivery:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    boOrder.UpdateDelivery(id);
                                    break;
                                case (int)BO.Enums.OrderChoice.Track:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    boOrder.Track(id);
                                    break;
                                default:
                                    throw new NotvalidException();
                                    
                            }
                            break;
                        case (int)BO.Enums.MainChoise.Cart:
                            Console.WriteLine(
                            @$"
press 1 to add Cart
2 to update Cart
3 to ConfirmationCart
                                      ");
                            tryparse = int.TryParse(Console.ReadLine(), out choice);
                            
                            switch (choice)
                            {
                                case (int)BO.Enums.CartChoise.addCart:
                                    cart = cartInput(cart);
                                    Console.WriteLine(
    @$" enter the id of the product ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    mycart = boCart.Add(cart, id);
                                    Console.WriteLine(mycart);
                                    break;
                                case (int)BO.Enums.CartChoise.updateCart:
                                    cart = cartInput(cart);
                                    Console.WriteLine(
    @$" enter the id of the product ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                  
                                    Console.WriteLine(
    @$" enter the new amount");
                                    tryparse = int.TryParse(Console.ReadLine(), out input);

                                    boCart.Update(cart, id, input);
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
                            tryparse = int.TryParse(Console.ReadLine(), out choice);
                            
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
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    boProduct.ProductDetailsForManager(id);
                                    break;
                                case (int)BO.Enums.ProductChoice.ProductDetailsForCostumer:
                                    Console.WriteLine(
    @$" enter the id of the order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
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
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    boProduct.Delete(id);
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
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    boProduct.RequestDetailsFromCostumer(id);
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
    