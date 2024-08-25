using Arbitragem.Application.InputModels;
using Arbitragem.Application.Services.Interfaces;
using Arbitragem.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arbitragem.API.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class SecurityController : Controller
{
    private readonly ISecurityService _securityService;
    private readonly IUserService _userService;

    public SecurityController(ISecurityService securityService, IUserService userService)
    {
        _securityService = securityService;
        _userService = userService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginInputModel loginInputModel)
    {
        var user = await _userService.GetByEmailAsync(loginInputModel.Email);

        if (user == null)
            return NotFound(new ErrorViewModel(404, "Any user was found", "Usuário não encontrado"));

        var passwordValidated = _securityService.ValidatePassword(user, loginInputModel.Password);

        if (!passwordValidated)
            return Unauthorized(new ErrorViewModel(401, "Invalid credentials", "Senha inválida"));
        
        var token = _securityService.Login(user, loginInputModel);
        
        return Ok(new {
            token = token
        });
    }
}