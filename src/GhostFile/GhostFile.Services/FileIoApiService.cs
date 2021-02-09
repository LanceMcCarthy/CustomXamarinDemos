using System.Net.Http;
using System.Threading.Tasks;
using GhostFile.Services.Models;
using Newtonsoft.Json;

namespace GhostFile.Services
{
    public class FileIoApiService
    {
        private static FileIoApiService _instance;
        public static FileIoApiService Instance => _instance ?? (_instance = new FileIoApiService());

        private readonly HttpClient _client;

        public FileIoApiService()
        {
            _client = new HttpClient();
        }

        public async Task<FileIoApiResult> UploadFileAsync(byte[] fileBytes, string expiration)
        {
            var bac = new ByteArrayContent(fileBytes);

            //var f = new StreamContent(fileBytes);

            var url = "https://file.io";

            if (string.IsNullOrEmpty(expiration))
            {
                url += $"?{expiration}";
            }

            using (var response = await _client.PostAsync(url, bac))
            {
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FileIoApiResult>(json);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<byte[]> DownloadFileAsync(string code)
        {
            byte[] response = await _client.GetByteArrayAsync($"https://file.io/{code}");
            return response;
        }
    }
}
