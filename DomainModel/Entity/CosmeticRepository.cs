using DomainModel.Classes;
using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entity
{
    public class CosmeticRepository : ICosmeticRepository
    {
        private EFContext context = new EFContext();
        private ColorRepository colorRepo = new ColorRepository();

        //----------------------------------------------------------
        //Выбираем все продукты из базы      
        public IQueryable<Cosmetic> GetAllProductsFromBase
        {
            get { return context.Cosmetics.Where(x => x.IsAvailable == true); }
        }
        //----------------------------------------------------------


        //----------------------------------------------------------
        //Метод по добавлению нового товара и по сохранению изменений в существующих
        public void SaveProduct(Cosmetic obj)
        {
            if (obj.ProductId == 0)
            {
                context.Cosmetics.Add(obj);
            }

            else
            {
                Cosmetic prod = context.Cosmetics.Find(obj.ProductId);
                if (prod != null)
                {
                    prod.ProductName = obj.ProductName;
                    prod.CategoryId = obj.CategoryId;
                    prod.Price = obj.Price;
                    prod.NewPrice = obj.NewPrice;
                    prod.Description = obj.Description;
                    prod.Type = obj.Type;
                    prod.SubType = obj.SubType;
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
        public void DeleteProduct(Cosmetic obj)
        {
            context.Cosmetics.Remove(obj);
            context.SaveChanges();
        }
        //----------------------------------------------------------

        //----------------------------------------------------------
        //Выбираем продукты определенной категории
        public IQueryable<Cosmetic> GetProductsFromCategory(int CategoryId)
        {
            IQueryable<Cosmetic> CategoryProducts = from Cosmetics in context.Cosmetics
                                                    where Cosmetics.CategoryId == CategoryId && Cosmetics.IsAvailable == true
                                                    select Cosmetics;

            foreach (var item in CategoryProducts)
            {
                item.Colors = colorRepo.GetAllColorsFromBase(item.ProductId, item.CategoryId).ToList();
            }
            return CategoryProducts;
        }

        //----------------------------------------------------------
        //Выбираем продукты определенной категории не более 12 штук(для первой загрузки сайта)
        public IQueryable<Cosmetic> GetNotAllProductsFromCategory(int CategoryId)
        {
            //IQueryable<Cosmetic> CategoryProducts = (from Cosmetics in context.Cosmetics
            //                                        where Cosmetics.CategoryId == CategoryId
            //                                        select Cosmetics).Take(1);
            IQueryable<Cosmetic> CategoryProducts = context.Cosmetics
                .Where(cosmetic => cosmetic.CategoryId == CategoryId && cosmetic.IsAvailable == true)
                .OrderByDescending(cosmetic => cosmetic.CountOfView).Take(12);

            foreach (var item in CategoryProducts)
            {
                item.Colors = colorRepo.GetAllColorsFromBase(item.ProductId, item.CategoryId).ToList();
            }
            return CategoryProducts;
        }

        //----------------------------------------------------------
        //Выбираем продукты определенной категории для админа
        public IQueryable<Cosmetic> AdminGetProductsFromCategory(int CategoryId)
        {
            IQueryable<Cosmetic> CategoryProducts = from Cosmetics in context.Cosmetics
                                                    where Cosmetics.CategoryId == CategoryId
                                                    select Cosmetics;

            foreach (var item in CategoryProducts)
            {
                item.Colors = colorRepo.GetAllColorsFromBase(item.ProductId, item.CategoryId).ToList();
            }
            return CategoryProducts;
        }

        //-----------------------------------------------------------
        //Выбираем продукт по идшнику
        public Cosmetic GetProductById(int ProductId)
        {
            Cosmetic obj = context.Cosmetics.FirstOrDefault(product => product.ProductId == ProductId);
            obj.Colors = colorRepo.GetAllColorsFromBase(obj.ProductId, obj.CategoryId).ToList();
            return obj;
        }

        //-----------------------------------------
        //Выбираем товары по переданным значениям из фильтра
        public IQueryable<Cosmetic> GetProductByFilters(CosmeticFilter obj)
        {
            IQueryable<Cosmetic> Cosmetics = GetAllProductsFromBase;
            //Переданы пустые значение в фильтре
            if (obj.Eyes==false && obj.Lips==false && obj.Nails==false && obj.Tonic==false && obj.Blusher == false && obj.Correctors == false && obj.Eye_Shadow == false && obj.Eyeliner == false && obj.Foundation == false && obj.Liner == false && obj.Lip_Gloss == false && obj.Lip_Liner == false && obj.Lipstick == false && obj.Makeup_Base == false && obj.Mascara == false && obj.Nail_Care == false && obj.Nail_Polish == false && obj.Nail_Polish_Remover == false && obj.Powder == false)
            {
                foreach (var item in Cosmetics)
                {
                    item.Colors = colorRepo.GetAllColorsFromBase(item.ProductId, item.CategoryId).ToList();
                }
                return Cosmetics;
            }

            else
            {
                List<Cosmetic> Result = new List<Cosmetic>();

                IQueryable<Cosmetic> Eyes; //Все для глаз
                IQueryable<Cosmetic> Mascara; //Тушь
                IQueryable<Cosmetic> Liner; // Подводка
                IQueryable<Cosmetic> Eyeliner; //Контурные карандаши для глаз
                IQueryable<Cosmetic> Eye_Shadow; //Тени для век

                IQueryable<Cosmetic> Lips; //Все для губ
                IQueryable<Cosmetic> Lipstick; //Помада
                IQueryable<Cosmetic> Lip_Gloss; //Блеск для губ
                IQueryable<Cosmetic> Lip_Liner; //Контурные карандаши для губ

                IQueryable<Cosmetic> Tonic; //Тональные средства
                IQueryable<Cosmetic> Foundation; //Тональный крем
                IQueryable<Cosmetic> Powder; //Пудра
                IQueryable<Cosmetic> Correctors; //Корректоры
                IQueryable<Cosmetic> Makeup_Base; //Основа под макияж
                IQueryable<Cosmetic> Blusher; //Румяна

                IQueryable<Cosmetic> Nails; //Все для ногтей
                IQueryable<Cosmetic> Nail_Polish; //Лаки
                IQueryable<Cosmetic> Nail_Care; //Уход за ногтями
                IQueryable<Cosmetic> Nail_Polish_Remover; //Жидкость для снятия лака


                //Выбираем все для глаз 
                if (obj.Eyes && obj.Mascara==false && obj.Liner==false && obj.Eyeliner==false && obj.Eye_Shadow==false)
                {
                    Eyes = Cosmetics.Where(cosmetic => cosmetic.Type == "Для глаз");
                    Result = Result.Concat(Eyes).ToList();
                }

                //фильтр по подтипу
                //для глаз               
                if (obj.Mascara)
                {
                    Mascara = from cosmetic in Cosmetics
                              where (cosmetic.SubType == "Тушь для ресниц")
                              select cosmetic;
                    Result = Result.Concat(Mascara).ToList();
                }


                if (obj.Liner)
                {
                    Liner = from cosmetic in Cosmetics
                            where (cosmetic.SubType == "Подводка")
                            select cosmetic;
                    Result = Result.Concat(Liner).ToList();
                }


                if (obj.Eyeliner)
                {
                    Eyeliner = from cosmetic in Cosmetics
                               where (cosmetic.SubType == "Контурные карандаши для глаз")
                               select cosmetic;
                    Result = Result.Concat(Eyeliner).ToList();
                }


                if (obj.Eye_Shadow)
                {
                    Eye_Shadow = from cosmetic in Cosmetics
                                 where (cosmetic.SubType == "Тени для век")
                                 select cosmetic;
                    Result = Result.Concat(Eye_Shadow).ToList();
                }


                //Выбираем все для губ 
                if (obj.Lips && obj.Lipstick == false && obj.Lip_Gloss == false && obj.Lip_Liner == false)
                {
                    Lips = Cosmetics.Where(cosmetic => cosmetic.Type == "Для губ");
                    Result = Result.Concat(Lips).ToList();
                }
                //для губ               
                if (obj.Lipstick)
                {
                    Lipstick = from cosmetic in Cosmetics
                               where (cosmetic.SubType == "Помада")
                               select cosmetic;
                    Result = Result.Concat(Lipstick).ToList();
                }


                if (obj.Lip_Gloss)
                {
                    Lip_Gloss = from cosmetic in Cosmetics
                                where (cosmetic.SubType == "Блеск для губ")
                                select cosmetic;
                    Result = Result.Concat(Lip_Gloss).ToList();
                }


                if (obj.Lip_Liner)
                {
                    Lip_Liner = from cosmetic in Cosmetics
                                where (cosmetic.SubType == "Контурные карандаши для губ")
                                select cosmetic;
                    Result = Result.Concat(Lip_Liner).ToList();
                }


                //Выбираем все тональные средства 
                if (obj.Tonic && obj.Foundation == false && obj.Powder == false && obj.Correctors == false && obj.Makeup_Base == false && obj.Blusher == false)
                {
                    Tonic = Cosmetics.Where(cosmetic => cosmetic.Type == "Тональные средства");
                    Result = Result.Concat(Tonic).ToList();
                }
                //тональные средства                
                if (obj.Foundation)
                {
                    Foundation = from cosmetic in Cosmetics
                                 where (cosmetic.SubType == "Тональный крем")
                                 select cosmetic;
                    Result = Result.Concat(Foundation).ToList();
                }


                if (obj.Powder)
                {
                    Powder = from cosmetic in Cosmetics
                             where (cosmetic.SubType == "Пудра")
                             select cosmetic;
                    Result = Result.Concat(Powder).ToList();
                }


                if (obj.Correctors)
                {
                    Correctors = from cosmetic in Cosmetics
                                 where (cosmetic.SubType == "Корректоры")
                                 select cosmetic;
                    Result = Result.Concat(Correctors).ToList();
                }


                if (obj.Makeup_Base)
                {
                    Makeup_Base = from cosmetic in Cosmetics
                                  where (cosmetic.SubType == "Основа под макияж")
                                  select cosmetic;
                    Result = Result.Concat(Makeup_Base).ToList();
                }


                if (obj.Blusher)
                {
                    Blusher = from cosmetic in Cosmetics
                              where (cosmetic.SubType == "Румяна")
                              select cosmetic;
                    Result = Result.Concat(Blusher).ToList();
                }

                //Выбираем все для ногтей 
                if (obj.Nails && obj.Nail_Polish == false && obj.Nail_Care == false && obj.Nail_Polish_Remover == false)
                {
                    Nails = Cosmetics.Where(cosmetic => cosmetic.Type == "Для ногтей");
                    Result = Result.Concat(Nails).ToList();
                }
                //для ногтей               
                if (obj.Nail_Polish)
                {
                    Nail_Polish = from cosmetic in Cosmetics
                                  where (cosmetic.SubType == "Лаки")
                                  select cosmetic;
                    Result = Result.Concat(Nail_Polish).ToList();
                }

                if (obj.Nail_Care)
                {
                    Nail_Care = from cosmetic in Cosmetics
                                where (cosmetic.SubType == "Уход за ногтями")
                                select cosmetic;
                    Result = Result.Concat(Nail_Care).ToList();
                }

                if (obj.Nail_Polish_Remover)
                {
                    Nail_Polish_Remover = from cosmetic in Cosmetics
                                          where (cosmetic.SubType == "Жидкость для снятия лака")
                                          select cosmetic;
                    Result = Result.Concat(Nail_Polish_Remover).ToList();
                }

                foreach (var item in Result)
                {
                    item.Colors = colorRepo.GetAllColorsFromBase(item.ProductId, item.CategoryId).ToList();
                }
                return Result.AsQueryable();
            }
        }

        //-----------------------------------------
        //Изменение доступности товара
        public void ChangeAvailability(int productId, bool isAvailable)
        {
            Cosmetic obj = GetProductById(productId);
            foreach (var item in obj.Colors)
            {
                context.Entry(item).State = EntityState.Modified;
            }
            obj.IsAvailable = isAvailable;
            context.SaveChanges();
        }

        //-----------------------------------------
        //Добавление просмотра
        public void AddCountOfView(int productId)
        {
            Cosmetic obj = GetProductById(productId);
            foreach (var item in obj.Colors)
            {
                context.Entry(item).State = EntityState.Modified;
            }
            obj.CountOfView += 1;
            context.SaveChanges();
        }
    }
}

