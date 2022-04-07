using CsvHelper;
using System.Globalization;

namespace Bot.OutputDTO
{
    public class StooqCSVModel
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public string Volume { get; set; }

        public static implicit operator StooqCSVModel(string csvString)
        {
            if (string.IsNullOrEmpty(csvString))
                return new();

            using var reader = new StringReader(csvString);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<StooqCSVModel>();
            return records.FirstOrDefault();
        }
    }
}
