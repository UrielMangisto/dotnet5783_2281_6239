using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
public class Enums
{
    public enum OrderStatus { Confirmed, Sent, Delivered, NullStatus };
    public enum Category { Action, Fantasy, Romanticism, Kids, Comics };
    public enum ClientName { Reuven, Simon, Levi, Juda, David, Jakob };
    public enum ClientAddress { };
    public enum MainChoise { order = 1, orderItem, product };
    public enum OrderChoice { addOrder = 1, deleteOrder, updateOrder, getOrder, getAllOrder };
    public enum orderItemChoise { addOrderItem = 1, deleteOrderItem, updateOrderItem, getOrderItem, getSpecificItem, getItemsByOrder, getAllItems };
    public enum ProductChoice { addProduct = 1, deleteProduct, updateProduct, getProduct, getAllProduct };
}