using FinancialReportSummaryService.APIs.Common;
using FinancialReportSummaryService.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinancialReportSummaryService.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class FinancialDataFindManyArgs : FindManyInput<FinancialData, FinancialDataWhereInput> { }
