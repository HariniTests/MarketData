using MarketData.Api.Entities;
namespace MarketData.Api.Validations
{
    public interface IValidationService
    {
        public bool ValidateMarketData(RecMarketData marketData);

    }
}