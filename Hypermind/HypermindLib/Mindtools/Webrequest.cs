using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace HypermindLib
{
    public class Webrequest
    {
        /// <summary>
        /// Get the Text stripped of html from an website at the provided url. Some magic is used to find the url in the provided string for ease of use.
        /// </summary>
        /// <param name="url"></param>
        /// <returns>readable text of website</returns>
        /// <exception cref="InvalidDataException"></exception>
        public string Get(string url)
        {
            url = url.Trim();

            Regex urlRegex = new Regex(@"http(s)?:\/\/[a-z0-9]+([\-\.]{1}[a-z0-9]+)*\.[a-z]{2,5}(:[0-9]{1,5})?(\/.*)?");
            Match urlMatch = urlRegex.Match(url);
            if (urlMatch.Success)
            {
                var filteredUrl = urlMatch.Value;
                
                //remove last char from filteredUrl if it is a point
                if(filteredUrl.EndsWith("."))
                {
                    filteredUrl = filteredUrl[..^1];
                }

                HttpClient client = new HttpClient();

                var html = client.GetStringAsync(filteredUrl);
                html.Wait();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html.Result);
                string result = doc.DocumentNode.InnerText;

                return result;
            }
            else
            {
                throw new InvalidDataException("Url was faulty. " + url);
            }
        }
    }
}
