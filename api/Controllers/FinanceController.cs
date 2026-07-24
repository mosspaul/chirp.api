using System.Security.Claims;
using core.DTOs.FinanceDtos;
using core.Managers.Interfaces;
using data.Models;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetConnections()
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

    [Authorize]
    [HttpGet("accounts")]
    public async Task<IActionResult> GetAccounts()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User not found");
            var connections = await _financeManager.GetConnections(userId);
            var accounts = new List<AccountDto>();
            foreach (var conn in connections)
            {
                accounts.AddRange(conn.Accounts);
            }
            return accounts != null ? Ok(accounts) : NoContent();
        } catch (Exception ex)
        {
            return BadRequest(ex);
        }
        
    }
}
