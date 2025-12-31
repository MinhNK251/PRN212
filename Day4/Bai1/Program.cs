using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;
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

                    var subjects = new[] { "Toan", "Van", "Ly", "Sinh", "NgoaiNgu", "Hoa", "LichSu", "DiaLy", "GDCD" };

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
                                { "NgoaiNgu", g.Count(r => r.NgoaiNgu.HasValue) },
                                { "Hoa", g.Count(r => r.Hoa.HasValue) },
                                { "LichSu", g.Count(r => r.LichSu.HasValue) },
                                { "DiaLy", g.Count(r => r.DiaLy.HasValue) },
                                { "GDCD", g.Count(r => r.GDCD.HasValue) }
                            }
                        })
                        .OrderBy(x => x.Year);

                    foreach (var entry in studentCountPerYear)
                    {
                        Console.WriteLine($"{entry.Year}:");
                        foreach (var subject in subjects)
                        {
                            Console.WriteLine($"{subject}: {entry.SubjectCounts[subject]} Hoc Sinh");
                        }
                        Console.WriteLine();
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
