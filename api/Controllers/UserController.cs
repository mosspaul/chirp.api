using core.DTOs.UserDtos;
using core.Managers.Interfaces;
using data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserAccountManager _userManager;
    private readonly IConfiguration _config;
    public UserController(IUserAccountManager userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    // POST SignUp -> receives a userdto and password and then maps to user 
    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
    {
        try
        {
            var profile = await _userManager.SignUp(signUpDto);
            return profile != null ? Ok(profile) : Unauthorized("Username or Email is already registered. Please try with different credentials.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // POST Login -> receives a username and password and checks that it exists, if so logs in to app
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        try
    {
        var token = await _userManager.Login(loginDto);
        return token != null ? Ok(new { token }) : Unauthorized("Login failed. Username or password is incorrect.");
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
    }
    // GET GetProfile -> recieves an id and with that gets the user's profile (returns profiledto)
    [Authorize]
    [HttpGet("profile/{id}")]
    public async Task<IActionResult> GetProfile(string id)
    {
        try
        {
            var profile = await _userManager.GetProfile(id);
            return profile != null ? Ok(profile) : NotFound();
        } 
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    } 
    // PUT/PATCH EditAccount-> receives an new userdto and maps that to the new profile (id is not changed)
    [HttpPut("profile/{id}")]
    public async Task<IActionResult> EditProfile([FromBody] ProfileDto profileDto)
    {
        try
        {
            var profile = await _userManager.EditProfile(profileDto);
            return profile != null ? Ok(profile) : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // DELETE DeleteAccount -> recieves a userid with which to delete the account
    [HttpDelete("profile/{id}")]
    public async Task<IActionResult> DeleteProfile(string id)
    {
        try
        {
            var accountDeleted = await _userManager.DeleteAccount(id);
            return accountDeleted ? NoContent() : BadRequest("There was a problem deleting the account.");
        } 
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // PUT/PATCH EditPassword -> receives a userid and new password to update
    [HttpPatch("profile/{id}")]
    public async Task<IActionResult> EditPassword(string id, [FromBody] EditPasswordDto editPassword)
    {
        try
        {
            var passwordChanged = await _userManager.EditPassword(id, editPassword);
            return passwordChanged ? NoContent() : BadRequest("Password could not be changed. Try again.");
            
        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
