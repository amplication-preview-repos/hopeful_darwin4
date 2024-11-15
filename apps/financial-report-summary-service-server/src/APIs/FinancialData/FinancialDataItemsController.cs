using Microsoft.AspNetCore.Mvc;

namespace FinancialReportSummaryService.APIs;

[ApiController()]
public class FinancialDataItemsController : FinancialDataItemsControllerBase
{
    public FinancialDataItemsController(IFinancialDataItemsService service)
        : base(service) { }
}
