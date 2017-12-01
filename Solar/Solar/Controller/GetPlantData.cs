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
    // Retrieves the Generation Calendar Data 
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
            // url of web service, including a PlantID
            var url = "http://fsdevweb.azurewebsites.net/api/plantapi/plants/data?ID=" + plantID + "&StartDate=9/1/2017&EndDate=9/5/2017";
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.access_token);
            // HTTP Response including Generation Calendar Data
            var response2 = client2.GetAsync(url).Result;
            // Deserialize into a List of Lists of PlantDataViewModel
            // The outer list is days and the inner list is readtimes, organizes by earliest to latest time
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
