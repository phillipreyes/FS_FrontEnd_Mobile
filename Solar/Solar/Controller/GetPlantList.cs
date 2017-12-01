using Newtonsoft.Json;
using Solar.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Solar.Controller
{
    // Object created to et the list of plants user is authorized to view
    public class GetPlantList
    {
        private HttpClient client;
        private TokenDTO tokenInfo;
        private List<PlantInfo> plantList;
        public GetPlantList(TokenDTO info)
        {
            client = new HttpClient();
            tokenInfo = info;

        }
        public List<PlantInfo> getPlants()
        {
            var client2 = new HttpClient();
            plantList = new List<PlantInfo>();

            // url of Web Service, the end is the Username of the logged in account
            var url = "http://fsdevweb.azurewebsites.net/api/plant_authorizationapi/authorizations/plants?UserName="  + tokenInfo.userName;            
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.access_token);
            // HTTP Response including PlantList
            var response =  client2.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                // Deserialize response into A list of Plant Information Objects
                plantList = JsonConvert.DeserializeObject<List<PlantInfo>>(response.Content.ReadAsStringAsync().Result);
                Debug.WriteLine("\n\n\n!******SUCCESS******!\n\n\n");
            }
            else
            {
                Debug.WriteLine("\n\n\n!******FAILURE******!\n\n\n");
            }
            return plantList;
        }
    }
}
