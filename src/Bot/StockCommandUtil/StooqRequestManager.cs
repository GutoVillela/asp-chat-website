using Bot.Constants;
using Bot.OutputDTO;
using System.Globalization;

namespace Bot.StockCommandUtil
{
    public class StooqRequestManager
    {
        private readonly HttpClient _client;

        public StooqRequestManager()
        {
            _client = new HttpClient();
        }

        public async Task<(string message, bool error)> GetStockMessageAsync(string stockCode)
        {
            try
            {
                string csvString = await DownloadCSVAsStringAsync(stockCode);
                StooqCSVModel outputDto = csvString;

                if(outputDto.Low > 0)
                {
                    string resultMessage = string.Format(BotResultMessages.SUCCESS_QUOTE_RESULT, stockCode.ToUpper(), outputDto.Low.ToString("F2", CultureInfo.InvariantCulture));
                    return (resultMessage, false);
                }

                string noResultMessage = string.Format(BotResultMessages.SUCCESS_QUOTE_WITH_NO_RESULT, stockCode.ToUpper());
                return (noResultMessage, true);

            }
            catch (Exception ex)
            {
                return (ex.Message, true);
            }
        }

        public async Task<string> DownloadCSVAsStringAsync(string stockCode)
        {
            HttpResponseMessage response = await _client.GetAsync(string.Format(StooqAPI.API_URL, stockCode));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


    }
}
