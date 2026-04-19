using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using core.DTOs.UserDtos;
using core.Gateways;
using core.Managers.Interfaces;
using core.Mappers.Interfaces;
using data.Models;
using data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace core.Managers;

public class UserAccountManager : IUserAccountManager
{
    private readonly IUserRepository _repo;
    private readonly IDtoToModelMapper _dtoToModelMapper;
    private readonly SimpleFinBridgeGateway _simpleFinGateway;
    private readonly UserManager<User> _identityUserManager;
    private readonly IConfiguration _config;
    public UserAccountManager(IUserRepository repo, IDtoToModelMapper dtoToModelMapper, SimpleFinBridgeGateway simpleFinGateway, UserManager<User> identityUserManager, IConfiguration config)
    {
        _repo = repo;
        _dtoToModelMapper = dtoToModelMapper;
        _simpleFinGateway = simpleFinGateway;
        _identityUserManager = identityUserManager;
        _config = config;
    }
    public async Task<bool> DeleteAccount(string userId)
    {
        return await _repo.DeleteAccount(userId);
    }

    public async Task<bool> EditPassword(string userId, EditPasswordDto newPassword)
    {
        return await _repo.EditPassword(userId, newPassword.NewPassword);
    }

    public async Task<ProfileDto?> EditProfile(ProfileDto profile)
    {
        var user = _dtoToModelMapper.ProfileToUser(profile);
        var updatedUser = await _repo.EditProfile(user);
        return updatedUser != null ? new ProfileDto(updatedUser) : null;
    }

    public async Task<ProfileDto?> GetProfile(string userId)
    {
        var user = await _repo.GetProfile(userId);
        return user != null ? new ProfileDto(user) : null;
    }

    public async Task<string?> Login(LoginDto login)
    {
        var user = await _identityUserManager.FindByNameAsync(login.Username);
        if (user == null || !await _identityUserManager.CheckPasswordAsync(user, login.Password))
            return null;

        return GenerateToken(user);
    }

    public async Task<AuthTokenDto?> SignUp(SignUpDto signUp)
    {
        var user = _dtoToModelMapper.SignUpToUser(signUp);
        user.SimpleFinAccessUrl = await _simpleFinGateway.GetAccessUrl(signUp.SimpleFinToken);
        var result = await _identityUserManager.CreateAsync(user, signUp.Password);
        if (!result.Succeeded) return null;
        var token = GenerateToken(user);

        ProfileDto? profile = user != null ? new ProfileDto(user) : null;
        return new AuthTokenDto(profile, token);
    }

    private string GenerateToken(User user)
    {
        var secret = Environment.GetEnvironmentVariable("JWT_SECRET") 
        ?? _config["Jwt:Secret"];

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email!)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}