using System;
using core.DTOs.SimpleFinDTOs;
using core.Gateways;
using core.Managers.Interfaces;
using core.Mappers.Interfaces;
using data.Repositories.Interfaces;

namespace core.Managers;

public class SimpleFinManager : ISimpleFinManager
{
    private readonly SimpleFinBridgeGateway _simpleFinGateway;
    private readonly IFinanceRepository _financeRepo;
    private readonly IDtoToModelMapper _mapper;
    public SimpleFinManager(SimpleFinBridgeGateway simpleFinBridgeGateway, IFinanceRepository financeRepository, IDtoToModelMapper mapper)
    {
        _simpleFinGateway = simpleFinBridgeGateway;
        _financeRepo = financeRepository;
        _mapper = mapper;
    }
    public async Task ConvertAccountSet(AccountSetDto? accountSet, string userId, CancellationToken ct)
    {
        if (accountSet != null)
        {
            var connections = _mapper.AccountSetToModels(accountSet, userId);
            await _financeRepo.UpsertConnections(connections);

        }
       
    }
}
