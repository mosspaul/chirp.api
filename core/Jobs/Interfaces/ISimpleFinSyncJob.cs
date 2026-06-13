using System;

namespace core.Jobs.Interfaces;

public interface ISimpleFinSyncJob
{
    Task RunAsync(CancellationToken ct);
}
