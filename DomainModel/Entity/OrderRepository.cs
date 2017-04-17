using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;
using DomainModel.Interfaces;


namespace DomainModel.Entity
{
    public class OrderRepository:IOrderRepository
    {
        private EFContext Context = new EFContext();

        //Выбор заказов из базы для админа
        public IQueryable<Order> GetOrdersFromBase (FilterParametersForOrders parameters)
        {
            IQueryable<Order> orders = null;
            
            if (parameters.PhoneNumber != null)
            {
                orders = Context.Orders.Where(order => order.OrderDate >= parameters.From && order.OrderDate <= parameters.To && order.Phone == parameters.PhoneNumber);
            }

            else if (parameters.NewPostRef != null)
            {
                orders = Context.Orders.Where(order => order.OrderDate >= parameters.From && order.OrderDate <= parameters.To && order.NewPostRef == parameters.NewPostRef);
            }

            else if (parameters.PhoneNumber != null && parameters.NewPostRef != null)
            {
                orders = Context.Orders.Where(order => order.OrderDate >= parameters.From && order.OrderDate <= parameters.To && order.Phone == parameters.PhoneNumber && order.NewPostRef==parameters.NewPostRef);
            }

            else
            {
                orders = (from o in Context.Orders
                           where (o.OrderDate >= parameters.From)
                           select o);
            }
            
            return orders;
        }

        //Добавление заказа в базу
        public int AddOrder(Order ord)
        {
            Context.Orders.Add(ord);
            Context.SaveChanges();
            return ord.OrderId;
        }

        //Закрытие заказа
        public void CloseOrder (string sentDate, string newPostRef, string orderId)
        {
            DateTime dateOfSendingOrder = DateTime.Parse(sentDate);
            int OrderIdInt = Int32.Parse(orderId);
            Order ord = Context.Orders.FirstOrDefault(order => order.OrderId == OrderIdInt);
            ord.OrderState = "Closed";
            ord.DateOfSendingOrderToClient = dateOfSendingOrder;
            ord.NewPostRef = newPostRef;

            Context.SaveChanges();
        }

        //Выбор товаров по ИД заказа
        public List<ProductsInOrder> GetOrderProducts(int orderId)
        {
            var ord = Context.Orders.FirstOrDefault(p => p.OrderId == orderId);
            var products = ord.ProductInOrder.ToList();             
            return products;
        }

        //Выбор заказа по ИД 
        public Order GetOrderById(int orderId)
        {
            return Context.Orders.FirstOrDefault(p => p.OrderId == orderId);
        }
    }
}
