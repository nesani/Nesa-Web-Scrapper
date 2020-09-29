
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WebScrapper.Framework;
using WebScrapper.Framework.Exceptions;
using WebScrapper.Framework.StaticPropeties;

namespace WebScrapper.ConsoleApp
{

    public class Program
    {
        static void Main(string[] args)
        {

            Dictionary<string, string> result = new Dictionary<string, string>();

            Console.WriteLine("Getting list of all Curencies");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            var allCurrencies = Curencies.GetAllCurencies(Properties.Settings.Default.TargetUrl);

            foreach (var curency in allCurrencies)
            {
                
                Console.WriteLine($"Getting data for: {curency} for past two days");
                Console.WriteLine("-----------------------------------------------------------------------------------------");

                try
                {
                    var dataFromSubmitedFrom = DataParsing.GetDataFromSubmittedForm(Properties.Settings.Default.TargetUrl, curency);
                    var pageDetails = DataParsing.GetPageDetails(dataFromSubmitedFrom);
                    CSVManipulation.ExportToCSV(pageDetails, curency);
                    result.Add(curency, "Successfuly Gathered data");
                }
                catch (NoRecordsException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    result.Add(curency, StaticStrings.NoRecordsForCurrency);
                }
                catch(SubmisionFailedException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    result.Add(curency, StaticStrings.UnableToSubmitForm);
                }

            }

            Console.WriteLine("------------------------------------------RESULT-----------------------------------------");
            foreach (var item in result)
            {
                if (item.Value == StaticStrings.NoRecordsForCurrency)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" curency {item.Key} completed with {item.Value}");
                    Console.ResetColor();
                }else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($" curency {item.Key} completed with {item.Value}");
                    Console.ResetColor();
                }

            }

            Console.ReadKey();
        }


    }
}
