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

        //Geting all currencies
        public static List<string> GetAllCurencies(string url)
        {
            // Initialazing currency list
            var allCurency = new List<string>();

            //Go to Predefind site
            var html = HtmlManipulation.GetHtml(url);

            //Selecting all ellements via CSS
            var curency = html.CssSelect("select > option");

            // Getting values from  currencies
            foreach (var item in curency)
            {
                // 0 value is: "select the currency"
                if (item.GetAttributeValue("value") != "0")
                    allCurency.Add(item.GetAttributeValue("value"));

            }
            return allCurency;
        }
    }
}
