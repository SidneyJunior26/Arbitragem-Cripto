using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arbitragem.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class ExchangeController : Controller
{
    private readonly IExchangeService _exchangeService;

    public ExchangeController(IExchangeService exchangeService)
    {
        _exchangeService = exchangeService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var exchanges = await _exchangeService.GetAllAsync();
        
        if (!exchanges.Any())
            return NotFound(new ErrorViewModel(404, "Any exchange was found", "Any exchange was found"));
        
        return Ok(exchanges);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        var exchange = await _exchangeService.GetByIdAsync(id);
        
        if (exchange == null)
            return NotFound(new ErrorViewModel(404, "Any exchange was found", "Any exchange was found"));
        
        return Ok(exchange);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = "Adm")]
    public async Task<IActionResult> Post(ExchangeInputModel exchangeInputModel)
    {
        var exchange = await _exchangeService.GetByNameAsync(exchangeInputModel.Name);

        if (exchange != null)
            return BadRequest(new ErrorViewModel(400, "Exchange already exists", "Exchange already exists"));

        var newExchange = await _exchangeService.CreateAsync(exchangeInputModel);

        return Created($"coin?id={newExchange.Id}", newExchange);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Adm")]
    public async Task<IActionResult> Put(Guid id, ExchangeInputModel exchangeInputModel)
    {
        var exchange = await _exchangeService.GetByIdAsync(id);

        if (exchange != null)
            return NotFound(new ErrorViewModel(400, "Exchange already exists", "Exchange already exists"));

        await _exchangeService.UpdateAsync(exchange, exchangeInputModel);

        return NoContent();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Adm")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        var exchange = await _exchangeService.GetByIdAsync(id);

        if (exchange == null)
            return NotFound(new ErrorViewModel(404, "Exchange not found", "Exchange not found"));

        await _exchangeService.DeleteAsync(exchange);

        return Ok();
    }
}