using MarketData.Api.Entities;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Globalization;
namespace MarketData.Api.Repository
{
    public class InMemoryRepository :IntRepository
    {
        //Using a temporary cache - to show the existing MarketData. In production scenario, this should be a databse.
        //accordinly alter the service registration details in program.cs 
        private readonly DateTimeOffset fxDate= DateTimeOffset.ParseExact("2022-09-22","yyyy-mm-dd",CultureInfo.InvariantCulture) ;
        private List<RecMarketData> RecMktData =new()
        {
                new RecMarketData ("FX", DateTimeOffset.Now, new FXData { RefCcy="USD", BidPrice=1.01, AskPrice=1.03}),
                new RecMarketData ("FX", DateTimeOffset.Now, new FXData { RefCcy="SGD", BidPrice=100, AskPrice=101}),
                new RecMarketData ("FX", DateTimeOffset.Now, new FXData { RefCcy="CAD", BidPrice=0.98, AskPrice=1.01})
        };
        public IEnumerable<RecMarketData> GetMarkets()
        {
            return RecMktData;
        }
        public RecMarketData GetMarketDataById(string id)
        {
            return RecMktData.Where(i =>i.Id ==id).SingleOrDefault();    

        }
        public void AddMarketData(RecMarketData recMarketData)
        {
            RecMktData.Add(recMarketData);
        }
    }

}