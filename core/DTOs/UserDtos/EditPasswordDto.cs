using System;

namespace core.DTOs.UserDtos;

public class EditPasswordDto
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}
