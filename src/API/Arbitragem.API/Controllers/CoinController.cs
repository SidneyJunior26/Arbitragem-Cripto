using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arbitragem.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class CoinController : Controller
{
    private readonly ICoinService _coinService;

    public CoinController(ICoinService coinService)
    {
        _coinService = coinService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var coins = await _coinService.GetAllAsync();

        if (!coins.Any())
            return NotFound(new ErrorViewModel(404, "Any coin was found", "Any coin was found"));

        return Ok(coins);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        var coin = await _coinService.GetByIdAsync(id);

        if (coin == null)
            return NotFound(new ErrorViewModel(404, "Coin not found", "Coin not found"));

        return Ok(coin);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize]
    public async Task<IActionResult> Post(NewCoinInputModel newCoinInputModel)
    {
        var coin = await _coinService.GetBySymbolAsync(newCoinInputModel.Symbol);

        if (coin != null)
            return BadRequest(new ErrorViewModel(400, "Coin already exists", "Coin already exists"));

        var newCoin = await _coinService.CreateAsync(newCoinInputModel);

        return Created($"coin/{newCoin.Id}", newCoin);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Put([FromQuery] Guid id, UpdateCoinInputModel updateCoinInputModel)
    {
        var coin = await _coinService.GetByIdAsync(id);

        if (coin == null)
            return NotFound(new ErrorViewModel(404, "Coin not found", "Coin not found"));

        await _coinService.UpdateCoin(coin, updateCoinInputModel);

        return NoContent();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        var coin = await _coinService.GetByIdAsync(id);

        if (coin == null)
            return NotFound(new ErrorViewModel(404, "Coin not found", "Coin not found"));

        await _coinService.DeleteAsync(coin);

        return Ok();
    }
}