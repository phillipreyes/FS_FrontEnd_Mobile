using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solar.Model
{
    // Authorization Token
    public class TokenDTO
    {
        public string access_token { get; set; } // allows access
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string userName { get; set; } // username associated with token
        [JsonProperty  (".issued")]
        public DateTime issued { get; set; }
        [JsonProperty (".expires")]
        public DateTime expires { get; set; }

    }
}
