using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solar.Model
{
    public class PlantDataViewModel
    {
        public long SiteAssetID { get; set; }
        public DateTime ReadTime { get; set; }
        public double? Power_kW { get; set; }
        public double? SpecificYieldPerc { get; set; }
        public double? IrradiancePOAWm2 { get; set; }
        public double? IrradiancePerc { get; set; }
        public double? TemperatureC { get; set; }
        public long? ETLInsertLogID { get; set; }
        public DateTime ETLInsertTimestamp { get; set; }
    }
}
