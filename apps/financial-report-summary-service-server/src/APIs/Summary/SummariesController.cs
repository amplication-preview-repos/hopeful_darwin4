using Microsoft.AspNetCore.Mvc;

namespace FinancialReportSummaryService.APIs;

[ApiController()]
public class SummariesController : SummariesControllerBase
{
    public SummariesController(ISummariesService service)
        : base(service) { }
}
