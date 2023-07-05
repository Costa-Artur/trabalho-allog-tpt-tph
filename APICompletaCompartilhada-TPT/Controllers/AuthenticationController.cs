using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Models;
using Univali.Api.Repositories;
using Univali.Api.Services;

namespace Univali.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly TokenService _tokenService;

    public AuthenticationController(
        IConfiguration configuration,
        IUserRepository userRepository,
        TokenService tokenService
    ) {
        _configuration = configuration;

        _userRepository = userRepository;
        
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> Authenticate(UserDTO user) 
    {
        if (user == null) { return BadRequest(); };

        var userEntity = _userRepository.Get(
            user.Username, user.Password
        );

        if (userEntity == null) { return Unauthorized(); }

        return Ok(_tokenService.GenerateToken(userEntity));
    }

}
