using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScrapper.Framework
{
    public static class CSVManipulation
    {
        public static void ExportToCSV(List<PageDetails> lstPageDetails, string currency, string targetDir)
        {
            //Get System root Example: "C:\\"
            string rootDir = Path.GetPathRoot(Environment.SystemDirectory);

            //Combine root with targetDir Predifined in Webscrapper.ConsoleApp settings
            string filePath = Path.Combine(rootDir, targetDir);

            //Check if filepath exists
            bool exists = Directory.Exists(filePath);

            //if filepath does not exist create it
            if (!exists)
            {
                Directory.CreateDirectory(filePath);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Created Output Directory");
                Console.ResetColor();
            }

            //Creating csv and writing information in it
            Console.WriteLine($"Generating CSV for {currency}");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
            using (var writer = new StreamWriter($"{filePath}\\{currency}_{DateTime.Now.ToString("yyyy-MM-dd")}_{DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd")}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(lstPageDetails);
            }

            Console.WriteLine("Created CSV file");
            Console.WriteLine("-----------------------------------------------------------------------------------------");
        }
    }
}
