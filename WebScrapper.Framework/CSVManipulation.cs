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
        public static void ExportToCSV(List<PageDetails> lstPageDetails, string currency)
        {

            string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}\\Output";
            bool exists = Directory.Exists(filePath);

            if (!exists)
            {
                Directory.CreateDirectory($"{AppDomain.CurrentDomain.BaseDirectory}\\Output");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Created Output Directory");
                Console.ResetColor();
            }

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
