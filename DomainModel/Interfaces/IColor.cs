using DomainModel.Classes;
using DomainModel.Classes.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Interfaces
{
    public interface IColor
    {
        IQueryable<Color> GetAllColorsFromBase(int ProductId,int CategoryId);
        void SaveColor(Color color);
        void DeleteColor(Color color);
        Color GetColorById(int ColorId);
        void ChangeAvailability(int colorId, bool isAvailable);
        IQueryable<Color> AdminGetAllColorsFromBase(int ProductId, int CategoryId);
    }
}
