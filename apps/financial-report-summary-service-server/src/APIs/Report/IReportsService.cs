using FinancialReportSummaryService.APIs.Common;
using FinancialReportSummaryService.APIs.Dtos;

namespace FinancialReportSummaryService.APIs;

public interface IReportsService
{
    /// <summary>
    /// Create one Report
    /// </summary>
    public Task<Report> CreateReport(ReportCreateInput report);

    /// <summary>
    /// Delete one Report
    /// </summary>
    public Task DeleteReport(ReportWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Reports
    /// </summary>
    public Task<List<Report>> Reports(ReportFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Report records
    /// </summary>
    public Task<MetadataDto> ReportsMeta(ReportFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Report
    /// </summary>
    public Task<Report> Report(ReportWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Report
    /// </summary>
    public Task UpdateReport(ReportWhereUniqueInput uniqueId, ReportUpdateInput updateDto);

    /// <summary>
    /// Connect multiple FinancialDataItems records to Report
    /// </summary>
    public Task ConnectFinancialDataItems(
        ReportWhereUniqueInput uniqueId,
        FinancialDataWhereUniqueInput[] financialDataId
    );

    /// <summary>
    /// Disconnect multiple FinancialDataItems records from Report
    /// </summary>
    public Task DisconnectFinancialDataItems(
        ReportWhereUniqueInput uniqueId,
        FinancialDataWhereUniqueInput[] financialDataId
    );

    /// <summary>
    /// Find multiple FinancialDataItems records for Report
    /// </summary>
    public Task<List<FinancialData>> FindFinancialDataItems(
        ReportWhereUniqueInput uniqueId,
        FinancialDataFindManyArgs FinancialDataFindManyArgs
    );

    /// <summary>
    /// Update multiple FinancialDataItems records for Report
    /// </summary>
    public Task UpdateFinancialDataItems(
        ReportWhereUniqueInput uniqueId,
        FinancialDataWhereUniqueInput[] financialDataId
    );

    /// <summary>
    /// Connect multiple Summaries records to Report
    /// </summary>
    public Task ConnectSummaries(
        ReportWhereUniqueInput uniqueId,
        SummaryWhereUniqueInput[] summariesId
    );

    /// <summary>
    /// Disconnect multiple Summaries records from Report
    /// </summary>
    public Task DisconnectSummaries(
        ReportWhereUniqueInput uniqueId,
        SummaryWhereUniqueInput[] summariesId
    );

    /// <summary>
    /// Find multiple Summaries records for Report
    /// </summary>
    public Task<List<Summary>> FindSummaries(
        ReportWhereUniqueInput uniqueId,
        SummaryFindManyArgs SummaryFindManyArgs
    );

    /// <summary>
    /// Update multiple Summaries records for Report
    /// </summary>
    public Task UpdateSummaries(
        ReportWhereUniqueInput uniqueId,
        SummaryWhereUniqueInput[] summariesId
    );
}
