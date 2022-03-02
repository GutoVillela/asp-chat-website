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

            string[] csvRows = csvString.Split("\n");
            if(!csvRows.Any() || csvRows.Length < 2)
                return new();

            string[] values = csvRows[1].Split(",");
            if (!values.Any())
                return new();

            
            StooqCSVModel result = new();
            result.Symbol = values[0];
            result.Date = values[1];
            result.Time = values[2];
            result.Open = values[3];
            decimal.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal high);
            decimal.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal low);
            decimal.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal close);
            result.High = high;
            result.Low = low;
            result.Close = close;
            result.Volume = values[7];

            return result;
        }
    }
}
