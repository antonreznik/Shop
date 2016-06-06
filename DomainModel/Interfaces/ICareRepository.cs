using DomainModel.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface ICareRepository
    {
        IQueryable<Care> GetAllProductsFromBase { get; }
        void SaveProduct(Care obj);
        void DeleteProduct(Care obj);
        IQueryable<Care> GetProductsFromCategory(int CategoryId);
        IQueryable<Care> GetNotAllProductsFromCategory(int CategoryId);
        Care GetProductById(int ProductId);
        IQueryable<Care> GetProductByFilters(bool FaceSkin, bool BodyCare, bool HairCare, bool MenSkinCare);
        void ChangeAvailability(int productId, bool isAvailable);
        IQueryable<Care> AdminGetProductsFromCategory(int CategoryId);
    }
}
