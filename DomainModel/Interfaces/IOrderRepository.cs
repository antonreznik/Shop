using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;

namespace DomainModel.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetOrdersFromBase(FilterParametersForOrders parameters);
        int AddOrder(Order obj);
        void CloseOrder(string date, string newPostRef, string orderId);
        List<ProductsInOrder> GetOrderProducts(int orderId);
    }
}
