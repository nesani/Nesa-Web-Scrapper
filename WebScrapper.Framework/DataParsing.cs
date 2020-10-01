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
        //parsing Data to a Model
        public static List<PageDetails> GetPageDetails(List<HtmlNode> htmlsNodes)
        {
            var lstPageDetails = new List<PageDetails>();

            Console.WriteLine("Parsing Colected Data");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            //Table data from site is taken in order it is displayed
            // #Not my proudest moment
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

        //Getting data from a submitted form
        public static List<HtmlNode> GetDataFromSubmittedForm(string url, string curency)
        {
            int curentPage = 0;
            int currentPageOnHtml = 1;

            List<HtmlNode> data = new List<HtmlNode>();

            //Because i was unable to get last page, i had to improvise,
            //Bank page ALWAYS returns last page in it's hidden form field
            //So i made logic around it
            do
            {
                curentPage++;

                // Submitting form
                var formResult = HtmlManipulation.SubmitForm(url, curency, curentPage, out string pageOnHtml);

                //Sellecting all elements with specific css, in this case "hui12_20"
                // which is table elements with output
                var htmlDataNodes = formResult.CssSelect(".hui12_20").ToList();

                currentPageOnHtml = int.Parse(pageOnHtml);

                Console.WriteLine($"Geting data from page {currentPageOnHtml}");
                Console.WriteLine("-----------------------------------------------------------------------------------------");


                // if no records are found  throw error
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
