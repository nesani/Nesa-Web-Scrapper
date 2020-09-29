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

        public static HtmlNode GetHtml(string url)
        {
            WebPage webpage = sBrowser.NavigateToPage(new Uri(url));
            return webpage.Html;
        }

        public static HtmlNode SubmitForm(string url, string curency, int page, out string pageOnHtml)
        {
            WebPage resultsPage = null;
            WebPage webpage = null;
            try
            {
                 webpage = sBrowser.NavigateToPage(new Uri(url));
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(StaticStrings.UnableToSubmitForm);
                Console.ResetColor();

                throw new NavigationFailedException(StaticStrings.UnableToNavigateToPage);
            }
            PageWebForm form = webpage.FindFormById(StaticStrings.HistorySearchForm);

            form.Action = "";
            form["erectDate"] = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd");
            form["nothing"] = DateTime.Now.ToString("yyyy-MM-dd");
            form["pjname"] = curency;

            form["page"] = page.ToString();

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
