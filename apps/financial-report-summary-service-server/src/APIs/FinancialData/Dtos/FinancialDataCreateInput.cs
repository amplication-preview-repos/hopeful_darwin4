namespace FinancialReportSummaryService.APIs.Dtos;

public class FinancialDataCreateInput
{
    public DateTime CreatedAt { get; set; }

    public double? DataPoint { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public Report? Report { get; set; }

    public DateTime UpdatedAt { get; set; }
}
