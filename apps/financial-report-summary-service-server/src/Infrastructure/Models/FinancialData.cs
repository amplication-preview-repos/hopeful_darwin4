using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancialReportSummaryService.Infrastructure.Models;

[Table("FinancialData")]
public class FinancialDataDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [Range(-999999999, 999999999)]
    public double? DataPoint { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? ReportId { get; set; }

    [ForeignKey(nameof(ReportId))]
    public ReportDbModel? Report { get; set; } = null;

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
