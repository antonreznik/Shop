using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;
using DomainModel.Interfaces;

namespace DomainModel.Entity
{
    public class CategoryRepository:ICategoryRepository
    {
        private EFContext context = new EFContext();

        //--------------------------------------------------
        //Выбираем все категории из списка
        public IQueryable<Category> GetAllCategoriesFromBase
        {
            get { return context.Categories; }
        }
        //--------------------------------------------------


        //--------------------------------------------------
        //Добавление новой категории или сохранение изменений в существующей
        public void SaveCategory(Category obj)
        {
            if (obj.CategoryId == 0)
            {
                context.Categories.Add(obj);
            }

            else
            { 
                Category categ = context.Categories.Find(obj.CategoryId);
                if (categ != null)
                {
                    categ.CategoryName = obj.CategoryName;
                }
            }
            context.SaveChanges();
        }
        //--------------------------------------------------


        //--------------------------------------------------
        //Удаление категории
        public string DeleteCategory(Category obj)
        {
            string result;
            List<Parfum> Products = context.Parfums.Where(x => x.CategoryId == obj.CategoryId).ToList();
            context.Categories.Remove(obj);         
            context.Parfums.RemoveRange(Products);
            context.SaveChanges(); 
            
            if (Products.Capacity>0)
            {
                result = String.Format("Категория '{0}' и все её товары удалены", obj.CategoryName);
            }

            else
            {
                result = String.Format("Категория {0} удалена",obj.CategoryName);
            }

            return result;
        }
        //--------------------------------------------------

        //--------------------------------------------------
        //Достаем имя категории
        public string GetCategoryName(int CategoryId)
        {
            Category obj = context.Categories.FirstOrDefault(x => x.CategoryId == CategoryId);
            return obj.CategoryName;
        }
        //--------------------------------------------------
    }
}
