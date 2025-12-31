using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace Bai1
{
    internal class Program
    {
        public class ExamScore
        {
            public string SBD { get; set; }
            public double? Toan { get; set; }
            public double? Van { get; set; }
            public double? Ly { get; set; }
            public double? Sinh { get; set; }

            [Name("Ngoai ngu")]
            public double? NgoaiNgu { get; set; }

            public int Year { get; set; }
            public double? Hoa { get; set; }

            [Name("Lich su")]
            public double? LichSu { get; set; }

            [Name("Dia ly")]
            public double? DiaLy { get; set; }

            public double? GDCD { get; set; }

            [Name("MaTinh")]
            public string Matinh { get; set; }
        }

        static void Main(string[] args)
        {
            string csvFilePath = @"..\..\..\Data\2017-2021.csv";

            try
            {
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ExamScore>().ToList();

                    var subjects = new[] { "Toan", "Van", "Ly", "Sinh", "Ngoai Ngu", "Hoa", "Lich Su", "Dia Ly", "GDCD" };

                    var studentCountPerYear = records
                        .GroupBy(r => r.Year)
                        .Select(g => new
                        {
                            Year = g.Key,
                            SubjectCounts = new Dictionary<string, int>
                            {
                                { "Toan", g.Count(r => r.Toan.HasValue) },
                                { "Van", g.Count(r => r.Van.HasValue) },
                                { "Ly", g.Count(r => r.Ly.HasValue) },
                                { "Sinh", g.Count(r => r.Sinh.HasValue) },
                                { "Ngoai Ngu", g.Count(r => r.NgoaiNgu.HasValue) },
                                { "Hoa", g.Count(r => r.Hoa.HasValue) },
                                { "Lich Su", g.Count(r => r.LichSu.HasValue) },
                                { "Dia Ly", g.Count(r => r.DiaLy.HasValue) },
                                { "GDCD", g.Count(r => r.GDCD.HasValue) }
                            }
                        })
                        .OrderBy(x => x.Year);

                    int subjectNameWidth = subjects.Max(s => s.Length);
                    int countWidth = studentCountPerYear
                        .SelectMany(year => year.SubjectCounts.Values)
                        .Max(count => count.ToString("N0").Length);

                    int cellWidth = Math.Max(subjectNameWidth, countWidth) + 2;

                    Console.Write("Year".PadRight(10) + "|");
                    foreach (var subject in subjects)
                    {
                        Console.Write($"{subject.PadLeft(cellWidth)} |");
                    }
                    Console.WriteLine();
                    Console.WriteLine(new string('-', 10 + 1 + (cellWidth + 2) * subjects.Length));

                    foreach (var entry in studentCountPerYear)
                    {
                        Console.Write($"{entry.Year.ToString().PadRight(10)}|");
                        foreach (var subject in subjects)
                        {
                            Console.Write($"{entry.SubjectCounts[subject].ToString("N0").PadLeft(cellWidth)} |");
                        }
                        Console.WriteLine();
                        Console.WriteLine(new string('-', 10 + 1 + (cellWidth + 2) * subjects.Length));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
