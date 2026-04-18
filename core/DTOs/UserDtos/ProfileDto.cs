
using System.Text.Json.Serialization;
using data.Models;

namespace core.DTOs.UserDtos;

public class ProfileDto
{
    public ProfileDto(User user)
    {
        Email = user.Email;
        Username = user.Username;
        FirstName = user.FirstName;
        LastName = user.LastName;
        UserId = user.Id;
    }
    [JsonPropertyName("username")]
    public string? Username { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }
    [JsonPropertyName("user_id")]
    public int UserId { get; set; }
}