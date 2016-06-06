using RealShop.WebRequests.NewPostAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealShop.Models
{
    public class NewPostOfficesViewModel
    {
        [Required (ErrorMessage="Выберите отделение")]
        public string NewPostOffice { get; set; }
        public IEnumerable<SelectListItem> NewPostOffices { get; set; }

        public NewPostOfficesViewModel(string City)
        {
            NewPostOffices = new SelectList(RequestToNewPostApi.GetRefCity(City),"Value","Text");
        }
    }



    

}
