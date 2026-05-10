using System;
using data.Models;

namespace data.Repositories.Interfaces;

public interface IFinanceRepository
{
    Task UpsertConnections(List<Connection> connections);
}
