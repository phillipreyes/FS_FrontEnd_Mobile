using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Solar.Model;
using System.Net.Http;
using System.Diagnostics;

namespace Solar.Controller
{
    class Login
    {
        HttpClient client;
        private UserClass User;
        private List<PlantDTO> PlantItems;
        public Login(UserClass user)
        {
            User = user;
            client = neGitHub.VisualStudio.vsixw HttpClient();
        }
        public async Task<List<PlantDTO>> TryLogin()
        {

            Debug.WriteLine("tryindg login!");
            PlantItems = new List<PlantDTO>();
            string jsonData = JsonConvert.SerializeObject(User);
            var uri = new Uri(string.Format("http://fsadminweb.azurewebsites.net/api/PlantApi/plants", string.Empty));
            Debug.WriteLine("try");
            try
            {
               
                var response = await client.GetAsync(uri);
                
                if (response.IsSuccessStatusCode)
                {
                   
                   var content = await response.Content.ReadAsStringAsync();
                   List<PlantDTO> PlantItems = JsonConvert.DeserializeObject<List<PlantDTO>>(content);
                    foreach (var id in PlantItems)
                    {
                        Debug.WriteLine(id.Id + " " + id.PlantName);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            return PlantItems;

        }
    }
    
}
