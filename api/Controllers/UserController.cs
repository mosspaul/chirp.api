using System.Security.Claims;
using core.DTOs.UserDtos;
using core.Managers.Interfaces;
using data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api;

/// <summary>
/// Manages user authentication and account management.
/// Provides endpoints for signup, login, profile management, and password reset.
/// </summary>
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

    /// <summary>
    /// Validates the current authentication token.
    /// Used to verify that a JWT token is still valid and has not expired.
    /// </summary>
    /// <returns>OK if the token is valid</returns>
    /// <response code="200">Token is valid and authenticated</response>
    /// <response code="401">Token is invalid or expired</response>
    [HttpGet("validate")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult ValidateToken()
    {
        return Ok();
    }

    /// <summary>
    /// Creates a new user account.
    /// </summary>
    /// <param name="signUpDto">User registration information including email, username, and password</param>
    /// <returns>User profile and authentication token if successful</returns>
    /// <response code="200">Successfully created account and logged in</response>
    /// <response code="400">Invalid input or server error</response>
    /// <response code="401">Username or email already registered</response>
    [HttpPost("signup")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    /// <param name="loginDto">Login credentials (username/email and password)</param>
    /// <returns>User profile and JWT authentication token</returns>
    /// <response code="200">Successfully authenticated</response>
    /// <response code="400">Invalid input or server error</response>
    /// <response code="401">Invalid username or password</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    /// <summary>
    /// Retrieves the authenticated user's profile information.
    /// </summary>
    /// <returns>User profile details</returns>
    /// <response code="200">Successfully retrieved profile</response>
    /// <response code="400">Server error</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="404">User profile not found</response>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Updates the authenticated user's profile information.
    /// </summary>
    /// <param name="profileDto">Updated profile information (firstName, lastName, email, etc.)</param>
    /// <returns>Updated user profile</returns>
    /// <response code="200">Successfully updated profile</response>
    /// <response code="400">Invalid input or server error</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="404">User profile not found</response>
    [Authorize]
    [HttpPut("profile/")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Permanently deletes the authenticated user's account.
    /// This action is irreversible and will delete all associated data.
    /// </summary>
    /// <returns>No content on successful deletion</returns>
    /// <response code="204">Account successfully deleted</response>
    /// <response code="400">Account deletion failed</response>
    /// <response code="401">User is not authenticated</response>
    [Authorize]
    [HttpDelete("profile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    /// <summary>
    /// Changes the authenticated user's password.
    /// </summary>
    /// <param name="editPassword">Current password and new password confirmation</param>
    /// <returns>No content on successful change</returns>
    /// <response code="204">Password successfully changed</response>
    /// <response code="400">Password change failed</response>
    /// <response code="401">User is not authenticated</response>
    [Authorize]
    [HttpPatch("profile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> EditPassword([FromBody] EditPasswordDto editPassword)
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();
            var passwordChanged = await _userManager.EditPassword(userId, editPassword);
            return passwordChanged ? NoContent() : BadRequest("Password could not be changed. Try again.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Generates a password reset token for a user who forgot their password.
    /// </summary>
    /// <param name="forgotPasswordDto">Email address of the account to reset</param>
    /// <returns>Password reset token (currently returned directly - integrate with email service for production)</returns>
    /// <response code="200">Reset token generated successfully</response>
    /// <response code="400">Invalid input or server error</response>
    /// <response code="404">Email address not found</response>
    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Resets a user's password using a valid reset token.
    /// </summary>
    /// <param name="resetPasswordDto">Email, reset token, and new password</param>
    /// <returns>No content on successful reset</returns>
    /// <response code="204">Password successfully reset</response>
    /// <response code="400">Password reset failed or invalid token</response>
    [HttpPost("reset-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
