using Xunit;
using Moq;
using MarketData.Api.Repository;
using MarketData.Api.Entities;
using MarketData.Api.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using MarketData.Api.Validations;
using FluentAssertions;
using MarketData.Api.Dtos;
using MarketData.Api.Extensions;
namespace MarketData.UnitTests;


public class GatewayTests
{
    private readonly Mock<IntRepository> repostub =new();
    private readonly Mock<ILogger<MarketDataGateway>> loggerstub = new();
    private readonly Mock<IValidationService> validationstub =new();
    [Fact]
    public void GetDataById_InvalidId()
    {
        
        repostub.Setup(repo => repo.GetMarketDataById("NonExistingID")).Returns((RecMarketData)null);
        var mktdataGateway = new MarketDataGateway(repostub.Object, loggerstub.Object, validationstub.Object);
        var result = mktdataGateway.GetDatabyId("NoID");
        result.Result.Should().BeOfType<NotFoundResult>();
    }
    [Fact]
    public void GetDatabyId_ExistingId()
    {
        string MktDataId = "FX-"+ DateTimeOffset.Now.ToString("yyyymmdd")+ "-SGD";
        var expectedData= new RecMarketData ("FX", DateTimeOffset.Now, new FXData { RefCcy="SGD", BidPrice=100, AskPrice=101});
        repostub.Setup(repo => repo.GetMarketDataById(MktDataId)).Returns(expectedData);
        var mktdataGateway = new MarketDataGateway(repostub.Object, loggerstub.Object, validationstub.Object);
        var result = mktdataGateway.GetDatabyId(MktDataId);
        result.Value.Should().BeEquivalentTo(expectedData, options =>options.ComparingByMembers<RecMarketData>());
    }
    [Fact]
    public void GetAllItemsTest()
    {
        var expectedData =new []
        {   
            new RecMarketData ("FX", DateTimeOffset.Now, new FXData { RefCcy="USD", BidPrice=1.01, AskPrice=1.03}),
            new RecMarketData ("FX", DateTimeOffset.Now, new FXData { RefCcy="SGD", BidPrice=100, AskPrice=101}),
            new RecMarketData ("FX", DateTimeOffset.Now, new FXData { RefCcy="CAD", BidPrice=0.98, AskPrice=1.01})
    };
        repostub.Setup(repo =>repo.GetMarkets()).Returns(expectedData);
        var mktdataGateway = new MarketDataGateway(repostub.Object, loggerstub.Object, validationstub.Object);
        var results = mktdataGateway.GetMarketData();
        results.Should().BeEquivalentTo(expectedData, options=>options.ComparingByMembers<RecMarketData>());


    }
    [Fact]
    public void MarketDataSubmissionTest_ValidData()
    {
        FXDataDto inputFxDataDto= new FXDataDto { RefCcy="INR", BidPrice=10, AskPrice=101};
        NewMarketDataDto inputMktDataDto= new NewMarketDataDto{MktDataType ="FX", RefDate=DateTimeOffset.Now, MktDataDto=inputFxDataDto};
        RecMarketData recmktdata = new RecMarketData ("FX", DateTimeOffset.Now, new FXData { RefCcy="INR", BidPrice=12, AskPrice=13});
        ValidationService vs = new ValidationService();
        //somereason not able to mockit using Stub. So adding the validationservice here
        var mktdataGateway = new MarketDataGateway(repostub.Object, loggerstub.Object, vs);
        var results = mktdataGateway.CreateFXData(inputMktDataDto);
       
        var outputaftersubmission = (results.Result as CreatedAtActionResult).Value ;
        outputaftersubmission.Should().BeOfType<MktDataDto>();
        var mktdtaaftersub =outputaftersubmission as MktDataDto;
        //as input dto and output dto are diff slightly - using Asert.Equal
        Assert.Equal(mktdtaaftersub.MktDataType, inputMktDataDto.MktDataType);
        Assert.Equal(mktdtaaftersub.MktData.RefCcy, inputMktDataDto.MktDataDto.RefCcy);
        Assert.Equal(mktdtaaftersub.MktData.BidPrice, inputMktDataDto.MktDataDto.BidPrice);
        Assert.Equal(mktdtaaftersub.MktData.AskPrice, inputMktDataDto.MktDataDto.AskPrice);


    }
    [Fact]
        public void MarketDataSubmissionTest_InValidData()
    {
        FXDataDto inputFxDataDto= new FXDataDto { RefCcy="INR", BidPrice=10, AskPrice=101};
        NewMarketDataDto inputMktDataDto= new NewMarketDataDto{MktDataType ="NonFX", RefDate=DateTimeOffset.Now, MktDataDto=inputFxDataDto};
        var mktdataGateway = new MarketDataGateway(repostub.Object, loggerstub.Object, validationstub.Object);
        var results = mktdataGateway.CreateFXData(inputMktDataDto);
        results.Result.Should().BeOfType<BadRequestResult>();

    }
}