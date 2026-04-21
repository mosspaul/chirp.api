using System.Security.Claims;
using core.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountManager _accountManager;
    public AccountController(IAccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    [Authorize]
    [HttpGet("[controller]")]
    public async Task<IActionResult> GetAccounts()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var accountSet = await _accountManager.GetAccountSet(userId);
            return accountSet != null ? Ok(accountSet) : NoContent();
        } catch (Exception ex)
        {
            return BadRequest(ex);
        }
        
    }
}
