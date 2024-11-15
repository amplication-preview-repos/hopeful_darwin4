using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialReportSummaryService.Infrastructure.Models;

[Table("Summaries")]
public class SummaryDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? GeneratedDate { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? ReportId { get; set; }

    [ForeignKey(nameof(ReportId))]
    public ReportDbModel? Report { get; set; } = null;

    [StringLength(1000)]
    public string? SummaryContent { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
