using System.Security.Claims;
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
    // GET validate -> ensures a token has been validated
    [HttpGet("validate")]
    [Authorize]
    public IActionResult ValidateToken()
    {
        return Ok();
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
        var profile = await _userManager.Login(loginDto);
        return profile != null ? Ok(profile) : Unauthorized("Login failed. Username or password is incorrect.");
    }
    catch (Exception ex)
    {
        return BadRequest(ex.Message);
    }
    }
    // GET GetProfile -> recieves an id and with that gets the user's profile (returns profiledto)
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetProfile()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var profile = await _userManager.GetProfile(userId);
            return profile != null ? Ok(profile) : NotFound();
        } 
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    } 
    // PUT/PATCH EditAccount-> receives an new userdto and maps that to the new profile (id is not changed)
    [Authorize]
    [HttpPut("profile/")]
    public async Task<IActionResult> EditProfile([FromBody] ProfileDto profileDto)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var profile = await _userManager.EditProfile(userId, profileDto);
            return profile != null ? Ok(profile) : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // DELETE DeleteAccount -> recieves a userid with which to delete the account
    [Authorize]
    [HttpDelete("profile")]
    public async Task<IActionResult> DeleteProfile()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var accountDeleted = await _userManager.DeleteAccount(userId);
            return accountDeleted ? NoContent() : BadRequest("There was a problem deleting the account.");
        } 
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // PUT/PATCH EditPassword -> receives a userid and new password to update
    [Authorize]
    [HttpPatch("profile")]
    public async Task<IActionResult> EditPassword([FromBody] EditPasswordDto editPassword)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var passwordChanged = await _userManager.EditPassword(userId, editPassword);
            return passwordChanged ? NoContent() : BadRequest("Password could not be changed. Try again.");

        } catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // POST ForgotPassword -> generates a password reset token for the given email
    // TODO: token is returned directly until an email service exists to deliver it out-of-band
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
    {
        try
        {
            var token = await _userManager.GeneratePasswordResetToken(forgotPasswordDto.Email);
            return token != null ? Ok(new { token }) : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    // POST ResetPassword -> receives an email, reset token, and new password to reset the password
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
    {
        try
        {
            var passwordReset = await _userManager.ResetPassword(resetPasswordDto);
            return passwordReset ? NoContent() : BadRequest("Password could not be reset. Try again.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
