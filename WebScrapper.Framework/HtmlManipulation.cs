using HtmlAgilityPack;
using ScrapySharp.Html.Forms;
using ScrapySharp.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScrapper.Framework.Exceptions;
using WebScrapper.Framework.StaticPropeties;

namespace WebScrapper.Framework
{
    public static class HtmlManipulation
    {
        static ScrapingBrowser sBrowser = new ScrapingBrowser();

        //Navigating to page using ScrappySharp nugget package
        public static HtmlNode GetHtml(string url)
        {
            WebPage webpage = sBrowser.NavigateToPage(new Uri(url));
            return webpage.Html;
        }

        //Submitting form using ScrappySharp nugget package
        public static HtmlNode SubmitForm(string url, string curency, int page, out string pageOnHtml)
        {
            WebPage resultsPage = null;
            WebPage webpage = null;

            //Navigating to Bank Page
            try
            {
                 webpage = sBrowser.NavigateToPage(new Uri(url));
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(StaticStrings.UnableToNavigateToPage);
                Console.ResetColor();

                throw new NavigationFailedException(StaticStrings.UnableToNavigateToPage);
            }

            //Search for from by its Id
            PageWebForm form = webpage.FindFormById(StaticStrings.HistorySearchForm);

            //Because url page is already targeting action, it must be ommited, so scrappy can do its work.
            form.Action = "";

            // form start date
            form["erectDate"] = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");

            //form end date
            form["nothing"] = DateTime.Now.ToString("yyyy-MM-dd");

            //form currency 
            form["pjname"] = curency;

            // form page
            form["page"] = page.ToString();

            // try to submitt form, if it fails throw error
            try
            {
                resultsPage = form.Submit();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(StaticStrings.UnableToSubmitForm);
                Console.ResetColor();

                throw new SubmisionFailedException(StaticStrings.UnableToSubmitForm);
            }

            PageWebForm formOnResult = resultsPage.FindForm(StaticStrings.PageHiddenForm);

            pageOnHtml = formOnResult["page"];

            return resultsPage.Html;

        }
    }
}
