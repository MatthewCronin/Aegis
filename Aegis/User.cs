using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aegis
{ 
    public class GetUserResult
    {
        [JsonProperty("GetUserResult")]
        public User user { get; set; }
    }
    public class User
    {
        [JsonProperty("UserID")]
        public int UserID { get; set; }
        [JsonProperty("UserName")]
        public string UserName { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("SecQuestion1")]
        public int SecQuestion1 { get; set; }
        [JsonProperty("SecQuestion2")]
        public int SecQuestion2 { get; set; }
        [JsonProperty("SecAnswer1")]
        public string SecAnswer1 { get; set; }
        [JsonProperty("SecAnswer2")]
        public string SecAnswer2 { get; set; }
        [JsonProperty("CreatedBy")]
        public int CreatedBy { get; set; }
        [JsonProperty("UpdatedBy")]
        public int UpdatedBy { get; set; }
        [JsonProperty("DisabledBY")]
        public int DisabledBY { get; set; }
        [JsonProperty("Disabled")]
        public bool Disabled { get; set; }
    }
}