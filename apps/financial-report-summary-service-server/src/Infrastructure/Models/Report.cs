using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialReportSummaryService.Infrastructure.Models;

[Table("Reports")]
public class ReportDbModel
{
    [StringLength(1000)]
    public string? Content { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    public List<FinancialDataDbModel>? FinancialDataItems { get; set; } =
        new List<FinancialDataDbModel>();

    [Key()]
    [Required()]
    public string Id { get; set; }

    public DateTime? PublishedDate { get; set; }

    public List<SummaryDbModel>? Summaries { get; set; } = new List<SummaryDbModel>();

    [StringLength(1000)]
    public string? Title { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
