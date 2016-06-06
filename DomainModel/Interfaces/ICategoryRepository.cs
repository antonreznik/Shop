using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;

namespace DomainModel.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> GetAllCategoriesFromBase { get;}
        void SaveCategory(Category obj);
        string DeleteCategory(Category obj);
        string GetCategoryName(int CategoryId);
    }
}
