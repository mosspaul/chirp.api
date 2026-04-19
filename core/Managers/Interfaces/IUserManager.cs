using System;
using core.DTOs.UserDtos;

namespace core.Managers.Interfaces;

public interface IUserAccountManager
{
    Task<AuthTokenDto?> SignUp(SignUpDto signUpDto);
    Task<string?> Login(LoginDto loginDto);
    Task<ProfileDto?> GetProfile(string userId);
    Task<ProfileDto?> EditProfile(ProfileDto userDetailsDto);
    Task<bool> DeleteAccount(string userId);
    Task<bool> EditPassword(string userId, EditPasswordDto newPassword);
}
