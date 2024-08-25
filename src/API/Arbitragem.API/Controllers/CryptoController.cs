using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arbitragem.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class CryptoController : Controller
{
    private readonly ICryptoService _cryptoService;

    public CryptoController(ICryptoService cryptoService)
    {
        _cryptoService = cryptoService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "AdmPolicy")]
    public async Task<IActionResult> GetAll()
    {
        var coins = await _cryptoService.GetAllAsync();

        if (!coins.Any())
            return NotFound(new ErrorViewModel(404, "Any coin was found", "Nenhuma criptomoeda encontrada"));

        return Ok(coins);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "AdmPolicy")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var coin = await _cryptoService.GetByIdAsync(id);

        if (coin == null)
            return NotFound(new ErrorViewModel(404, "Coin not found", "Criptomoeda não encontrada"));

        return Ok(coin);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = "AdmPolicy")]
    public async Task<IActionResult> Post(NewCoinInputModel newCoinInputModel)
    {
        var coin = await _cryptoService.GetBySymbolAsync(newCoinInputModel.Symbol);

        if (coin != null)
            return BadRequest(new ErrorViewModel(400, "Coin already exists", "Criptomoeda já cadastrada"));

        var newCoin = await _cryptoService.CreateAsync(newCoinInputModel);

        return Created($"coin?id={newCoin.Id}", newCoin);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "AdmPolicy")]
    public async Task<IActionResult> Put([FromQuery] Guid id, UpdateCoinInputModel updateCoinInputModel)
    {
        var coin = await _cryptoService.GetByIdAsync(id);

        if (coin == null)
            return NotFound(new ErrorViewModel(404, "Coin not found", "Nenhuma Criptomoeda encontrada"));

        await _cryptoService.Update(coin, updateCoinInputModel);

        return NoContent();
    }
    
    [HttpPut("status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "AdmPolicy")]
    public async Task<IActionResult> PutStatus([FromQuery] Guid id)
    {
        var coin = await _cryptoService.GetByIdAsync(id);

        if (coin == null)
            return NotFound(new ErrorViewModel(404, "Coin not found", "Nenhuma Criptomoeda encontrada"));

        await _cryptoService.UpdateStatus(coin);

        return NoContent();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "AdmPolicy")]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        var coin = await _cryptoService.GetByIdAsync(id);

        if (coin == null)
            return NotFound(new ErrorViewModel(404, "Coin not found", "Criptomoeda já cadastrada"));

        await _cryptoService.DeleteAsync(coin);

        return Ok();
    }
}