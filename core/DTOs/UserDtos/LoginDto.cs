using System;
using System.Text.Json.Serialization;

namespace core.DTOs.UserDtos;

public class LoginDto
{
    [JsonPropertyName("username")]
    public required string Username {get;set;}
    [JsonPropertyName("username")]
    public required string Password {get;set;}
}
