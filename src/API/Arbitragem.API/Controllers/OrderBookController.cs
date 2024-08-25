using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solution.Core.Enum;

namespace Arbitragem.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class OrderBookController : Controller
{
    private readonly IOrderBookService _orderBookService;

    public OrderBookController(IOrderBookService orderBookService)
    {
        _orderBookService = orderBookService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Get([FromQuery] Guid cryptoId, [FromQuery] Guid exchangeId, [FromQuery] Side side)
    {
        var orderBooks = await _orderBookService.GetByCryptoAndExchange(cryptoId, exchangeId, side);

        if (!orderBooks.Any())
            return NotFound(new ErrorViewModel(404, "Any order book was found", "Nenhum Order Book encontrado"));

        return Ok(orderBooks);
    }
}