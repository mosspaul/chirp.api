using System;
using core.Gateways;
using core.Jobs.Interfaces;
using core.Managers.Interfaces;
using data.Models;
using data.Repositories.Interfaces;

namespace core.Jobs;

public class SimpleFinSyncJob : ISimpleFinSyncJob
{
    private readonly SimpleFinBridgeGateway _simpleFinGateway;
    private readonly ISimpleFinManager _simpleFinManager;
    private readonly IUserRepository _userRepo;
    public SimpleFinSyncJob(SimpleFinBridgeGateway simpleFinGateway, ISimpleFinManager simpleFinManager, IUserRepository userRepo)
    {
        _simpleFinGateway = simpleFinGateway;
        _simpleFinManager = simpleFinManager;
        _userRepo = userRepo;
    }
    public async Task RunAsync(CancellationToken ct)
    {
        var users = await _userRepo.GetUsersWithSimpleFinAccessUrl(ct);
        foreach (var user in users)
        {
            var data = await _simpleFinGateway.GetAccountSet(user.SimpleFinAccessUrl, ct);
            await _simpleFinManager.ConvertAccountSet(data, user.Id, ct);
        }
        
    }
}
