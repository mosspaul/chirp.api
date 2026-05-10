using System.Security.Claims;
using core.Managers.Interfaces;
using Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinanceController : ControllerBase
{
    private readonly IFinanceManager _financeManager;
    public FinanceController(IFinanceManager financeManager)
    {
        _financeManager = financeManager;
    }

    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetAccounts()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User not found");
            var connections = await _financeManager.GetConnections(userId);
            return connections != null ? Ok(connections) : NoContent();
        } catch (Exception ex)
        {
            return BadRequest(ex);
        }
        
    }
}
