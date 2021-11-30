using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSI.Models
{
    public class GoogleProfileData
    {
        [JsonProperty("id")]
        public string Id;
    }
}
