using Microsoft.AspNetCore.Mvc;
using MarketData.Api.Repository;
using MarketData.Api.Entities;
using MarketData.Api.Dtos;
using System;
namespace MarketData.Api.Controllers{
[ApiController]
[Route("RecMarketData")]
public class MarketDataGateway : ControllerBase
{
    private readonly IntRepository repo;
    public MarketDataGateway(IntRepository repository)
    {
        this.repo = repository;
        
    }

    [HttpGet]
    public IEnumerable<MktDataDto> GetMarketData()
    {
        return repo.GetMarkets().Select(i => i.AsDto());
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
        RecMarketData recMarketData =new RecMarketData("FX", newMktDataDto.RefDate, newMktDataDto.MktDataDto.AsObj());
        repo.AddMarketData(recMarketData);
        return CreatedAtAction(nameof(GetDatabyId), new {id = recMarketData.Id}, recMarketData.AsDto());
    }
}
}