using BlogAPI.DTOs.Auth;
using BlogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;
	private readonly IConfiguration _configuration;
	private readonly ILogger<AuthController> _logger;

	public AuthController(IAuthService authService, IConfiguration configuration, ILogger<AuthController> logger)
	{
		_authService = authService;
		_configuration = configuration;
		_logger = logger;
	}
	// https://localhost:5000/auth/login
	[HttpPost]
	[Route("/[controller]/Login")]
	public async Task<UserLoginResponseDTO> Login(UserLoginRequestDTO userLoginRequestDto)
	{
		_logger.LogInformation("Login request performed");
		var authUser = await _authService.Login(userLoginRequestDto);
		return authUser;
	}

	// https://localhost:5000/auth/signup
	// [HttpPost]
	// [Route("/[controller]/Signup")]
	// public async Task<UserSignUpResponseDto> Signup(UserSignUpRequestDto userSignUpRequestDto)
	// {
	// 	userSignUpRequestDto.Email = userSignUpRequestDto.Email.ToLower();

	// 	var createdUser = await _authService.Signup(userSignUpRequestDto);
	// 	return createdUser;
	// }

	// https://localhost:5000/auth/resetpassword
	// [HttpPost]
	// [Route("/[controller]/ResetPassword")]
	// public async Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto)
	// {
	// 	var updatedUser = await _authService.ResetPassword(resetPasswordRequestDto);
	// 	return updatedUser;
	// }
}
