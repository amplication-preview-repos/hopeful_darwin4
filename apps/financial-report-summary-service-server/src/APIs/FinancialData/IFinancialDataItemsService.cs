using FinancialReportSummaryService.APIs.Common;
using FinancialReportSummaryService.APIs.Dtos;

namespace FinancialReportSummaryService.APIs;

public interface IFinancialDataItemsService
{
    /// <summary>
    /// Create one FinancialData
    /// </summary>
    public Task<FinancialData> CreateFinancialData(FinancialDataCreateInput financialdata);

    /// <summary>
    /// Delete one FinancialData
    /// </summary>
    public Task DeleteFinancialData(FinancialDataWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many FinancialDataItems
    /// </summary>
    public Task<List<FinancialData>> FinancialDataItems(FinancialDataFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about FinancialData records
    /// </summary>
    public Task<MetadataDto> FinancialDataItemsMeta(FinancialDataFindManyArgs findManyArgs);

    /// <summary>
    /// Get one FinancialData
    /// </summary>
    public Task<FinancialData> FinancialData(FinancialDataWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one FinancialData
    /// </summary>
    public Task UpdateFinancialData(
        FinancialDataWhereUniqueInput uniqueId,
        FinancialDataUpdateInput updateDto
    );

    /// <summary>
    /// Get a Report record for FinancialData
    /// </summary>
    public Task<Report> GetReport(FinancialDataWhereUniqueInput uniqueId);
}
