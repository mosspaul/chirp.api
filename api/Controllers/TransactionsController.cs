using System.Security.Claims;
using core.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

/// <summary>
/// Manages financial transactions across connected accounts.
/// Requires JWT authentication for all endpoints.
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly ITransactionManager _transactionManager;

    public TransactionController(ITransactionManager transactionManager)
    {
        _transactionManager = transactionManager;
    }

    /// <summary>
    /// Retrieves all transactions for the authenticated user.
    /// </summary>
    /// <returns>List of all user transactions sorted by date</returns>
    /// <response code="200">Successfully retrieved transactions</response>
    /// <response code="401">User is not authenticated</response>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTransactions()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User not found");
        var transactions = await _transactionManager.GetTransactions(userId);
        return Ok(transactions);
    }
}