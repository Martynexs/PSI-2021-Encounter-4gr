using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSI.Models
{
    public class FacebookProfileData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
