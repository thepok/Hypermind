using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HypermindLib
{
    //TODO implement split regarding tokening
    public static class Textsplitter
    {
        /// <summary>
        /// First slpits at /n/n than /n than " " and finaly at any point to get below max length if needed (for extremly long words)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="maxLength">in chars</param>
        /// <returns></returns>
        public static string[] SmartStringSplit(string text, int maxLength)
        {
            return SplitText(text, maxLength, new string[] { "\n\n", "\n", " " });
        }
        public static string[] SplitText(string text, int maxLength, params string[] splitmarkers)
        {
            //return text if his length below max
            if (text.Length <= maxLength)
            {
                return new string[] { text };
            }

            //reached end of Recursion, hard splits
            if (splitmarkers.Length == 0)
            {
                return SimpleSplitText(text, maxLength);
            }

            
            var parts = new List<string>();

            // Split text into parts of max length startng with the first splitmarker
            var splits = Regex.Split(text, @"(?<=[" + splitmarkers[0] +"])").ToList();

            foreach(var part in splits)
            {
                // Split the part into parts of max length
                if (part.Length > maxLength)
                {
                    if (splitmarkers.Length > 1)
                    {
                        parts.AddRange(SplitText(part, maxLength, splitmarkers.Skip(1).ToArray()));
                    }
                    else
                    {
                        parts.AddRange(SimpleSplitText(part, maxLength));
                    }
                }
                else
                {
                    parts.Add(part);
                }
            }


            //remerge parts that can be fused together while staying below maxlength
            var newparts = new List<string>();
            var currentpart = "";
            foreach (var part in parts)
            {
                if (currentpart.Length + part.Length < maxLength)
                {
                    currentpart += part;
                }
                else
                {
                    newparts.Add(currentpart);
                    currentpart = part;
                }
            }
            newparts.Add(currentpart);
            return newparts.ToArray();
        }

        public static string[] SimpleSplitText(string text, int maxLength)
        {
            var parts = new List<string>();

            // Split text into parts of max length
            if (text.Length > maxLength)
            {
                var part = text.Substring(0, maxLength);
                parts.Add(part);
                parts.AddRange(SimpleSplitText(text.Substring(maxLength), maxLength));
            }
            else
            {
                parts.Add(text);
            }

            return parts.ToArray();
        }
    }
}
