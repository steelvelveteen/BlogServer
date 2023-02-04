using BlogAPI.DTOs.Auth;

namespace BlogAPI.Services;

public interface IAuthService
{
	Task<UserLoginResponseDTO> Login(UserLoginRequestDTO userLoginRequestDto);
	// Task<UserSignUpResponseDto> Signup(UserSignUpRequestDto userSignUpRequestDto);

	// Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto);
}
public class AuthService : IAuthService
{
	public Task<UserLoginResponseDTO> Login(UserLoginRequestDTO userLoginRequestDto)
	{
		throw new NotImplementedException();
	}

	// public Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordRequestDto resetPasswordRequestDto)
	// {
	// 	throw new NotImplementedException();
	// }

	// public Task<UserSignUpResponseDto> Signup(UserSignUpRequestDto userSignUpRequestDto)
	// {
	// 	throw new NotImplementedException();
	// }
}
