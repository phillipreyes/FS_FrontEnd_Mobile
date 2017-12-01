using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solar.Model
{
    // Object representation of a row in the Generation Calendar Database table
    public class PlantDataViewModel
    {
        public long SiteAssetID { get; set; } // plantID
        public DateTime ReadTime { get; set; } // readtime
        public double? Power_kW { get; set; }
        public double? SpecificYieldPerc { get; set; } // specific yield
        public double? IrradiancePOAWm2 { get; set; } 
        public double? IrradiancePerc { get; set; } // irradiance
        public double? TemperatureC { get; set; }
        public long? ETLInsertLogID { get; set; }
        public DateTime ETLInsertTimestamp { get; set; }
    }
}
