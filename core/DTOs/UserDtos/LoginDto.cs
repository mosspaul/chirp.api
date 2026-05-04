using Newtonsoft.Json;

namespace core.DTOs.UserDtos;

public class LoginDto
{
    [JsonProperty("username")]
    public required string Username {get;set;}
    [JsonProperty("password")]
    public required string Password {get;set;}
}
