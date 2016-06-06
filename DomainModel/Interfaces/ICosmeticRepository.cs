using DomainModel.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface ICosmeticRepository
    {
        IQueryable<Cosmetic> GetAllProductsFromBase { get; }
        void SaveProduct(Cosmetic obj);
        void DeleteProduct(Cosmetic obj);
        IQueryable<Cosmetic> GetProductsFromCategory(int CategoryId);
        IQueryable<Cosmetic> GetNotAllProductsFromCategory(int CategoryId);
        Cosmetic GetProductById(int ProductId);
        IQueryable<Cosmetic> GetProductByFilters(CosmeticFilter filt);
        void ChangeAvailability(int productId, bool isAvailable);
        IQueryable<Cosmetic> AdminGetProductsFromCategory(int CategoryId);
    }
}
