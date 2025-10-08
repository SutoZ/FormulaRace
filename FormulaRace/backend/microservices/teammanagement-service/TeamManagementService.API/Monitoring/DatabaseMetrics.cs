using System.Diagnostics.Metrics;

namespace TeamManagementService.API.Monitoring;

public static class DatabaseMetrics
{
    private static readonly Meter Meter = new("TeamManagementService.Database", "1.0.0");

    private static readonly Counter<long> MigrationsAppliedCounter =
        Meter.CreateCounter<long>("db.migrations.applied.count", description: "The number of database migrations applied.");

    private static readonly Histogram<double> MigrationDuration =
        Meter.CreateHistogram<double>("db.migrations.duration.ms", "ms", "The duration of database migration operations.");

    public static void RecordMigration(long durationMs, int migrationCount)
    {
        MigrationDuration.Record(durationMs);
        MigrationsAppliedCounter.Add(migrationCount);
    }
}
