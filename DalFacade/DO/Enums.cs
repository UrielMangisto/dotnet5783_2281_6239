﻿
namespace DO;

/// <summary>
/// we save here all the enums we use at the project
/// </summary>

public enum Category { All, Action, Fantasy, Romanticism, Kids, Comics };
public enum ClientName { Reuven, Simon, Levi, Juda, David, Jakob };
public enum ClientAddress { };
public enum MainChoise {order=1,orderItem,product };
public enum OrderChoice{addOrder=1,deleteOrder,updateOrder,getOrder,getAllOrder, getOrderBysomeTerm };
public enum orderItemChoise {addOrderItem=1,deleteOrderItem,updateOrderItem,getOrderItem,getSpecificItem, getItemsByOrder, getAllItems, getOrderItemBysomeTerm };
public enum ProductChoice{addProduct=1,deleteProduct,updateProduct,getProduct,getAllProduct, getProdactBysomeTerm };

