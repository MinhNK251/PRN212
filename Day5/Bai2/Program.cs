using System.Globalization;
using System.Text.Json;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;

namespace Bai2
{
    public class ExamScore
    {
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
    }

    [Serializable]
    public class SubjectCount
    {
        public string Year { get; set; }
        public Dictionary<string, int> SubjectCounts { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string csvFilePath = @"..\..\..\..\Bai1\Data\2017-2021.csv";
            string[] Scopes = { SheetsService.Scope.Spreadsheets };
            string ApplicationName = "WriteToGoogleSheet";

            try
            {
                List<ExamScore> records;
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<ExamScore>().ToList();
                }

                var subjects = new[] { "Toan", "Van", "Ly", "Sinh", "Ngoai Ngu", "Hoa", "Lich Su", "Dia Ly", "GDCD" };
                var studentCountPerYear = records
                    .GroupBy(r => r.Year)
                    .Select(g => new SubjectCount
                    {
                        Year = g.Key.ToString(),
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
                    .OrderBy(x => x.Year)
                    .ToList();

                var jsonFilePath = @"..\..\..\..\result.json";
                var jsonString = JsonSerializer.Serialize(studentCountPerYear, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(jsonFilePath, jsonString);
                Console.WriteLine("Data successfully exported to JSON.");

                var xmlFilePath = @"..\..\..\..\result.xml";
                var xmlString = new System.Xml.Linq.XDocument(
                    new System.Xml.Linq.XElement("Root",
                        studentCountPerYear.Select(sc => new System.Xml.Linq.XElement("SubjectCount",
                            new System.Xml.Linq.XElement("Year", sc.Year),
                            new System.Xml.Linq.XElement("SubjectCounts",
                                sc.SubjectCounts.Select(kv =>
                                    new System.Xml.Linq.XElement(kv.Key.Replace(' ', '_'), kv.Value)
                                )
                            )
                        ))
                    )
                ).ToString();
                File.WriteAllText(xmlFilePath, xmlString);
                Console.WriteLine("Data successfully exported to XML.");

                UserCredential credential;
                using (var stream = new FileStream(@"..\..\..\..\credentials.json", FileMode.Open, FileAccess.Read))
                {
                    string credPath = "token.json";
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.FromStream(stream).Secrets,
                        Scopes,
                        "user",
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }

                var service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

                var spreadsheetId = "1QdNKC7EIfJBIinf4O0rgsY_VRSJyJ1KIbBSBPFxaRGY";
                var range = "Sheet1!A1";
                var values = new List<IList<object>>();
                var header = new List<object> { "Year" };
                header.AddRange(subjects);
                values.Add(header);

                foreach (var entry in studentCountPerYear)
                {
                    var row = new List<object> { entry.Year };
                    foreach (var subject in subjects)
                    {
                        row.Add(entry.SubjectCounts[subject].ToString("N0", CultureInfo.InvariantCulture));
                    }
                    values.Add(row);
                }

                var valueRange = new ValueRange();
                valueRange.Values = values;
                var appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadsheetId, range);
                appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
                var appendResponse = appendRequest.Execute();
                Console.WriteLine("Data successfully written to Google Sheets.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
