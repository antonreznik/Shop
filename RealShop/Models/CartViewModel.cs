using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Classes;
using RealShop.Extensions;
using DomainModel.Classes.Colors;

namespace RealShop.Models
{
    public class CartViewModel
    {
        /*public List<ParfumInCart> Parfums { get; set; }
        public List<CosmeticInCart> Cosmetics { get; set; }
        public List <CareInCart> Cares { get; set; }*/
        public InfoCartViewModel InfoCart = new InfoCartViewModel();
        public List<ProductsInCart> Products { get; set; }


        /*public class ParfumInCart
        {
            public Parfum obj { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
        }

        public class CosmeticInCart
        {
            public Cosmetic obj { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
        }

        public class CareInCart
        {
            public Care obj { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
        }*/

        public class ProductsInCart 
        {
            public AbstractProduct obj { get; set; }
            public int Quantity { get; set; }
            public decimal TotalPrice { get; set; }
            public string Size { get; set; }
            public Color Color { get; set; }
        }

        //------------------------------------------
        //Конструктор
        public CartViewModel()
        {
            /*Parfums = new List<ParfumInCart>();
            Cosmetics = new List<CosmeticInCart>();
            Cares = new List<CareInCart>();*/
            Products = new List<ProductsInCart>();
        }

        //-----------------------------------------
        //Добавление товара в корзину
        public void AddProductToCart (AbstractProduct obj)
        {
            if (obj.Colors != null && obj.Colors.Count > 0)
            {
                ProductsInCart prod = Products.FirstOrDefault(x => x.obj.ProductId == obj.ProductId && x.obj.CategoryId == obj.CategoryId && x.Color.ColorId == obj.Colors[0].ColorId);
                if (prod != null)
                {
                    prod.Quantity++;
                    prod.TotalPrice = prod.Quantity * prod.obj.Price;
                }

                else
                {
                    int QuantityProduct = 1;
                    Products.Add(new ProductsInCart
                    {
                        obj = obj,
                        Quantity = QuantityProduct,
                        TotalPrice = QuantityProduct * obj.Price,
                        Color = obj.Colors[0]
                    });
                }
            }

            else
            {
                ProductsInCart prod = Products.FirstOrDefault(x => x.obj.ProductId == obj.ProductId && x.obj.CategoryId == obj.CategoryId);
                if (prod != null)
                {
                    prod.Quantity++;
                    prod.TotalPrice = prod.Quantity * prod.obj.Price;
                }

                else
                {
                    int QuantityProduct = 1;
                    Products.Add(new ProductsInCart
                    {
                        obj = obj,
                        Quantity = QuantityProduct,
                        TotalPrice = QuantityProduct * obj.Price                      
                    });
                }
            }        
        }

        //------------------------------------------
        //Добавление парфюма в корзину
        public void AddParfumToCart (Parfum obj, string Size)
        {
            ProductsInCart parfum = Products.FirstOrDefault(x => x.obj.ProductId == obj.ProductId && x.Size == Size);
            if (parfum != null)
            {            
                parfum.Quantity++;
                parfum.TotalPrice = parfum.Quantity * parfum.obj.Price;     
            }

            else
            {
                int QuantityParfum = 1;
                Products.Add(new ProductsInCart
                {
                    obj = obj,
                    Quantity=QuantityParfum,
                    TotalPrice=QuantityParfum*obj.Price,
                    Size = Size
                });
            }
        }
        
        //-----------------------------------------
        //Добавление косметики в корзину
        /*public void AddCosmeticToCart(Cosmetic obj)
        {
            CosmeticInCart cosmetics = Cosmetics.FirstOrDefault(x => x.obj.ProductId == obj.ProductId);
            if (cosmetics != null)
            {
                cosmetics.Quantity++;
                cosmetics.TotalPrice = cosmetics.Quantity * cosmetics.obj.Price;
            }

            else
            {
                int QuantityCosmetic = 1;
                Cosmetics.Add(new CosmeticInCart
                {
                    obj = obj,
                    Quantity = QuantityCosmetic,
                    TotalPrice = QuantityCosmetic * obj.Price
                });
            }
        }


        //-----------------------------------------
        //Добавление средства для ухода в корзину
        public void AddCareToCart(Care obj)
        {
            CareInCart cares = Cares.FirstOrDefault(x => x.obj.ProductId == obj.ProductId);
            if (cares != null)
            {
                cares.Quantity++;
                cares.TotalPrice = cares.Quantity * cares.obj.Price;
            }

            else
            {
                int QuantityCare = 1;
                Cares.Add(new CareInCart
                {
                    obj = obj,
                    Quantity = QuantityCare,
                    TotalPrice = QuantityCare * obj.Price
                });
            }
        }*/

        //-----------------------------------------
        //Удаление продукта из корзины
        public void RemoveProductFromCart(AbstractProduct obj)
        {
            if (obj.Colors != null && obj.Colors.Capacity > 0)
            {
                ProductsInCart prod = Products.FirstOrDefault(x => x.obj.ProductId == obj.ProductId && x.obj.CategoryId == obj.CategoryId && x.Color.ColorId == obj.Colors[0].ColorId);
                if (prod.Quantity > 1)
                {
                    prod.Quantity--;
                    prod.TotalPrice = prod.Quantity * prod.obj.Price;
                }

                else
                {
                    Products.Remove(prod);
                }
            }

            else
            {
                ProductsInCart prod = Products.FirstOrDefault(x => x.obj.ProductId == obj.ProductId && x.obj.CategoryId == obj.CategoryId);

                if (prod.Quantity > 1)
                {
                    prod.Quantity--;
                    prod.TotalPrice = prod.Quantity * prod.obj.Price;
                }

                else
                {
                    Products.Remove(prod);
                }
            }
            
        }

        //-----------------------------------------
        //Удаление парфюма из корзины
        public void RemoveParfumFromCart(Parfum obj, string Size)
        {
            ProductsInCart parfum = Products.FirstOrDefault(x => x.obj.ProductId == obj.ProductId && x.Size == Size);

            if (parfum.Quantity > 1)
            {
                parfum.Quantity--;
                parfum.TotalPrice = parfum.Quantity * parfum.obj.Price;
            }

            else
            {
                Products.Remove(parfum);
            }
        }

        //-----------------------------------------
        //Удаление косметики из корзины
        /*public void RemoveCosmeticFromCart(Cosmetic obj)
        {
            CosmeticInCart cosmetics = Cosmetics.FirstOrDefault(x => x.obj.ProductId == obj.ProductId);

            if (cosmetics.Quantity > 1)
            {
                cosmetics.Quantity--;
                cosmetics.TotalPrice = cosmetics.Quantity * cosmetics.obj.Price;
            }

            else
            {
                Cosmetics.Remove(cosmetics);
            }
        }
        

        //-----------------------------------------
        //Удаление средства для ухода из корзины
        public void RemoveCareFromCart(Care obj)
        {
            CareInCart cares = Cares.FirstOrDefault(x => x.obj.ProductId == obj.ProductId);

            if (cares.Quantity > 1)
            {
                cares.Quantity--;
                cares.TotalPrice = cares.Quantity * cares.obj.Price;
            }

            else
            {
                Cares.Remove(cares);
            }
        }*/
    }


}