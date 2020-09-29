using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Framework
{
    public static class Curencies 
    {
     
        public static List<string> GetAllCurencies(string url)
        {
            var allCurency = new List<string>();
            var html = HtmlManipulation.GetHtml(url);

            var curency = html.CssSelect("select > option");

            foreach (var item in curency)
            {
                if (item.GetAttributeValue("value") != "0")
                    allCurency.Add(item.GetAttributeValue("value"));

            }
            return allCurency;
        }
    }
}
