using System;
namespace MarketData.Api.Entities{
    public record RecMarketData{

        //public Guid Id {get; init;}
        public string Id {get; init;}
        public string MktDataType { get; init; }
        public DateTimeOffset RefDate {get; set;}
        public FXData MktData {get; init;}

        public RecMarketData(string mktdatatype, DateTimeOffset refdate, FXData mktdata)
        {
            if(mktdatatype=="FX")
            {
                FXData fxMktData =(FXData) mktdata;
                Id = mktdatatype+"-"+ refdate.ToString("yyyymmdd")+ "-"+ fxMktData.RefCcy;  //create a unique id as combination of type (EX: FX), date and Ref CCy
                MktDataType= mktdatatype;
                MktData = mktdata;
                RefDate= refdate;

            }

        }
    }
    public class FXData :MarketDataStruct {
        
        public string RefCcy { get; set; }
        public double BidPrice { get; set; }
        public double AskPrice {get; set;}
    }
}