using System.Text.Json.Serialization;

namespace core.DTOs.UserDtos;

public class SignUpDto
{
    [JsonPropertyName("username")]
    public required string Username { get; set; }
    [JsonPropertyName("email")]
    public required string Email { get; set; }
    [JsonPropertyName("first_name")]
    public required string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public required string LastName { get; set; }
    [JsonPropertyName("password")]
    public required string Password { get; set; }
}