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
        public List<PlantInfo> getPLants()
        {
            var client2 = new HttpClient();
            plantList = new List<PlantInfo>();
            
            var url = "http://fsdevweb.azurewebsites.net/api/plant_authorizationapi/authorizations/plants?UserName=testuser1@fsweb.com";
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenInfo.access_token);
            var response =  client2.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                plantList = JsonConvert.DeserializeObject<List<PlantInfo>>(response.Content.ReadAsStringAsync().Result);
                
            }
            return plantList;
        }
    }
}
