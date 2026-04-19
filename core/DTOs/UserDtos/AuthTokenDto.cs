using System;

namespace core.DTOs.UserDtos;

public class AuthTokenDto
{
    public AuthTokenDto(ProfileDto? profile, string? token)
    {
        ProfileDto = profile;
        Token = token;
    }
    public ProfileDto? ProfileDto { get; set; }
    public string? Token { get; set; }
}
