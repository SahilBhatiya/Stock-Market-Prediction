using StockMarket.Models;
using YahooFinanceApi;

namespace StockMarket.Custom
{
    public class StockData
    {

        public async Task<String> getStockData(string symbol,DateTime startDate,DateTime EndDate )
        {
            List<StockDisplay> data = new List<StockDisplay>();
            string Data = String.Empty;
            try
            {
                IReadOnlyList<Candle>? historicData = await Yahoo.GetHistoricalAsync(symbol,startDate,EndDate);
                var security = await Yahoo.Symbols(symbol).Fields(Field.LongName).QueryAsync();
                var ticker = security[symbol];
                var companyName = ticker[Field.LongName];
                for ( int i = 0 ; i < historicData.Count ; i++ )
                {
                    data.Add(new StockDisplay( )
                    {
                        date = historicData.ElementAt(i).DateTime,
                        ClosePrice = historicData.ElementAt(i).Close,
                        Volume = historicData.ElementAt(i).Volume,
                    });
                }
                Data = Newtonsoft.Json.JsonConvert.SerializeObject(data.ToArray());
            }
            catch(Exception e )
            {
                Data = e.ToString( );
            }

            return Data;
        }
    }
}
