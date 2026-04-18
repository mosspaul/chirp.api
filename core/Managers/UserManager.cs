using core.DTOs.UserDtos;
using core.Managers.Interfaces;
using core.Mappers.Interfaces;
using data.Repositories.Interfaces;

namespace core.Managers;

public class UserManager : IUserManager
{
    private readonly IUserRepository _repo;
    private readonly IDtoToModelMapper _dtoToModelMapper;
    public UserManager(
        IUserRepository repo, 
        IDtoToModelMapper dtoToModelMapper
        )
    {
        _repo = repo;
        _dtoToModelMapper = dtoToModelMapper;
    }
    public async Task<bool> DeleteAccount(int userId)
    {
        return await _repo.DeleteAccount(userId);
    }

    public async Task<bool> EditPassword(int userId, EditPasswordDto newPassword)
    {
        return await _repo.EditPassword(userId, newPassword.NewPassword);
    }

    public async Task<ProfileDto?> EditProfile(ProfileDto profile)
    {
        var user = _dtoToModelMapper.ProfileToUser(profile);
        var updatedUser = await _repo.EditProfile(user);
        return updatedUser != null ? new ProfileDto(updatedUser) : null;
    }

    public async Task<ProfileDto?> GetProfile(int userId)
    {
        var user = await _repo.GetProfile(userId);
        return user != null ? new ProfileDto(user) : null;
    }

    public async Task<bool> Login(LoginDto login)
    {
        bool isLoggedIn = await _repo.Login(login.Username, login.Password);
        return isLoggedIn;
    }

    public async Task<ProfileDto?> SignUp(SignUpDto signUp)
    {
        var user = _dtoToModelMapper.SignUpToUser(signUp);
        var createdUser = await _repo.SignUp(user);
        return createdUser != null ? new ProfileDto(createdUser) : null;
    }
}