using MarketData.Api.Entities;
using MarketData.Api.Dtos;

namespace MarketData.Api.Extensions {
    public static class Extensions {
        public static MktDataDto AsDto(this RecMarketData origstruct)
        {
            return new MktDataDto
            {
                Id = origstruct.Id,
                MktData =origstruct.MktData,
                RefDate = origstruct.RefDate,
                MktDataType= origstruct.MktDataType
            };
        }

        public static FXDataDto AsDto(this FXData origstruct)
        {
            return new FXDataDto
            {
                RefCcy =origstruct.RefCcy,
                BidPrice = origstruct.BidPrice,
                AskPrice =origstruct.AskPrice
            };
        }


        public static FXData AsObj(this FXDataDto origstruct)
        {
            return new FXData
            {
                RefCcy =origstruct.RefCcy,
                BidPrice = origstruct.BidPrice,
                AskPrice =origstruct.AskPrice
            };
        }
    }
}