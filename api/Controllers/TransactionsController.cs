using System.Security.Claims;
using core.Managers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionManager _transactionManager;
    public TransactionController(ITransactionManager transactionManager)
    {
        _transactionManager = transactionManager;
    }

    [Authorize]
    [HttpGet("all")]
    public async Task<IActionResult> GetTransactions()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User not found");
        var transactions = await _transactionManager.GetTransactions(userId);
        return Ok(transactions);
    }
}