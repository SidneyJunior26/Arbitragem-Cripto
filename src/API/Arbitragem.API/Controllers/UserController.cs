using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arbitragem.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Adm")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();

        if (!users.Any())
            return NotFound(new ErrorViewModel(404, "Any user was found", "Any user was found"));

        return Ok(users);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
            return NotFound(new ErrorViewModel(404, "Any user was found", "Any user was found"));

        return Ok(user);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        var user = await _userService.GetByEmailAsync(email);

        if (user == null)
            return NotFound(new ErrorViewModel(404, "Any user was found", "Any user was found"));

        return Ok(user);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Policy = "Adm")]
    public async Task<IActionResult> Post(NewUserInputModel newUserInputModel)
    {
        var user = await _userService.GetByEmailAsync(newUserInputModel.Email);

        if (user != null)
            return BadRequest(new ErrorViewModel(400, "User already exists", "User already exists"));

        var newUSer = await _userService.CreateAsync(newUserInputModel);

        return Created($"user?id={newUSer.Id}", newUSer);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Adm")]
    public async Task<IActionResult> Put([FromQuery] Guid id, UpdateUserInputModel updateUserInputModel)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
            return NotFound(new ErrorViewModel(404, "User not found", "User not found"));

        await _userService.UpdateAsync(user, updateUserInputModel);

        return NoContent();
    }
    
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Policy = "Adm")]
    public async Task<IActionResult> Put([FromQuery] Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
            return NotFound(new ErrorViewModel(404, "User not found", "User not found"));

        await _userService.DeleteAsync(user);

        return NoContent();
    }
}
