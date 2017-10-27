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
        public async Task<bool> TryLogin()
        {
            //return authenication to be returned. 
            bool isAuth = false;
            
            Debug.WriteLine("trying login!");
            PlantItems = new List<PlantInfo>();
            //string jsonData = JsonConvert.SerializeObject(User);
            //Debug.WriteLine(jsonData);
            //var uri = new Uri(string.Format("http://fsdevweb.azurewebsites.net/api/account/authenticate", string.Empty));
            var uri = "http://fsdevweb.azurewebsites.net/api/account/authenticate";
            Debug.WriteLine("------try---------");
            try
            {
                //var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                
               
                var prms = new List<KeyValuePair<string, string>>
                    {
                        
                        new KeyValuePair<string, string>("username", User.email),
                        new KeyValuePair<string, string>("password", User.password),
                        new KeyValuePair<string, string>("grant_type", "password")
                    };
               
                // var parameters = new Dictionary<string, string>();
                //parameters["text"] = text;

                Debug.WriteLine(prms.ToString());
                var response =  await client.PostAsync(uri, new FormUrlEncodedContent(prms));

                Debug.WriteLine("-------------before status code------------");
                Debug.WriteLine(response);
                Debug.WriteLine("");
                
                
                if (response.IsSuccessStatusCode)
                {
                    isAuth = true;
                    Debug.WriteLine("================after status code=============");
                    var result = JsonConvert.DeserializeObject<TokenDTO>(response.Content.ReadAsStringAsync().Result);
                    Debug.WriteLine("access_token : " + result.access_token + "\ntoken_ type : " + result.token_type + "\nexpires_in : " + result.expires_in +
                    "\nuserName : " + result.userName + "\n.issued : " + result.issued + "\n.expires : " + result.expires);
                    

                }

           }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            Debug.WriteLine("returning ");
            return isAuth;
        }
    }
    
}
