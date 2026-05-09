using System;
using core.DTOs.SimpleFinDTOs;

namespace core.Managers.Interfaces;

public interface ISimpleFinManager
{
    Task ConvertAccountSet(AccountSetDto? data, string userId, CancellationToken ct);
}
