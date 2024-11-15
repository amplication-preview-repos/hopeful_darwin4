using FinancialReportSummaryService.Infrastructure;

namespace FinancialReportSummaryService.APIs;

public class ReportsService : ReportsServiceBase
{
    public ReportsService(FinancialReportSummaryServiceDbContext context)
        : base(context) { }
}
