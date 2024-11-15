namespace FinancialReportSummaryService.APIs.Dtos;

public class FinancialDataWhereInput
{
    public DateTime? CreatedAt { get; set; }

    public double? DataPoint { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public string? Report { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
