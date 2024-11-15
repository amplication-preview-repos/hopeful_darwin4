namespace FinancialReportSummaryService.APIs.Dtos;

public class SummaryCreateInput
{
    public DateTime CreatedAt { get; set; }

    public DateTime? GeneratedDate { get; set; }

    public string? Id { get; set; }

    public Report? Report { get; set; }

    public string? SummaryContent { get; set; }

    public DateTime UpdatedAt { get; set; }
}
