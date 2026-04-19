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
            UserName = profile.Username,
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
            UserName = signUp.Username,
            PasswordHash = signUp.Password,
            Email = signUp.Email,
            FirstName = signUp.FirstName,
            LastName = signUp.LastName,
            DateCreated = DateTime.UtcNow,
            SimpleFinAccessUrl = null,
        };
    }
}