using FinancialReportSummaryService.APIs.Dtos;
using FinancialReportSummaryService.Infrastructure.Models;

namespace FinancialReportSummaryService.APIs.Extensions;

public static class FinancialDataItemsExtensions
{
    public static FinancialData ToDto(this FinancialDataDbModel model)
    {
        return new FinancialData
        {
            CreatedAt = model.CreatedAt,
            DataPoint = model.DataPoint,
            Description = model.Description,
            Id = model.Id,
            Report = model.ReportId,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static FinancialDataDbModel ToModel(
        this FinancialDataUpdateInput updateDto,
        FinancialDataWhereUniqueInput uniqueId
    )
    {
        var financialData = new FinancialDataDbModel
        {
            Id = uniqueId.Id,
            DataPoint = updateDto.DataPoint,
            Description = updateDto.Description
        };

        if (updateDto.CreatedAt != null)
        {
            financialData.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.Report != null)
        {
            financialData.ReportId = updateDto.Report;
        }
        if (updateDto.UpdatedAt != null)
        {
            financialData.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return financialData;
    }
}
