using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealShop.Models
{
    public class ModelForCosmeticFilter
    {
        //Для глаз
        public bool Eyes { get; set; } //Для глаз
        public bool Mascara { get; set; } //Тушь
        public bool Liner { get; set; } // Подводка
        public bool Eyeliner { get; set; } //Контурные карандаши для глаз
        public bool Eye_Shadow { get; set; } //Тени для век

        //Для бровей
        public bool Sourcils { get; set; } //Для бровей

        //Для губ
        public bool Lips { get; set; } //Для губ
        public bool Lipstick { get; set; } //Помада
        public bool Lip_Gloss { get; set; } //Блеск для губ
        public bool Lip_Liner { get; set; } //Контурные карандаши для губ

        //Тональные средства
        public bool Tonic { get; set; } //Тональные средства
        public bool Foundation { get; set; } //Тональный крем
        public bool Powder { get; set; } //Пудра
        public bool Correctors { get; set; } //Корректоры
        public bool Makeup_Base { get; set; } //Основа под макияж
        public bool Blusher	 { get; set; } //Румяна

        //Для ногтей
        public bool Nails { get; set; } //Для ногтей
        public bool Nail_Polish { get; set; } //Лаки
        public bool Nail_Care { get; set; } //Уход за ногтями
        public bool Nail_Polish_Remover { get; set; } //Жидкость для снятия лака

    }
}
