using DomainModel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Classes;

namespace DomainModel.Entity
{
    public class DataForPriceRepository : IDataForPrice
    {
        private EFContext context = new EFContext();

        public DataForPriceBuilding GetData()
        {
            return context.DataForPrice.First();
        }
    }
}
