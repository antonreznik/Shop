using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;
using DomainModel.Interfaces;

namespace DomainModel.Entity
{
    public class ProductRepository:IProductRepository
    {
        private EFContext context = new EFContext();     
        
        //----------------------------------------------------------
        //Выбираем все продукты из базы      
        public IQueryable<Parfum> GetAllProductsFromBase
        {
            get { return context.Parfums; }
        }
        //----------------------------------------------------------


        //----------------------------------------------------------
        //Метод по добавлению нового товара и по сохранению изменений в существующих
        public void SaveProduct(Parfum obj)
        {
            if (obj.ProductId == 0)
            {
                context.Parfums.Add(obj);
            }

            else
            {
                Parfum prod = context.Parfums.Find(obj.ProductId);
                if (prod != null)
                {
                    prod.ProductName = obj.ProductName;
                    prod.CategoryId = obj.CategoryId;
                    //prod.Price = obj.Price;
                    prod.PriceFor50ml = obj.PriceFor50ml;
                    prod.PriceFor75ml = obj.PriceFor75ml;
                    prod.PriceFor100ml = obj.PriceFor100ml;
                    prod.Size25ml = obj.Size25ml;
                    prod.Size50ml = obj.Size50ml;
                    prod.Size75ml = obj.Size75ml;
                    prod.Size100ml = obj.Size100ml;
                    prod.Description = obj.Description;               
                    prod.Sex = obj.Sex;
                    if (obj.ImageMimeType != null && obj.ImageData != null)
                    {
                        prod.ImageData = obj.ImageData;
                        prod.ImageMimeType = obj.ImageMimeType;
                    }
                }
            }
            context.SaveChanges();
        }
        //----------------------------------------------------------


        //----------------------------------------------------------
        //Удаление товара
        public void DeleteProduct(Parfum obj)
        {
            context.Parfums.Remove(obj);
            context.SaveChanges();
        }
        //----------------------------------------------------------

        //----------------------------------------------------------
        //Выбираем продукты определенной категории
        public IQueryable<Parfum> GetProductsFromCategory(int CategoryId)
        {
            IQueryable<Parfum> CategoryProducts = from Parfums in context.Parfums
                                                   where Parfums.CategoryId == CategoryId
                                                   select Parfums;
            return CategoryProducts;
        }

        //-----------------------------------------------------------
        //Выбираем продукт по идшнику
        public Parfum GetProductById(int ProductId)
        {
            Parfum obj = context.Parfums.FirstOrDefault(product => product.ProductId == ProductId);
            return obj;
        }
        
        //-----------------------------------------
        //Выбираем товары по переданным значениям из фильтра
        public IQueryable<Parfum> GetProductByFilters(bool IsMen, bool IsWomen, bool Size25, bool Size50, bool Size75, bool Size100)
        {
            //Переданы пустые значение в фильтре
            IQueryable<Parfum> Products = GetAllProductsFromBase;
            
            List<Parfum> Result = new List<Parfum>();
            IQueryable<Parfum> Parfum25;
            IQueryable<Parfum> Parfum50;
            IQueryable<Parfum> Parfum75;
            IQueryable<Parfum> Parfum100;
            
            
                //Фильтр по полу           
                if (IsMen)
                {
                    Products = GetAllProductsFromBase.Where(x => x.Sex == "Men");
                }

                if (IsWomen)
                {
                    Products = GetAllProductsFromBase.Where(x => x.Sex =="Women");
                }

                if (IsMen && IsWomen)
                {
                    Products = GetAllProductsFromBase;
                }
                
                //фильтр по литражу если выбран
                if (Size25 || Size50 || Size75 || Size100)
                {
                    if (Size25)
                    {
                        Parfum25 = from product in Products
                                   where (product.Size25ml == true)
                                   select product;                      
                       Result = Result.Concat(Parfum25).ToList();
                    }

                    if (Size50)
                    {
                        Parfum50 = (from product in Products
                                    where (product.Size50ml == true)
                                   select product);
                        Result = Result.Concat(Parfum50).ToList();
                    }

                    if (Size75)
                    {
                        Parfum75 = from product in Products
                                   where (product.Size75ml == true)
                                   select product;
                        Result = Result.Concat(Parfum75).ToList();
                    }

                    if (Size100)
                    {
                        Parfum100 = from product in Products
                                   where (product.Size100ml == true)
                                   select product;
                        Result = Result.Concat(Parfum100).ToList();
                    }
                    return Result.AsQueryable().Distinct();
                }

                // литраж не выбран на фильтре
                else
                {
                    return Products;
                }
            
            }
        }       
   }

