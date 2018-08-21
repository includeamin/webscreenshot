using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string Url = "http://conjugator.reverso.net/conjugation-german-verb-abbrechen.html";
            int count = 0;
            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            WebClient amin = new WebClient();
            var data = amin.DownloadString("http://conjugator.reverso.net/index-german-1-250.html");

            document.LoadHtml(data);
            var sel1 = document.DocumentNode.SelectNodes("//div[contains(@class, 'index-content')]");
            var lis = sel1[0].InnerHtml;

            document.LoadHtml(lis);
            List<string> facts = new List<string>();
            foreach (HtmlNode li in document.DocumentNode.SelectNodes("//li"))
            {
                facts.Add(li.InnerText);
                //  Console.WriteLine(li.InnerText);

            }
            foreach (var a in facts)
            {
                count++;
                var trimed = a.Trim();
                Console.WriteLine($"[{count}] Download [{trimed}] ");
                var url = "http://conjugator.reverso.net/conjugation-german-verb-" + a + ".html";
                WebsitesScreenshot.WebsitesScreenshot _Obj = new WebsitesScreenshot.WebsitesScreenshot();
                WebsitesScreenshot.WebsitesScreenshot.Result _Result;
                //Capture web page for the specified url
                _Result = _Obj.CaptureWebpage(url);
                if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                {
                    _Obj.ImageFormat = WebsitesScreenshot.WebsitesScreenshot.ImageFormats.PNG;
                    _Obj.SaveImage($"{trimed}.png");
                }
                //Convert local web page to image
                //_Result = _Obj.CaptureWebpage("atest.html");
                //if (_Result == WebsitesScreenshot.WebsitesScreenshot.Result.Captured)
                //{
                //    _Obj.ImageFormat = WebsitesScreenshot.WebsitesScreenshot.ImageFormats.PNG;
                //    _Obj.SaveImage("test.png");
                //}
                _Obj.Dispose();
            }

            Console.WriteLine($"End {count}");
            Console.ReadLine();
        }
    }
}
