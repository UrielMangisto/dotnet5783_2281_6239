using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace DalApi;
/// <summary>
/// abstract of orderItemm
/// </summary>
public interface IOrderItem:ICrud<OrderItem>
{
    IEnumerable<OrderItem> GetItemsByOrder(int orderId);
    OrderItem specificItemGet(int idOfProduct, int idOfOrder);
}
