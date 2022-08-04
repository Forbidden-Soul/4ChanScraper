using HtmlAgilityPack;
using System;
using System.Linq;

namespace _4ChanScraper
{
    class Program
    {
        static bool run = true;
        static Uri uri;
        static System.Net.WebClient client = new System.Net.WebClient();
        static HtmlDocument doc = new HtmlDocument();
        static HtmlNodeCollection nod;
        static void Main(string[] args)
        {
            while (run)
            {
                Console.Clear();
                Console.WriteLine("Enter Web Address To Scrape:");
                if (Uri.TryCreate(Console.ReadLine(), UriKind.Absolute, out uri))
                {
                    Console.WriteLine("Using folder: " + uri.Segments[uri.Segments.Length - 1]);
                    System.IO.Directory.CreateDirectory(uri.Segments[uri.Segments.Length - 1]);
                    doc.LoadHtml(client.DownloadString(uri));
                    nod = doc.DocumentNode.SelectNodes("//*[contains(@class,'fileText')]");
                    foreach (var f in nod)
                    {
                        Console.WriteLine("Downloading: " + f.InnerText);
                        client.DownloadFile("https:" + f.SelectSingleNode("a").Attributes["href"].Value, uri.Segments[uri.Segments.Length - 1] + "/" + System.IO.Path.GetFileName(f.SelectSingleNode("a").Attributes["href"].Value));
                        //Console.WriteLine(f.SelectSingleNode("a").Attributes["href"].Value);
                        //Console.WriteLine(uri.Segments[uri.Segments.Length - 1] + "/" + System.IO.Path.GetFileName(f.SelectSingleNode("a").Attributes["href"].Value));
                    }
                }
                else
                {
                    Console.WriteLine("Invalid URL");                    
                }
                Console.ReadKey();
            }
        }
    }
}
