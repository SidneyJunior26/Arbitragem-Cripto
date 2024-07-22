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

    public OpportunityController(IOpportunityService opportunityService)
    {
        _opportunityService = opportunityService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var opportunities = await _opportunityService.GetAllAsync();

        if (!opportunities.Any())
            return NotFound(new ErrorViewModel(404, "Any opportunity was found", "Any opportunity was found"));
        
        return Ok(opportunities);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        var opportunity = await _opportunityService.GetByIdAsync(id);

        if (opportunity == null)
            return NotFound(new ErrorViewModel(404, "Opportunity not found", "Opportunity not found"));
        
        return Ok(opportunity);
    }
}