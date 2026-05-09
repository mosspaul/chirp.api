using core.Jobs.Interfaces;

public class SimpleFinSyncService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<SimpleFinSyncService> _logger;
    
    // 6 times a day = every 4 hours
    private readonly TimeSpan _interval = TimeSpan.FromHours(4);

    public SimpleFinSyncService(IServiceScopeFactory scopeFactory, ILogger<SimpleFinSyncService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Run once immediately on startup, then on interval
        await RunSync(stoppingToken);

        using var timer = new PeriodicTimer(_interval);

        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await RunSync(stoppingToken);
        }
    }

    private async Task RunSync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("SimpleFin sync starting at {Time}", DateTimeOffset.UtcNow);

        try
        {
            // BackgroundService is a singleton, so scope-in to get scoped services (DbContext, repos)
            using var scope = _scopeFactory.CreateScope();
            var syncJob = scope.ServiceProvider.GetRequiredService<ISimpleFinSyncJob>();

            await syncJob.RunAsync(stoppingToken);

            _logger.LogInformation("SimpleFin sync completed at {Time}", DateTimeOffset.UtcNow);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(ex, "SimpleFin sync failed");
            // Don't rethrow — keeps the service alive for the next tick
        }
    }
}