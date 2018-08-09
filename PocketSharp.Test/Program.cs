using System;
using System.Diagnostics;

namespace PocketSharp.Test
{
    class Program
    {
        const string CONSUMER_KEY = "1234-abcd1234abcd1234abcd1234";

        static void Main(string[] args)
        {
            PocketClient pocket = new PocketClient(CONSUMER_KEY);
            string authUrl = pocket.GetAuthUrlAsync().Result;
            Console.WriteLine("AuthUrl: " + authUrl);
            Console.WriteLine("Please auth action.");
            Console.ReadLine();

            if (pocket.SendAuthCompleteAsync().Result)
            {
                Console.WriteLine("Complete.");

                pocket.Add("https://getpocket.com/developer/docs/v3/add", tags: new[] { "test" }).Wait();
            }
        }
    }
}
