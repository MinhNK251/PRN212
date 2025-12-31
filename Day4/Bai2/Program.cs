using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Bai2
{
    internal class Program
    {
        public class ExamScore
        {
            [Name("MaTinh")]
            public int ProvinceCode { get; set; }

            public double? Toan { get; set; }

            [Name("Year")]
            public int Year { get; set; }
        }

        public class Province
        {
            [Name("MaTinh")]
            public int ProvinceCode { get; set; }

            [Name("TenTinh")]
            public string ProvinceName { get; set; }
        }

        static void Main(string[] args)
        {
            string mathCsvFilePath = @"..\..\..\..\Bai1\Data\2017-2021.csv";
            string provinceCsvFilePath = @"..\..\..\..\Bai1\Data\Tinh.csv";

            try
            {
                List<ExamScore> mathScores;
                using (var reader = new StreamReader(mathCsvFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    mathScores = csv.GetRecords<ExamScore>().ToList();
                }

                Dictionary<int, string> provinceNames;
                using (var reader = new StreamReader(provinceCsvFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var provinces = csv.GetRecords<Province>().ToList();
                    provinceNames = provinces.ToDictionary(p => p.ProvinceCode, p => p.ProvinceName);
                }

                var provinceMathAverages = mathScores
                    .Where(s => s.Year == 2017 && s.Toan.HasValue)
                    .GroupBy(s => s.ProvinceCode)
                    .Select(g => new
                    {
                        ProvinceCode = g.Key,
                        ProvinceName = provinceNames.ContainsKey(g.Key) ? provinceNames[g.Key] : "Unknown",
                        AverageMathScore = g.Average(s => s.Toan)
                    })
                    .OrderByDescending(x => x.AverageMathScore)
                    .Take(3);

                Console.WriteLine("Top 3 Provinces with the Highest Math Score Averages in 2017:");
                int count = 1;
                foreach (var province in provinceMathAverages)
                {
                    Console.WriteLine($"{count++}.{province.ProvinceName}: {province.AverageMathScore}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
