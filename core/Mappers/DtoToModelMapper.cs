using core.DTOs.UserDtos;
using core.Mappers.Interfaces;
using data.Models;

namespace core.Mappers;

public class DtoToModelMapper : IDtoToModelMapper
{
    public User ProfileToUser(ProfileDto profile)
    {
        return new User()
        {
            Username = profile.Username,
            PasswordHash = null,
            Email = profile.Email,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            DateCreated = null,
            Id = profile.UserId
        };
    }

    public User SignUpToUser(SignUpDto signUp)
    {
        return new User()
        {
            Username = signUp.Username,
            PasswordHash = signUp.Password,
            Email = signUp.Email,
            FirstName = signUp.FirstName,
            LastName = signUp.LastName,
            DateCreated = DateTime.UtcNow,
            Id = 0
        };
    }
}