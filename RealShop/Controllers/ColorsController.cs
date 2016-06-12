using DomainModel.Classes;
using DomainModel.Classes.Colors;
using DomainModel.Entity;
using RealShop.Models.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealShop.Controllers
{
    public class ColorsController : Controller
    {
        private ColorRepository colorRepo = new ColorRepository();

        //add color
        public ActionResult AddColor(int ProductId, int CategoryId)
        {
            ColorViewModel color = new ColorViewModel();
            color.ProductId = ProductId;
            color.CategoryId = CategoryId;
            return View(color);
        }

        [HttpPost]
        public ActionResult AddColor(ColorViewModel color, IEnumerable<HttpPostedFileBase> Images)
        {

            if (ModelState.IsValid)
            {
                if (Images != null)
                {
                    foreach (var item in Images)
                    {
                        Color newColor = new Color();
                        newColor.CategoryId = color.CategoryId;
                        newColor.ProductId = color.ProductId;
                        newColor.ColorName = item.FileName.Substring(0, item.FileName.IndexOf("."));
                        newColor.ImageData = new byte[item.ContentLength];
                        newColor.ImageMimeType = item.ContentType;
                        newColor.IsAvailable = true;
                        item.InputStream.Read(newColor.ImageData, 0, item.ContentLength);
                        colorRepo.SaveColor(newColor);
                        TempData["message"] = String.Format("Цвета добавлены");
                    }
                }

                return RedirectToAction("EditCosmetic", "Admin", new { color.ProductId });
            }

            else
            {
                return View(color);
            }
        }

        //edit color
        public ActionResult EditColor(int ColorId)
        {
            Color color = colorRepo.GetColorById(ColorId);
            ColorViewModel colorView = new ColorViewModel();
            colorView.ColorId = color.ColorId;
            colorView.ColorName = color.ColorName;
            colorView.ImageData = color.ImageData;
            colorView.ImageMimeType = color.ImageMimeType;
            return View(colorView);
        }

        [HttpPost]
        public ActionResult EditColor(ColorViewModel colorView, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    colorView.ImageMimeType = Image.ContentType;
                    colorView.ImageData = new byte[Image.ContentLength];
                    Image.InputStream.Read(colorView.ImageData, 0, Image.ContentLength);
                }

                Color color = new Color();
                color.ColorId = colorView.ColorId;
                //color.CategoryId = colorView.CategoryId;
                color.ColorName = colorView.ColorName;
                color.ImageData = colorView.ImageData;
                color.ImageMimeType = colorView.ImageMimeType;
                colorRepo.SaveColor(color);
                TempData["message"] = String.Format("Цвет {0} отредактирован", color.ColorName);
                return RedirectToAction("GetAllColorsFromProduct", "Colors", new { ProductId = color.ProductId, CategoryId = color.CategoryId });
            }

            else
            {
                return View(colorView);
            }
        }

        //delete color
        public ActionResult DeleteColor(int ColorId)
        {
            Color color = colorRepo.GetColorById(ColorId);
            colorRepo.DeleteColor(color);
            TempData["message"] = String.Format("Цвет {0} удален", color.ColorName);
            return (RedirectToAction("GetAllColorsFromProduct", "Colors", new { ProductId = color.ProductId, CategoryId = color.CategoryId }));
            
        }

        //get all colors from product
        public ActionResult GetAllColorsFromProduct(int ProductId, int CategoryId, string productName)
        {
            TempData["CategoryName"] = productName;
            IQueryable<Color> colors = colorRepo.GetAllColorsFromBase(ProductId, CategoryId);
            return View(colors);
        }

        //admin get all colors from product
        [Authorize]
        public ActionResult AdminGetAllColorsFromProduct(int ProductId, int CategoryId, string productName)
        {
            TempData["CategoryName"] = productName;
            IQueryable<Color> colors = colorRepo.AdminGetAllColorsFromBase(ProductId, CategoryId);
            return View("GetAllColorsFromProduct", colors);
        }

        //get image of color
        public FileContentResult GetColorImages(int ColorId)
        {
            Color color = colorRepo.GetColorById(ColorId);
            if (color != null)
            {
                return File(color.ImageData, color.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        //get partial view of color in one product info page
        public ActionResult GetPartialViewOfColor(int ColorId)
        {
            Color color = colorRepo.GetColorById(ColorId);
            return PartialView(color);
        }
    }
}