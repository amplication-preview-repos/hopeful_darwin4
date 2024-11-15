using FinancialReportSummaryService.APIs.Dtos;
using FinancialReportSummaryService.Infrastructure.Models;

namespace FinancialReportSummaryService.APIs.Extensions;

public static class ReportsExtensions
{
    public static Report ToDto(this ReportDbModel model)
    {
        return new Report
        {
            Content = model.Content,
            CreatedAt = model.CreatedAt,
            FinancialDataItems = model.FinancialDataItems?.Select(x => x.Id).ToList(),
            Id = model.Id,
            PublishedDate = model.PublishedDate,
            Summaries = model.Summaries?.Select(x => x.Id).ToList(),
            Title = model.Title,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ReportDbModel ToModel(
        this ReportUpdateInput updateDto,
        ReportWhereUniqueInput uniqueId
    )
    {
        var report = new ReportDbModel
        {
            Id = uniqueId.Id,
            Content = updateDto.Content,
            PublishedDate = updateDto.PublishedDate,
            Title = updateDto.Title
        };

        if (updateDto.CreatedAt != null)
        {
            report.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            report.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return report;
    }
}
