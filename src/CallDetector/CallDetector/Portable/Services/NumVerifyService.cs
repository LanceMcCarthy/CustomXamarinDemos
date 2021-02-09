using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CallDetector.Portable.Models;
using Newtonsoft.Json;

namespace CallDetector.Portable.Services
{
    public class NumVerifyService : IDisposable
    {
        #region Singleton members

        private static NumVerifyService _instance;
        public static NumVerifyService Instance => _instance ?? (_instance = new NumVerifyService());

        #endregion

        private readonly HttpClient _client;

        public NumVerifyService()
        {
            _client = new HttpClient();
        }

        public async Task<VerifiedNumber> ValidateNumberAsync(string phoneNumber)
        {
            try
            {
                var builder = new UriBuilder("http://apilayer.net/validate");

                var query = HttpUtility.ParseQueryString(builder.Query);
                query["access_key"] = "560791ac0660f32cadea46a9925c1240";
                query["number"] = phoneNumber;
                query["format"] = "1";
                query["country_code"] = "US";
                builder.Query = query.ToString();

                using (var response = await _client.GetAsync(builder.ToString()))
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<VerifiedNumber>(json);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<VerifiedCountry>> GetCountryCodesAsync(string phoneNumber)
        {
            try
            {
                var builder = new UriBuilder("http://apilayer.net/countries");

                var query = HttpUtility.ParseQueryString(builder.Query);
                query["access_key"] = "560791ac0660f32cadea46a9925c1240";
                builder.Query = query.ToString();

                using (var response = await _client.GetAsync(builder.ToString()))
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<VerifiedCountry>>(json);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
