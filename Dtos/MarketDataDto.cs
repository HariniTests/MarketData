using MarketData.Entities;
namespace MarketData.Dtos
{
    public record MktDataDto {
        //public Guid Id {get; init;}
        public string Id {get; init;}
        public string MktDataType { get; init; }
        public DateTimeOffset RefDate {get; set;}
        public FXData MktData {get; init;}
    }
}