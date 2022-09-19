using MarketData.Api.Entities;
namespace MarketData.Api.Validations{
    public class ValidationService: IValidationService
    {
        public bool ValidateMarketData(RecMarketData marketData)
        {
            //ideally this should implement various validations based on the MarketData type . For now returning True
            //for now just checking if bid price is lower than Ask price
            if(marketData.MktDataType =="FX")
            {
                FXData fxdata= (FXData) marketData.MktData;
                return (fxdata.BidPrice<=fxdata.AskPrice);
            }
            else
            {
                //if any other marketdatatype is passed , for the purpose of this test, it returns false.
                return false;
            }

        }
    }
}