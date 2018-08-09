using PocketSharp.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PocketSharp
{
    public class PocketClient : IPocketClient
    {
        private static string API_AUTH { get; } = "https://getpocket.com/v3/oauth/authorize";
        private static string API_AUTH_REQUEST { get; } = "https://getpocket.com/v3/oauth/request";
        private static string API_AUTH_BASE_ADDRESS { get; } = "https://getpocket.com/auth/authorize?request_token={0}";
        private static string API_ADD { get; } = "https://getpocket.com/v3/add";
        private static PocketHttpHandler Handler { get; } = new PocketHttpHandler();
        private static HttpClient Client { get; } = new HttpClient(Handler);

        public string AuthCode { get; private set; }
        public string ConsumerKey { get; private set; }
        public string AccessToken { get; private set; }
        public string UserName { get; private set; }
        
        public static IPocketClient GetInstance(string consumerKey)
        {
            return GetInstance(consumerKey, string.Empty);
        }

        public static IPocketClient GetInstance(string consumerKey, string accessToken)
        {
            return new PocketClient(consumerKey, accessToken);
        }

        private PocketClient(string consumerKey) : this(consumerKey, string.Empty) { }

        private PocketClient(string consumerKey, string accessToken)
        {
            ConsumerKey = consumerKey;
            AccessToken = accessToken;
        }

        private async Task<HttpResponseMessage> PostJsonAsync<T>(string requestUri, T obj)
        {
            string content = JsonHelper.Serialize(obj);

            var json = new StringContent(content, Encoding.UTF8, "application/json");

            return await Client.PostAsync(requestUri, json);
        }

        private async Task<string> GetAuthCodeAsync()
        {
            var response = await PostJsonAsync(API_AUTH_REQUEST, new AuthRequest
            {
                ConsumerKey = ConsumerKey,
                RedirectUri = "pocketapp1234:authorizationFinished",
            });

            var code = await response.Content.ReadAsStringAsync();

            return Regex.Match(code, "^code=(.*)$").Groups[1].Value;
        }

        public async Task<string> GetAuthUrlAsync()
        {
            AuthCode = await GetAuthCodeAsync();
            string authUrl = string.Format(API_AUTH_BASE_ADDRESS, AuthCode);

            return authUrl;
        }

        public async Task<bool> SendAuthCompleteAsync()
        {
            var response = await PostJsonAsync(API_AUTH, new Auth
            {
                ConsumerKey = ConsumerKey,
                Code = AuthCode,
            });

            var content = await response.Content.ReadAsStringAsync();
            AccessToken = Regex.Match(content, @"(?<=access_token=)([0-9a-z\-]*)").Groups[1].Value;
            UserName = Regex.Match(content, @"(?<=username=)([0-9a-z\-]*)").Groups[1].Value;

            if (string.IsNullOrWhiteSpace(AccessToken) || string.IsNullOrWhiteSpace(UserName))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        public async Task Add(string url, string title = "", string tweetId = "", IEnumerable<string> tags = null)
        {
            var response = await PostJsonAsync(API_ADD, new Add
            {
                ConsumerKey = ConsumerKey,
                AccessToken = AccessToken,
                Url = url,
                Title = title,
                Tags = string.Join(",", tags?.ToArray() ?? new string[0]),
                TweetId = tweetId,
            });
        }
    }
}
