using BlApi;
using BlImplementation;
using BO;
using System.Collections.Generic;

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
                

                BlApi.IBl? bl = BlApi.Factory.Get();

               var a = bl.Product.catalogGrouping(bl.Product.CatalogRequest());
                Console.WriteLine("List of the categories of the product list:");
                foreach (var c in a)
                {
                    Console.WriteLine(c?.Category);
                }


               // BoCart boCart = (BoCart)Enums.bl.Cart;
               // BoOrder boOrder = (BoOrder)Enums.bl.Order;
               // BoProduct boProduct = (BoProduct)Enums.bl.Product;
               //  BoCart boCart = new BoCart();
               // BoOrder boOrder = new BoOrder();
               // BoProduct boProduct = new BoProduct();
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
press 1 to get Order list
2 to Details of Order for manager
3 to Details of Order for customer
4 to Shipping Update
5 to Update Delivery
6 to Track

                                      ");
                            tryparse = int.TryParse(Console.ReadLine(), out choice);

                            switch (choice)
                            {
                                case (int)BO.Enums.OrderChoice.GetOrderList:

                                    foreach (var order in bl.Order.GetOrderList())
                                    {
                                        Console.WriteLine(order);
                                    }
                                    break;
                                case (int)BO.Enums.OrderChoice.DetailsOfOrderForManager:
                                    Console.WriteLine(
    @$" enter the id of the Order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.Order.DetailsOfOrderForManager(id));
                                    break;
                                case (int)BO.Enums.OrderChoice.DetailsOfOrderForCustomer:
                                    Console.WriteLine(
    @$" enter the id of the Order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.Order.DetailsOfOrderForCustomer(id));
                                    break;
                                case (int)BO.Enums.OrderChoice.ShippingUpdate:
                                    Console.WriteLine(
    @$" enter the id of the Order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.Order.ShippingUpdate(id));
                                    break;
                                case (int)BO.Enums.OrderChoice.UpdateDelivery:
                                    Console.WriteLine(
    @$" enter the id of the Order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.Order.UpdateDelivery(id));
                                    break;
                                case (int)BO.Enums.OrderChoice.Track:
                                    Console.WriteLine(
    @$" enter the id of the Order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.Order.Track(id));
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
    @$" enter the id of the Product ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    mycart = bl.Cart.Add(cart, id);
                                    Console.WriteLine(mycart);
                                    break;
                                case (int)BO.Enums.CartChoise.updateCart:
                                    cart = cartInput(cart);
                                    Console.WriteLine(
    @$" enter the id of the Product ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);

                                    Console.WriteLine(
    @$" enter the new amount");
                                    tryparse = int.TryParse(Console.ReadLine(), out input);

                                    bl.Cart.Update(cart, id, input);
                                    break;
                                case (int)BO.Enums.CartChoise.ConfirmationCart:
                                    cart = cartInput(cart);
                                    bl.Cart.Confirmation(cart);
                                    break;
                                default:
                                    throw new NotvalidException();
                            }
                            break;
                        case (int)BO.Enums.MainChoise.Product:
                            Console.WriteLine(
                         @$"
press 1 to Get Product list
2 to Product details for manager
3 to Product details for costumer
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
                                    bl.Product.GetProductList();
                                    foreach (var produc in bl.Product.GetProductList())
                                    {
                                        Console.WriteLine(produc);
                                    }
                                    break;
                                case (int)BO.Enums.ProductChoice.ProductDetailsForManager:
                                    Console.WriteLine(
    @$" enter the id of the Order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.Product.ProductDetailsForManager(id));
                                    break;
                                case (int)BO.Enums.ProductChoice.ProductDetailsForCostumer:
                                    Console.WriteLine(
    @$" enter the id of the Order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    cart = cartInput(cart);
                                    Console.WriteLine(bl.Product.ProductDetailsForCostumer(id, cart));
                                    break;
                                case (int)BO.Enums.ProductChoice.Add:
                                    product = ProductInput(product);
                                    bl.Product.Add(product);
                                    break;
                                case (int)BO.Enums.ProductChoice.Delete:
                                    Console.WriteLine(
    @$" enter the id of the Order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    bl.Product.Delete(id);
                                    break;
                                case (int)BO.Enums.ProductChoice.Update:
                                    product = ProductInput(product);
                                    bl.Product.Update(product);
                                    break;
                                case (int)BO.Enums.ProductChoice.CatalogRequest:
                                    foreach (var p in bl.Product.CatalogRequest())
                                    {
                                        Console.WriteLine(p);
                                    }
                                    break;
                                case (int)BO.Enums.ProductChoice.RequestDetailsFromCostumer:
                                    Console.WriteLine(
    @$" enter the id of the Order ");
                                    tryparse = int.TryParse(Console.ReadLine(), out id);
                                    Console.WriteLine(bl.Product.RequestDetailsFromCostumer(id));
                                    break;
                                default:
                                    throw new NotvalidException();
                            }
                            break;

                    }
                }
                while (choice != 0);

            }
            catch (NotFoundException)
            {

            }
            catch (AlreadyExistException)
            {

            }
            catch (NotvalidException)
            {

            }
            catch (RequestProductFaildException)
            {

            }
            catch (InCorrectDataException)
            {

            }
            catch (ProductExistInOrderException)
            {

            }
            catch (NotExistInStockException)
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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
            product.Category = (BO.Enums.Category1)(cat);

            Console.WriteLine("enter Product price");
            product.Price = int.Parse(Console.ReadLine());

            Console.WriteLine("how many products are in stock?");
            product.InStock = int.Parse(Console.ReadLine());

            return product;

        }



    }
        

}
    