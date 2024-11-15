using FinancialReportSummaryService.APIs.Dtos;
using FinancialReportSummaryService.Infrastructure.Models;

namespace FinancialReportSummaryService.APIs.Extensions;

public static class SummariesExtensions
{
    public static Summary ToDto(this SummaryDbModel model)
    {
        return new Summary
        {
            CreatedAt = model.CreatedAt,
            GeneratedDate = model.GeneratedDate,
            Id = model.Id,
            Report = model.ReportId,
            SummaryContent = model.SummaryContent,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static SummaryDbModel ToModel(
        this SummaryUpdateInput updateDto,
        SummaryWhereUniqueInput uniqueId
    )
    {
        var summary = new SummaryDbModel
        {
            Id = uniqueId.Id,
            GeneratedDate = updateDto.GeneratedDate,
            SummaryContent = updateDto.SummaryContent
        };

        if (updateDto.CreatedAt != null)
        {
            summary.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Report != null)
        {
            summary.ReportId = updateDto.Report;
        }
        if (updateDto.UpdatedAt != null)
        {
            summary.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return summary;
    }
}
