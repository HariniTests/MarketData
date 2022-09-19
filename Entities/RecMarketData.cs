using System;
namespace MarketData.Entities{
    public record RecMarketData{

        //public Guid Id {get; init;}
        public string Id {get; init;}
        public string MktDataType { get; init; }
        public DateTimeOffset RefDate {get; set;}
        public FXData MktData {get; init;}

        public RecMarketData(string mktdatatype, DateTimeOffset refdate, FXData mktdata)
        {
            Id = mktdatatype+"-"+ refdate.ToString("yyyymmdd")+ "-"+ mktdata.RefCcy;  //create a unique id as combination of type (EX: FX), date and Ref CCy
            MktDataType= mktdatatype;
            MktData = mktdata;
            RefDate= refdate;

        }
    }
    public class FXData {
        
        public string RefCcy { get; set; }
        public double BidPrice { get; set; }
        public double AskPrice {get; set;}
    }
}