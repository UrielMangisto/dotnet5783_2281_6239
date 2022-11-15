

namespace DO;
public static class Enums
{
    public enum Category { Action, Fantasy, Romanticism, Kids, Comics };
    public enum ClientName { Reuven, Simon, Levi, Juda, David, Jakob };
    public enum ClientAddress { };
    public enum mainChoise {order=1,orderItem,product };
    public enum OrderChoice{addOrder=1,deleteOrder,updateOrder,getOrder,getAllOrder };
    public enum orderItem {addOrderItem=1,deleteOrderItem,updateOrderItem,getOrderItem,getSpecificItem, getItemsByOrder };
    public enum ProductChoice{addProduct=1,deleteProduct,updateProduct,getOrder,getAllProduct };

}