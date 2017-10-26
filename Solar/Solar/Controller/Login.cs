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
        //testss
        HttpClient client;
        private User User;
        //private List<PlantDTO> PlantItems;
        private List<PlantInfo> PlantItems;
        public Login(User user)
        {
            User = user;
            client = new HttpClient();
        }
        public async Task<List<PlantInfo>> TryLogin()
        {
            /*
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
            */
             /* Debug.WriteLine("trying login!");
              PlantItems = new List<PlantInfo>();
              string jsonData = JsonConvert.SerializeObject(User);
              Debug.WriteLine(jsonData);
              var uri = new Uri(string.Format("http://fsdevweb.azurewebsites.net/api/account/authenticate/", string.Empty));
              Debug.WriteLine("------try---------");
              try
              {
                  var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                  var response = await client.PostAsync(uri, content);
                  Debug.WriteLine("-------------before status code------------");
                Debug.WriteLine(response);
                Debug.WriteLine(response.IsSuccessStatusCode);
                  if (response.IsSuccessStatusCode)
                  {
                      Debug.WriteLine("================after status code=============");
                      var recieved = await response.Content.ReadAsStringAsync();
                      List<PlantInfo> PlantItems = JsonConvert.DeserializeObject<List<PlantInfo>>(recieved);
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
            */
            Debug.WriteLine("trying login!");
            PlantItems = new List<PlantInfo>();
            //string jsonData = JsonConvert.SerializeObject(User);
            //Debug.WriteLine(jsonData);
            var uri = new Uri(string.Format("http://fsdevweb.azurewebsites.net/api/account/authenticate", string.Empty));
            Debug.WriteLine("------try---------");
            try
            {
                //var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpClient client1 = new HttpClient();
               
                var prms = new List<KeyValuePair<string, string>>
                    {
                        
                        new KeyValuePair<string, string>("email", "testuser1@fsweb.com"),
                        new KeyValuePair<string, string>("password", "P@ssw0rd"),
                        new KeyValuePair<string, string>("grant_type", "password")
                    };
               
                // var parameters = new Dictionary<string, string>();
                //parameters["text"] = text;

                Debug.WriteLine(prms.ToString());
                var response = await client1.PostAsync(uri, new FormUrlEncodedContent(prms));
                Debug.WriteLine("-------------before status code------------");
                Debug.WriteLine(response);
               // if (response.IsSuccessStatusCode)
               // /{
                    Debug.WriteLine("================after status code=============");
                    var recieved = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(recieved);
                    //List<PlantInfo> PlantItems = JsonConvert.DeserializeObject<List<PlantInfo>>(recieved);
                   /* foreach (var id in PlantItems)
                    {
                        Debug.WriteLine(id.Id + " " + id.PlantName);
                    }*/

               // }

           }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }
            

            return PlantItems;
        }
    }
    
}
