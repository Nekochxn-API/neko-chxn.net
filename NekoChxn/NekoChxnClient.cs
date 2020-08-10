using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace NekoChxn
{
    public class NekoChxnClient
    {
        private readonly HttpClient Client = new HttpClient();
        private async Task<NekoChxnResponse> Fetch(string url)
        {
            NekoChxnResponse apiRes = null;
            HttpResponseMessage res = null;
            try
            {
                res = await Client.GetAsync(url);
                res.EnsureSuccessStatusCode();
                string rawJson = await res.Content.ReadAsStringAsync();
                var json = JObject.Parse(rawJson);
                apiRes = new NekoChxnResponse(json, res.IsSuccessStatusCode, (int)res.StatusCode);
            }
            catch (Exception e)
            {
                apiRes = new NekoChxnResponse(null, false, res == null ? 404 : (int)res.StatusCode, e.Message);
            }
            return apiRes;
        }
    }

    public class NekoChxnResponse
    {
        public readonly string Url = null;
        public readonly bool Ok;
        public readonly int StatusCode;
        public readonly string ErrorMessage;
        public NekoChxnResponse(JObject json, bool success, int statusCode, string error = null)
        {
            StatusCode = statusCode;
            ErrorMessage = error;
            Ok = success;

            if (json == null || !json.ContainsKey("url"))
            {
                Ok = false;
            }
            else
            {
                Url = (string)json["url"];
            }
        }
    }
}
