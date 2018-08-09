using System.Collections.Generic;
using System.Threading.Tasks;

namespace PocketSharp
{
    public interface IPocketClient
    {
        string AccessToken { get; }
        string AuthCode { get; }
        string ConsumerKey { get; }
        string UserName { get; }

        Task Add(string url, string title = "", string tweetId = "", IEnumerable<string> tags = null);
        Task<string> GetAuthUrlAsync();
        Task<bool> SendAuthCompleteAsync();
    }
}