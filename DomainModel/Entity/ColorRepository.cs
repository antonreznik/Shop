using DomainModel.Classes;
using DomainModel.Classes.Colors;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entity
{
    public class ColorRepository:IColor
    {
        EFContext ef = new EFContext();
        
        //get all colors from base by product Id and CategoryId
        public IQueryable<Color> GetAllColorsFromBase(int ProductId, int CategoryId)
        {
            IQueryable<Color> colors = ef.Colors.Where(color => color.ProductId == ProductId && color.CategoryId == CategoryId && color.IsAvailable == true);
            return colors;
        }

        //add or save color
        public void SaveColor(Color color)
        {
            if (color.ColorId == 0) 
            {
                ef.Colors.Add(color);
            }

            else
            {
                Color existColor = ef.Colors.Find(color.ColorId);
                if (existColor != null)
                {
                    existColor.ColorName = color.ColorName;
                    if (color.ImageMimeType != null && color.ImageData != null)
                    {
                        existColor.ImageMimeType = color.ImageMimeType;
                        existColor.ImageData = color.ImageData;
                    }
                }
            }
            ef.SaveChanges();
        }

        //delete color
        public void DeleteColor(Color color)
        {
            ef.Colors.Remove(color);
            ef.SaveChanges();
        }

        //get color by id
        public Color GetColorById(int ColorId)
        {
            Color color = ef.Colors.FirstOrDefault(x => x.ColorId == ColorId);
            return color;
        }

        //change availability of color
        public void ChangeAvailability(int colorId, bool isAvailable)
        {
            Color obj = GetColorById(colorId);
            obj.IsAvailable = isAvailable;
            ef.SaveChanges();
        }

        //admin method to get all colors from base
        public IQueryable<Color> AdminGetAllColorsFromBase(int ProductId, int CategoryId)
        {
            IQueryable<Color> colors = ef.Colors.Where(color => color.ProductId == ProductId && color.CategoryId == CategoryId);
            return colors;
        }

    }
}
