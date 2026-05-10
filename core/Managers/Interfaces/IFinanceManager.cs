using data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Managers.Interfaces;

public interface IFinanceManager
{
    Task<List<Connection>> GetConnections(string userId);
}