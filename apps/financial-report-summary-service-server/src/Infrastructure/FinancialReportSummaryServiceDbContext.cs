using FinancialReportSummaryService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialReportSummaryService.Infrastructure;

public class FinancialReportSummaryServiceDbContext : DbContext
{
    public FinancialReportSummaryServiceDbContext(
        DbContextOptions<FinancialReportSummaryServiceDbContext> options
    )
        : base(options) { }

    public DbSet<SummaryDbModel> Summaries { get; set; }

    public DbSet<ReportDbModel> Reports { get; set; }

    public DbSet<FinancialDataDbModel> FinancialDataItems { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
