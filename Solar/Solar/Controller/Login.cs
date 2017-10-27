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
        private TokenDTO tokenobj;
        public Login(User user)
        {
            User = user;
            client = new HttpClient();
            tokenobj = new TokenDTO();
        }
        public async Task<Tuple<TokenDTO, bool>> TryLogin()
        {
            //return authenication to be returned. 

            
            bool isAuth = false;
            
            Debug.WriteLine("trying login!");
            
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
                   tokenobj = JsonConvert.DeserializeObject<TokenDTO>(response.Content.ReadAsStringAsync().Result);
                    Debug.WriteLine("access_token : " + tokenobj.access_token + "\ntoken_ type : " + tokenobj.token_type + "\nexpires_in : " + tokenobj.expires_in +
                    "\nuserName : " + tokenobj.userName + "\n.issued : " + tokenobj.issued + "\n.expires : " + tokenobj.expires);
                    

                }

           }
            catch (Exception ex)
            {
                Debug.WriteLine(@"				ERROR {0}", ex.Message);
            }

            Debug.WriteLine("returning ");
            return new Tuple<TokenDTO, bool>(tokenobj, isAuth);
        }
    }
    
}
