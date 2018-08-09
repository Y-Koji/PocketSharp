using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PocketSharp.Json
{
    [DataContract]
    class Add
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "tags")]
        public string Tags { get; set; }

        [DataMember(Name = "tweet_id")]
        public string TweetId { get; set; }

        [DataMember(Name = "consumer_key")]
        public string ConsumerKey { get; set; }

        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }
}
