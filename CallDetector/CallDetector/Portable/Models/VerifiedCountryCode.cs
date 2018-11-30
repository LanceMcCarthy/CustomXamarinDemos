using Newtonsoft.Json;

namespace CallDetector.Portable.Models
{
    public class VerifiedCountry
    {
        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("dialling_code")]
        public string DiallingCode { get; set; }
    }
}
