
using data.Models;
using Newtonsoft.Json;

namespace core.DTOs.UserDtos;

public class ProfileDto
{
    public ProfileDto(User user)
    {
        Email = user.Email;
        Username = user.UserName;
        FirstName = user.FirstName;
        LastName = user.LastName;
        UserId = user.Id;
    }
    [JsonProperty("username")]
    public string? Username { get; set; }
    [JsonProperty("email")]
    public string? Email { get; set; }
    [JsonProperty("firstName")]
    public string? FirstName { get; set; }
    [JsonProperty("lastName")]
    public string? LastName { get; set; }
    [JsonProperty("userId")]
    public string UserId { get; set; }
}