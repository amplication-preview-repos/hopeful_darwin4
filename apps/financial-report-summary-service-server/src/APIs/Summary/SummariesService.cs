using FinancialReportSummaryService.Infrastructure;

namespace FinancialReportSummaryService.APIs;

public class SummariesService : SummariesServiceBase
{
    public SummariesService(FinancialReportSummaryServiceDbContext context)
        : base(context) { }
}
