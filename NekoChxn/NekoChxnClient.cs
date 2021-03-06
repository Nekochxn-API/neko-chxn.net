﻿using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace NekoChxn
{
    public class NekoChxnClient
    {
        private const string BaseUrl = "https://api.neko-chxn.xyz/v1/";
        private readonly HttpClient Client = new HttpClient();
        /// <summary>Emits debug info</summary> 
        public Func<string, Task> Debug = null;

        private async Task<NekoChxnResponse> Fetch(string endpoint)
        {
            NekoChxnResponse apiRes = null;
            HttpResponseMessage res = null;
            try
            {
                res = await Client.GetAsync(BaseUrl + endpoint);
                res.EnsureSuccessStatusCode();
                string rawJson = await res.Content.ReadAsStringAsync();
                var json = JObject.Parse(rawJson);
                apiRes = new NekoChxnResponse(endpoint, json, res.IsSuccessStatusCode, (int)res.StatusCode);
                if (Debug != null) _ = Debug($"[NekoChxn] Successfully fetched the {apiRes.Endpoint} endpoint");
            }
            catch (Exception e)
            {
                apiRes = new NekoChxnResponse(endpoint, null, false, res == null ? 404 : (int)res.StatusCode, e.Message);
                if (Debug != null) _ = Debug($"[NekoChxn] Failed fetching the {apiRes.Endpoint} endpoint: {(string.IsNullOrEmpty(apiRes.ErrorMessage) ? "No error" : apiRes.ErrorMessage)}");
            }
            return apiRes;
        }

        /// <summary>Get a blush gif</summary>
        public Task<NekoChxnResponse> Blush() => Fetch("blush/img");

        /// <summary>Get a cry gif</summary>
        public Task<NekoChxnResponse> Cry() => Fetch("cry/img");

        /// <summary>Get a cuddle gif</summary>
        public Task<NekoChxnResponse> Cuddle() => Fetch("cuddle/img");

        /// <summary>Get a dance gif</summary>
        public Task<NekoChxnResponse> Dance() => Fetch("dance/img");

        /// <summary>Get a hug gif</summary>
        public Task<NekoChxnResponse> Hug() => Fetch("hug/img");

        /// <summary>Get a kick gif</summary>
        public Task<NekoChxnResponse> Kick() => Fetch("kick/img");

        /// <summary>Get a kiss gif</summary>
        public Task<NekoChxnResponse> Kiss() => Fetch("kiss/img");

        /// <summary>Get a love gif</summary>
        public Task<NekoChxnResponse> Love() => Fetch("love/img");

        /// <summary>Get a pat gif</summary>
        public Task<NekoChxnResponse> Pat() => Fetch("pat/img");

        /// <summary>Get a punch gif</summary>
        public Task<NekoChxnResponse> Punch() => Fetch("punch/img");

        /// <summary>Get a smirk gif</summary>
        public Task<NekoChxnResponse> Smirk() => Fetch("smirk/img");

        /// <summary>Get a tickle gif</summary>
        public Task<NekoChxnResponse> Tickle() => Fetch("tickle/img");

        /// <summary>Get a yell gif</summary>
        public Task<NekoChxnResponse> Yell() => Fetch("yell/img");

    }

    /// <summary>Represents a NekoChxn api response</summary>
    public class NekoChxnResponse
    {
        /// <summary>The endpoint that was called</summary>
        public readonly string Endpoint;
        /// <summary>The resulting image URL</summary>
        public readonly string Url;
        /// <summary>Indicator whether the request was successful</summary>
        public readonly bool Ok;
        public readonly int StatusCode;
        /// <summary>The error if there was any</summary>
        public readonly string ErrorMessage;

        public NekoChxnResponse(string endpoint, JObject json, bool success, int statusCode, string error = null)
        {
            Endpoint = endpoint;
            StatusCode = statusCode;
            ErrorMessage = error;
            Ok = success;

            if (json == null || !json.ContainsKey("url"))
            {
                Ok = false;
                Url = null;
            }
            else
            {
                Url = (string)json["url"];
            }
        }

        /// <summary>Override to make logging output to the console easier</summary>
        public override string ToString() => $"Endpoint: {Endpoint}\nStatus_Code: {StatusCode}\nOkay: {Ok}\nErrorMessage: {ErrorMessage}\nUrl: {Url}";
    }
}
