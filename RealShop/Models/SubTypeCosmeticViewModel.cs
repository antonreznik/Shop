using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealShop.Models
{
    public class SubTypeCosmeticViewModel
    {
        [Required(ErrorMessage="Выберите подтип")]
        public string SubType { get; set; }
        public IEnumerable<SelectListItem> SubTypeList { get; set; }

        public SubTypeCosmeticViewModel(string type)
        {
            List<SelectListItem> subtypes_to_add = new List<SelectListItem>();
            
            if (type == "Для глаз")
            {
                List<string> subtypes = new List<string>();
                subtypes.Add("Тушь для ресниц");
                subtypes.Add("Подводка");
                subtypes.Add("Контурные карандаши для глаз");
                subtypes.Add("Тени для век");

                for (int i = 0; i < subtypes.Count; i++)
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Selected = false;
                    obj.Text = subtypes[i];
                    obj.Value = subtypes[i];
                    subtypes_to_add.Add(obj);
                }
                this.SubTypeList = new SelectList(subtypes_to_add,"Value","Text");
            }

            else if (type == "Для губ")
            {
                List<string> subtypes = new List<string>();
                subtypes.Add("Помада");
                subtypes.Add("Блеск для губ");
                subtypes.Add("Контурные карандаши для губ");

                for (int i = 0; i < subtypes.Count; i++)
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Selected = false;
                    obj.Text = subtypes[i];
                    obj.Value = subtypes[i];
                    subtypes_to_add.Add(obj);
                }
                this.SubTypeList = new SelectList(subtypes_to_add,"Value","Text");
            }

            else if (type == "Тональные средства")
            {
                List<string> subtypes = new List<string>();
                subtypes.Add("Тональный крем");
                subtypes.Add("Пудра");
                subtypes.Add("Корректоры");
                subtypes.Add("Основа под макияж");
                subtypes.Add("Румяна");

                for (int i = 0; i < subtypes.Count; i++)
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Selected = false;
                    obj.Text = subtypes[i];
                    obj.Value = subtypes[i];
                    subtypes_to_add.Add(obj);
                }
                this.SubTypeList = new SelectList(subtypes_to_add,"Value","Text");
            }

            else if (type == "Для ногтей")
            {
                List<string> subtypes = new List<string>();
                subtypes.Add("Лаки");
                subtypes.Add("Уход за ногтями");
                subtypes.Add("Жидкость для снятия лака");

                for (int i = 0; i < subtypes.Count; i++)
                {
                    SelectListItem obj = new SelectListItem();
                    obj.Selected = false;
                    obj.Text = subtypes[i];
                    obj.Value = subtypes[i];
                    subtypes_to_add.Add(obj);
                }
                this.SubTypeList = new SelectList(subtypes_to_add,"Value","Text");
            }
        }
    }
}