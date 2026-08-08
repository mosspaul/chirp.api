using System.Security.Claims;
using core.DTOs.FinanceDtos;
using core.Managers.Interfaces;
using data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

/// <summary>
/// Manages financial connections and accounts.
/// Provides endpoints for retrieving connected financial institutions and associated accounts.
/// Requires JWT authentication for all endpoints.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FinanceController : ControllerBase
{
    private readonly IFinanceManager _financeManager;

    public FinanceController(IFinanceManager financeManager)
    {
        _financeManager = financeManager;
    }

    /// <summary>
    /// Retrieves all financial connections for the authenticated user.
    /// </summary>
    /// <returns>List of financial connections with their associated accounts and holdings</returns>
    /// <response code="200">Successfully retrieved connections</response>
    /// <response code="204">No connections found for the user</response>
    /// <response code="400">Error retrieving connections</response>
    /// <response code="401">User is not authenticated</response>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetConnections()
    {
        try
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User not found");
            var connections = await _financeManager.GetConnections(userId);
            return connections != null ? Ok(connections) : NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    /// <summary>
    /// Retrieves all accounts across all financial connections.
    /// Aggregates accounts from all connected financial institutions.
    /// </summary>
    /// <returns>List of all accounts from connected financial institutions</returns>
    /// <response code="200">Successfully retrieved accounts</response>
    /// <response code="204">No accounts found</response>
    /// <response code="400">Error retrieving accounts</response>
    /// <response code="401">User is not authenticated</response>
    [HttpGet("accounts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
