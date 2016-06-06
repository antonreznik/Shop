using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;

namespace DomainModel.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Parfum> GetAllProductsFromBase { get; }
        void SaveProduct(Parfum obj);
        void DeleteProduct(Parfum obj);
        IQueryable<Parfum> GetProductsFromCategory(int CategoryId);
        Parfum GetProductById(int ProductId);
        IQueryable<Parfum> GetProductByFilters(bool IsMen, bool IsWomen, bool Size25, bool Size50, bool Size75, bool Size100);
    }
}
