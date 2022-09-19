using Microsoft.AspNetCore.Mvc;
using MarketData.Api.Repository;
using MarketData.Api.Entities;
using MarketData.Api.Dtos;
using MarketData.Api.Validations;
using MarketData.Api.Extensions;
namespace MarketData.Api.Controllers{
[ApiController]
[Route("RecMarketData")]
public class MarketDataGateway : ControllerBase
{
    private readonly IntRepository repo;
    private readonly ILogger<MarketDataGateway> logger;
    private readonly IValidationService validationService;
    public MarketDataGateway(IntRepository repository, ILogger<MarketDataGateway> logger, IValidationService validationservice)
    {
        this.repo = repository;
        this.logger =logger;
        this.validationService= validationservice;
    }

    [HttpGet]
    public IEnumerable<MktDataDto> GetMarketData()
    {
        var mktdata= repo.GetMarkets().Select(i => i.AsDto());
        logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} : Retrieved {mktdata}");
        return mktdata;
    }

    [HttpGet("{id}")]
    public ActionResult<MktDataDto> GetDatabyId(string id)
    {
        var mktdata =repo.GetMarketDataById(id);
        if (mktdata is null)
            return NotFound();
        return mktdata.AsDto();
    }

    [HttpPost]
    public ActionResult<RecMarketData> CreateFXData (NewMarketDataDto newMktDataDto)
    {
        if (newMktDataDto.MktDataType=="FX")
        {
            FXDataDto fxdataDto = (FXDataDto) newMktDataDto.MktDataDto;
            RecMarketData recMarketData =new RecMarketData("FX",newMktDataDto.RefDate, fxdataDto.AsObj()  );
            if (validationService.ValidateMarketData(recMarketData))
            {
                repo.AddMarketData(recMarketData);
                return CreatedAtAction(nameof(GetDatabyId), new {id = recMarketData.Id}, recMarketData.AsDto());
            }

        }
        return BadRequest();
        
    }
}
}