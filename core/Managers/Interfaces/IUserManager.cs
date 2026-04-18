using System;
using core.DTOs.UserDtos;

namespace core.Managers.Interfaces;

public interface IUserManager
{
    Task<ProfileDto?> SignUp(SignUpDto signUpDto);
    Task<bool> Login(LoginDto loginDto);
    Task<ProfileDto?> GetProfile(int userId);
    Task<ProfileDto?> EditProfile(ProfileDto userDetailsDto);
    Task<bool> DeleteAccount(int userId);
    Task<bool> EditPassword(int userId, EditPasswordDto newPassword);
}
