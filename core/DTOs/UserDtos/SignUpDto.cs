using Newtonsoft.Json;

namespace core.DTOs.UserDtos;

public class SignUpDto
{
    [JsonProperty("username")]
    public required string Username { get; set; }
    [JsonProperty("email")]
    public required string Email { get; set; }
    [JsonProperty("first_name")]
    public required string FirstName { get; set; }
    [JsonProperty("last_name")]
    public required string LastName { get; set; }
    [JsonProperty("simple_fin_token")]
    public required string SimpleFinToken { get; set; }
    [JsonProperty("password")]
    public required string Password { get; set; }
}