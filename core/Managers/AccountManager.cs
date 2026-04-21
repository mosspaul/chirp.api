using System;
using core.DTOs.SimpleFinDTOs;
using core.Gateways;
using core.Managers.Interfaces;
using data.Repositories.Interfaces;

namespace core.Managers;

public class AccountManager : IAccountManager
{
    private readonly SimpleFinBridgeGateway _simpleFinGateway;
    private readonly IUserRepository _userRepo;
    public AccountManager(SimpleFinBridgeGateway simpleFinBridgeGateway, IUserRepository userRepo)
    {
        _simpleFinGateway = simpleFinBridgeGateway;
        _userRepo = userRepo;
    }
    public async Task<AccountSet> GetAccountSet(string userId)
    {
        var user = await _userRepo.GetUserById(userId) ?? throw new Exception("user cannot be null");
        var accountSet = await _simpleFinGateway.GetAccountSet(user.SimpleFinAccessUrl);
        return accountSet ?? throw new Exception();
    }
}
