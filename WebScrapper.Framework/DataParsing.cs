using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapper.Framework.Exceptions;
using WebScrapper.Framework.StaticPropeties;

namespace WebScrapper.Framework
{
    public static class DataParsing 
    {
        public static List<PageDetails> GetPageDetails(List<HtmlNode> htmlsNodes)
        {
            var lstPageDetails = new List<PageDetails>();

            Console.WriteLine("Parsing Colected Data");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            for (int i = 0; i < htmlsNodes.Count; i++)
            {
                var pageDetails = new PageDetails();

                pageDetails.CurrencyName = htmlsNodes[i].InnerHtml;

                ++i;

                pageDetails.BuyingRate = htmlsNodes[i].InnerHtml;

                ++i;

                pageDetails.CashBuyingRate = htmlsNodes[i].InnerHtml;

                ++i;

                pageDetails.SellingRate = htmlsNodes[i].InnerHtml;

                ++i;

                pageDetails.CashSellingRate = htmlsNodes[i].InnerHtml;

                ++i;

                pageDetails.MiddleRate = htmlsNodes[i].InnerHtml;

                ++i;

                pageDetails.PubTime = htmlsNodes[i].InnerHtml;

                lstPageDetails.Add(pageDetails);

            }
            Console.WriteLine("Data Parsed");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            return lstPageDetails;
        }

        public static List<HtmlNode> GetDataFromSubmittedForm(string url, string curency)
        {
            int curentPage = 0;
            int currentPageOnHtml = 1;

            List<HtmlNode> data = new List<HtmlNode>();

            do
            {
                curentPage++;

                var formResult = HtmlManipulation.SubmitForm(url, curency, curentPage, out string pageOnHtml);

                var htmlDataNodes = formResult.CssSelect(".hui12_20").ToList();

                currentPageOnHtml = int.Parse(pageOnHtml);

                Console.WriteLine($"Geting data from page {currentPageOnHtml}");
                Console.WriteLine("-----------------------------------------------------------------------------------------");



                if (htmlDataNodes[0].InnerHtml == StaticStrings.NoRecords)
                {
                    throw new NoRecordsException("No records found");
                }

                if (curentPage == currentPageOnHtml)
                    data.AddRange(htmlDataNodes);



            } while (curentPage == currentPageOnHtml);

            Console.WriteLine($"Collected data on {currentPageOnHtml} pages");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            return data;

        }
    }
}
