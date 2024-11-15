using FinancialReportSummaryService.APIs;

namespace FinancialReportSummaryService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IFinancialDataService, FinancialDataService>();
        services.AddScoped<IReportsService, ReportsService>();
        services.AddScoped<ISummariesService, SummariesService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
