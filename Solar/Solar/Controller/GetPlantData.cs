using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Diagnostics;
using Solar.Model;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Solar.Controller
{
    public class GetPlantData
    {
        private HttpClient client;
        private TokenDTO tokenInfo;
        private string plantID { get; set; }

        public GetPlantData(TokenDTO info, string pi)
        {
            client = new HttpClient();
            tokenInfo = info;
            plantID = pi;
        }

        public List<List<PlantDataViewModel>>GetGenerationCalendarData()
        {
            var client2 = new HttpClient();
            // var url = "http://fsdevweb.azurewebsites.net/api/plantapi/plants/data?ID=4141&StartDate=9/1/2017&EndDate=9/5/2017";
            var url = "http://fsdevweb.azurewebsites.net/api/plantapi/plants/data?ID=" + plantID + "&StartDate=9/1/2017&EndDate=9/5/2017";
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.access_token);
            var response2 = client2.GetAsync(url).Result;

            var ListOfDays = JsonConvert.DeserializeObject<List<List<PlantDataViewModel>>>(response2.Content.ReadAsStringAsync().Result);
            foreach (List<PlantDataViewModel> DataObjectList in ListOfDays)
            {
                foreach (PlantDataViewModel DataObject in DataObjectList)
                {
                    Debug.WriteLine("\nSiteAssetID: " + DataObject.SiteAssetID + "\nReadTime: " + DataObject.ReadTime + "\nPower_KW: " + DataObject.Power_kW + "\nSpecificDataObjectieldPerc: " + DataObject.SpecificYieldPerc
                        + "\nIrradiancePOAWm2: " + DataObject.IrradiancePOAWm2 + "\nIrradiancePerc: " + DataObject.IrradiancePerc + "\nTemperatureC: " + DataObject.TemperatureC + "\nETLInsertLogID: "
                        + DataObject.ETLInsertLogID + "\nETLInsertTimestamp: " + DataObject.ETLInsertTimestamp);
                }
            }
            return ListOfDays;
        }
    }
}
