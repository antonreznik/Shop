using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Classes
{
    public class DataForPriceBuilding
    {
        public int DataForPriceBuildingId { get; set; }

        public double PriceCoefficient { get; set; }

        public int Currency { get; set; }

        public double PostPercentComission { get; set; }

        public double PostFixedComission { get; set; }

        public double DeliveryFixedComission { get; set; }
    }
}
