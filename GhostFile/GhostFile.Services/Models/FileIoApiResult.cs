using System;
using Newtonsoft.Json;

namespace GhostFile.Services.Models
{
    public class FileIoApiResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("expiry")]
        public string Expiry { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
