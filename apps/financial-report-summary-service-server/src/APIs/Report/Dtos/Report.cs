namespace FinancialReportSummaryService.APIs.Dtos;

public class Report
{
    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<string>? FinancialDataItems { get; set; }

    public string Id { get; set; }

    public DateTime? PublishedDate { get; set; }

    public List<string>? Summaries { get; set; }

    public string? Title { get; set; }

    public DateTime UpdatedAt { get; set; }
}
