using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace CustomerWeb.Models.Common
{
    [Serializable]
    public class ResponseAPI<T>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("messages")]
        public List<string> Messages { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
