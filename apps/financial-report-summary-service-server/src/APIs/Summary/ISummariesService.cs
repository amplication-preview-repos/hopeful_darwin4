using FinancialReportSummaryService.APIs.Common;
using FinancialReportSummaryService.APIs.Dtos;

namespace FinancialReportSummaryService.APIs;

public interface ISummariesService
{
    /// <summary>
    /// Create one Summary
    /// </summary>
    public Task<Summary> CreateSummary(SummaryCreateInput summary);

    /// <summary>
    /// Delete one Summary
    /// </summary>
    public Task DeleteSummary(SummaryWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Summaries
    /// </summary>
    public Task<List<Summary>> Summaries(SummaryFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Summary records
    /// </summary>
    public Task<MetadataDto> SummariesMeta(SummaryFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Summary
    /// </summary>
    public Task<Summary> Summary(SummaryWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Summary
    /// </summary>
    public Task UpdateSummary(SummaryWhereUniqueInput uniqueId, SummaryUpdateInput updateDto);

    /// <summary>
    /// Get a Report record for Summary
    /// </summary>
    public Task<Report> GetReport(SummaryWhereUniqueInput uniqueId);
}
