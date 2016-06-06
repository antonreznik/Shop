using DomainModel.Classes;
using DomainModel.Classes.Colors;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entity
{
    public class CareRepository:ICareRepository
    {
        private EFContext context = new EFContext();

        //----------------------------------------------------------
        //Выбираем все продукты из базы      
        public IQueryable<Care> GetAllProductsFromBase
        {
            get { return context.Cares.Where(x => x.IsAvailable == true); }
        }
        //----------------------------------------------------------


        //----------------------------------------------------------
        //Метод по добавлению нового товара и по сохранению изменений в существующих
        public void SaveProduct(Care obj)
        {
            if (obj.ProductId == 0)
            {
                context.Cares.Add(obj);
            }

            else
            {
                Care prod = context.Cares.Find(obj.ProductId);
                if (prod != null)
                {
                    prod.ProductName = obj.ProductName;
                    prod.CategoryId = obj.CategoryId;
                    prod.Price = obj.Price;
                    prod.Description = obj.Description;
                    prod.Type = obj.Type;
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
        public void DeleteProduct(Care obj)
        {
            context.Cares.Remove(obj);
            context.SaveChanges();
        }
        //----------------------------------------------------------

        //----------------------------------------------------------
        //Выбираем продукты определенной категории
        public IQueryable<Care> GetProductsFromCategory(int CategoryId)
        {
            IQueryable<Care> CategoryProducts = from Cares in context.Cares
                                                where Cares.CategoryId == CategoryId && Cares.IsAvailable == true
                                                    select Cares;
            return CategoryProducts;
        }

        //----------------------------------------------------------
        //Выбираем продукты определенной категории не более 12 штук(для первой загрузки сайта)
        public IQueryable<Care> GetNotAllProductsFromCategory(int CategoryId)
        {
            IQueryable<Care> CategoryProducts = context.Cares
                .Where(cares => cares.CategoryId == CategoryId && cares.IsAvailable == true)
                .OrderBy(cares => cares.Price).Take(12);

            return CategoryProducts;
        }


        //----------------------------------------------------------
        //Выбираем продукты определенной категории для админа
        public IQueryable<Care> AdminGetProductsFromCategory(int CategoryId)
        {
            IQueryable<Care> CategoryProducts = from Cares in context.Cares
                                                where Cares.CategoryId == CategoryId
                                                select Cares;
            return CategoryProducts;
        }

        //-----------------------------------------------------------
        //Выбираем продукт по идшнику
        public Care GetProductById(int ProductId)
        {
            Care obj = context.Cares.FirstOrDefault(product => product.ProductId == ProductId);
            obj.Colors = new List<Color>();
            return obj;
        }

        //------------------------------------------------------------
        //Выбираем товары по значениям переданным из фильтра
        public IQueryable<Care> GetProductByFilters(bool FaceSkin, bool BodyCare, bool LipsCare, bool HandCare)
        {
            //Переданы пустые значение в фильтре
            IQueryable<Care> Products = GetAllProductsFromBase;

            List<Care> Result = new List<Care>();
            IQueryable<Care> FaceSkinProducts;
            IQueryable<Care> BodyCareProducts;
            IQueryable<Care> LipsCareProducts;
            IQueryable<Care> HandCareProducts;

            //фильтр по товару если выбран
            if (FaceSkin || BodyCare || LipsCare || HandCare)
            {
                if (FaceSkin)
                {
                    FaceSkinProducts = from product in Products
                               where (product.Type == "Уход за кожей лица")
                               select product;
                    Result = Result.Concat(FaceSkinProducts).ToList();
                }

                if (BodyCare)
                {
                    BodyCareProducts = (from product in Products
                                where (product.Type == "Уход за телом")
                                select product);
                    Result = Result.Concat(BodyCareProducts).ToList();
                }

                if (LipsCare)
                {
                    LipsCareProducts = from product in Products
                               where (product.Type == "Уход за губами")
                               select product;
                    Result = Result.Concat(LipsCareProducts).ToList();
                }

                if (HandCare)
                {
                    HandCareProducts = from product in Products
                                where (product.Type == "Уход за кожей рук")
                                select product;
                    Result = Result.Concat(HandCareProducts).ToList();
                    
                }
                return Result.AsQueryable();
            }

            // значения не выбраны на фильтре
            else
            {
                return Products;
            }
        }

        //-----------------------------------------
        //Изменение доступности товара
        public void ChangeAvailability(int productId, bool isAvailable)
        {
            Care obj = GetProductById(productId);
            foreach (var item in obj.Colors)
            {
                context.Entry(item).State = EntityState.Modified;
            }
            obj.IsAvailable = isAvailable;
            context.SaveChanges();
        }
    }
}
