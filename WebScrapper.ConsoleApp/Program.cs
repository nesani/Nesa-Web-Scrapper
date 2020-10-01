
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
            //Initializing Result Dictionary that will be printed after all curencies are retrieved
            Dictionary<string, string> result = new Dictionary<string, string>();

            Console.WriteLine("Getting list of all Curencies");
            Console.WriteLine("-----------------------------------------------------------------------------------------");

            //Getting list of all Curencies
            var allCurrencies = Curencies.GetAllCurencies(Properties.Settings.Default.TargetUrl);

            //For Every Currency Get Data, Parse it and Create CSV file
            foreach (var curency in allCurrencies)
            {
                
                Console.WriteLine($"Getting data for: {curency} for past two days");
                Console.WriteLine("-----------------------------------------------------------------------------------------");

                try
                {
                    //Getting data from submitted form
                    var dataFromSubmitedFrom = DataParsing.GetDataFromSubmittedForm(Properties.Settings.Default.TargetUrl, curency);

                    //Parsing Data in appropriate model
                    var pageDetails = DataParsing.GetPageDetails(dataFromSubmitedFrom);

                    //Creating  CSV
                    CSVManipulation.ExportToCSV(pageDetails, curency, Properties.Settings.Default.TargetDir);
                    result.Add(curency, "Successfuly Gathered data");
                }

                //If No Records are displayed for specific currency throw custom error
                catch (NoRecordsException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{ex.Message} for {curency}");
                    Console.ResetColor();
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    result.Add(curency, StaticStrings.NoRecordsForCurrency);
                }
                // If Submission fails for specific page, throw custom error
                catch(SubmisionFailedException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{ex.Message} for {curency}");
                    Console.ResetColor();
                    Console.WriteLine("-----------------------------------------------------------------------------------------");
                    result.Add(curency, StaticStrings.UnableToSubmitForm);
                }

            }

            //Print result of beforementioned dictionary
            Console.WriteLine("------------------------------------------RESULT-----------------------------------------");
            foreach (var item in result)
            {
                if (item.Value == StaticStrings.NoRecordsForCurrency)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" curency {item.Key} completed with {item.Value}");
                    Console.ResetColor();
                }
                else if(item.Value == StaticStrings.UnableToSubmitForm)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($" curency {item.Key} completed with {item.Value}");
                    Console.ResetColor();
                }
                
                else
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
