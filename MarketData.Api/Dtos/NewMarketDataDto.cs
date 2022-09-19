using MarketData.Api.Entities;
using System.ComponentModel.DataAnnotations;
namespace MarketData.Api.Dtos
{
    public record NewMarketDataDto
    {
        [Required]
        public string MktDataType { get; init; }
        [Required]
        public DateTimeOffset RefDate {get; set;}
        [Required]
        public FXDataDto MktDataDto {get; init;}
    }
    public record FXDataDto :MarketDataStruct
    {
        [Required]
        public string RefCcy { get; set; }
        [Required]
        [Range(0.001, 999)]
        public double BidPrice { get; set; }
        [Range(0.001, 999)]
        public double AskPrice {get; set;}
    }
}
