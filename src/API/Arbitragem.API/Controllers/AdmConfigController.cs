using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arbitragem.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class AdmConfigController : Controller
{
    private readonly IAdmConfigurationService _configurationService;

    public AdmConfigController(IAdmConfigurationService configurationService)
    {
        _configurationService = configurationService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Adm")]
    public async Task<IActionResult> Get()
    {
        var admConfig = await _configurationService.GetAsync();

        if (admConfig == null)
            return NotFound(new ErrorViewModel(404, "Any configuration was found", 
                "Any configuration was found"));

        return Ok(admConfig);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Adm")]
    public async Task<IActionResult> Put([FromQuery] Guid id, ConfigurationInputModel configurationInputModel)
    {
        var admConfig = await _configurationService.GetByIdAsync(id);

        if (admConfig == null)
            return NotFound(new ErrorViewModel(404, "Any configuration was found", 
                "Any configuration was found"));

        return Ok(admConfig);
    }
}