using Microsoft.AspNetCore.Mvc;

namespace FinancialReportSummaryService.APIs;

[ApiController()]
public class ReportsController : ReportsControllerBase
{
    public ReportsController(IReportsService service)
        : base(service) { }
}
