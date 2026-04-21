using System;
using core.DTOs.SimpleFinDTOs;

namespace core.Managers.Interfaces;

public interface IAccountManager
{
    Task<AccountSet> GetAccountSet(string userId);
}
