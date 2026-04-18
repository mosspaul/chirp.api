using System;
using core.DTOs.UserDtos;
using data.Models;

namespace core.Mappers.Interfaces;

public interface IDtoToModelMapper
{
    User ProfileToUser(ProfileDto profile);
    User SignUpToUser(SignUpDto signUpDto);
}
