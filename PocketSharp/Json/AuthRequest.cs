using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace PocketSharp.Json
{
    [DataContract]
    class AuthRequest
    {
        [DataMember(Name = "consumer_key")]
        public string ConsumerKey { get; set; }

        [DataMember(Name = "redirect_uri")]
        public string RedirectUri { get; set; }
    }
}
