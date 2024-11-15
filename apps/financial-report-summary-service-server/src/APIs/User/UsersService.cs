using FinancialReportSummaryService.Infrastructure;

namespace FinancialReportSummaryService.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(FinancialReportSummaryServiceDbContext context)
        : base(context) { }
}
