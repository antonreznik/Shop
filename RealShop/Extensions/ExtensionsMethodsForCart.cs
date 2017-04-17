using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealShop.Models;

namespace RealShop.Extensions
{
    public static class ExtensionsMethodsForCart
    {
        public static InfoCartViewModel GetCartInfo (this CartViewModel Cart, double deliveryPrice = 0)
        {
            InfoCartViewModel Info = new InfoCartViewModel ();
            if (HttpContext.Current.Session["cart"] == null)
            {
                return Info;
            }

            else
            {
                Info.TotalQuantityOfProducts = Cart.Products.Select(x => x.Quantity).Sum();
                //Info.TotalPriceOfProducts = Cart.Products.Select(x => x.obj.Price * x.Quantity).Sum();
                //Info.TotalPriceOfProducts = Cart.Products.Select(x => (x.obj.PriceToShow == 0 ? x.obj.Price : x.obj.PriceToShow) * x.Quantity).Sum();
                Info.TotalPriceOfProducts = Cart.Products.Select(x => x.obj.PriceToShow * x.Quantity).Sum();
                Info.TotalPriceWithDelivery = Info.TotalPriceOfProducts < 700 ? (Info.TotalPriceOfProducts + deliveryPrice) : Info.TotalPriceOfProducts;
                return Info;
            }
        }

        public static void RemoveAllFromCart(this CartViewModel cart)
        {
            HttpContext.Current.Session.Remove("cart");
        }
    }
}