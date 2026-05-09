using System;
using core.DTOs.SimpleFinDTOs;
using core.Gateways;
using core.Managers.Interfaces;
using data.Repositories.Interfaces;

namespace core.Managers;

public class SimpleFinManager : ISimpleFinManager
{
    private readonly SimpleFinBridgeGateway _simpleFinGateway;
    private readonly IUserRepository _userRepo;
    public SimpleFinManager(SimpleFinBridgeGateway simpleFinBridgeGateway, IUserRepository userRepo)
    {
        _simpleFinGateway = simpleFinBridgeGateway;
        _userRepo = userRepo;
    }
    public async Task ConvertAccountSet(AccountSetDto? accountSet, string userId, CancellationToken ct)
    {
        
    }
}
