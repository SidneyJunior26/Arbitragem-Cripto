using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arbitragem.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class OpportunityController : Controller
{
    private readonly IOpportunityService _opportunityService;
    private readonly IDolarService _dolarService;

    public OpportunityController(IOpportunityService opportunityService, IDolarService dolarService)
    {
        _opportunityService = opportunityService;
        _dolarService = dolarService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var opportunities = await _opportunityService.GetAllAsync();
        var dolar = await _dolarService.GetAsync();

        if (!opportunities.Any())
            return NotFound(new ErrorViewModel(404, "Any opportunity was found", "Any opportunity was found"));

        var opportunityViewModel = opportunities.Select((o, index) => new OpportunityViewModel(
            index + 1,
            o.LowerValue,
            o.HighestValue,
            o.DifferencePercentage,
            o.Crypto.Id,
            o.Crypto.Symbol,
            o.Crypto.Name,
            o.ExchangeToBuy.Id,
            o.ExchangeToBuy.Name,
            o.ExchangeToSell.Id,
            o.ExchangeToSell.Name,
            o.RegisterDate,
            o.ExchangeToBuy.ExchangeUrl,
            o.ExchangeToSell.ExchangeUrl,
            o.ExchangeToBuy.Fee + o.ExchangeToSell.Fee
        ));

        return Ok(new { opportunities = opportunityViewModel, dolar = dolar.Value });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var opportunity = await _opportunityService.GetByIdAsync(id);

        if (opportunity == null)
            return NotFound(new ErrorViewModel(404, "Opportunity not found", "Opportunity not found"));

        return Ok(opportunity);
    }
}