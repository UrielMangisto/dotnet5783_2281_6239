﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Enums
{
    public enum OrderStatus { Confirmed, Sent, Delivered, NullStatus };
    public enum Category {All, Action, Fantasy, Romanticism, Kids, Comics };
    public enum Category1 { Action = 1, Fantasy, Romanticism, Kids, Comics };
    public enum ClientName { Reuven, Simon, Levi, Juda, David, Jakob };
    public enum ClientAddress {Rehovot, Ramle, Petah_Tikva, Jerusalem, Beit_Shemesh, Lod };
    public enum MainChoise { Order = 1, Cart, Product };
    public enum OrderChoice { GetOrderList = 1, DetailsOfOrderForManager, DetailsOfOrderForCustomer, ShippingUpdate, UpdateDelivery, Track };
    public enum CartChoise { addCart = 1, updateCart, ConfirmationCart };
    public enum ProductChoice { GetProductList = 1, ProductDetailsForManager, ProductDetailsForCostumer, Add, Delete, Update , CatalogRequest , RequestDetailsFromCostumer ,};
}