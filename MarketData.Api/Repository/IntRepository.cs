using MarketData.Api.Entities;
namespace MarketData.Api.Repository {


    public interface IntRepository
    {
    public IEnumerable<RecMarketData> GetMarkets();
    public RecMarketData GetMarketDataById(string id);
    public void AddMarketData (RecMarketData recMarketData);
    }
}
