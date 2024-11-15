using FinancialReportSummaryService.Infrastructure;

namespace FinancialReportSummaryService.APIs;

public class FinancialDataItemsService : FinancialDataItemsServiceBase
{
    public FinancialDataItemsService(FinancialReportSummaryServiceDbContext context)
        : base(context) { }
}
