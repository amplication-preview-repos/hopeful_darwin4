using Microsoft.AspNetCore.Mvc;

namespace FinancialReportSummaryService.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
