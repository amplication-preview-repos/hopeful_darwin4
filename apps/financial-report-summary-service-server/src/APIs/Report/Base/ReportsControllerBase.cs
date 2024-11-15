using FinancialReportSummaryService.APIs;
using FinancialReportSummaryService.APIs.Common;
using FinancialReportSummaryService.APIs.Dtos;
using FinancialReportSummaryService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FinancialReportSummaryService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ReportsControllerBase : ControllerBase
{
    protected readonly IReportsService _service;

    public ReportsControllerBase(IReportsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Report
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Report>> CreateReport(ReportCreateInput input)
    {
        var report = await _service.CreateReport(input);

        return CreatedAtAction(nameof(Report), new { id = report.Id }, report);
    }

    /// <summary>
    /// Delete one Report
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteReport([FromRoute()] ReportWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteReport(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Reports
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Report>>> Reports([FromQuery()] ReportFindManyArgs filter)
    {
        return Ok(await _service.Reports(filter));
    }

    /// <summary>
    /// Meta data about Report records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> ReportsMeta(
        [FromQuery()] ReportFindManyArgs filter
    )
    {
        return Ok(await _service.ReportsMeta(filter));
    }

    /// <summary>
    /// Get one Report
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Report>> Report([FromRoute()] ReportWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Report(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Report
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateReport(
        [FromRoute()] ReportWhereUniqueInput uniqueId,
        [FromQuery()] ReportUpdateInput reportUpdateDto
    )
    {
        try
        {
            await _service.UpdateReport(uniqueId, reportUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple FinancialDataItems records to Report
    /// </summary>
    [HttpPost("{Id}/financialDataItems")]
    public async Task<ActionResult> ConnectFinancialDataItems(
        [FromRoute()] ReportWhereUniqueInput uniqueId,
        [FromQuery()] FinancialDataWhereUniqueInput[] financialDataItemsId
    )
    {
        try
        {
            await _service.ConnectFinancialDataItems(uniqueId, financialDataItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple FinancialDataItems records from Report
    /// </summary>
    [HttpDelete("{Id}/financialDataItems")]
    public async Task<ActionResult> DisconnectFinancialDataItems(
        [FromRoute()] ReportWhereUniqueInput uniqueId,
        [FromBody()] FinancialDataWhereUniqueInput[] financialDataItemsId
    )
    {
        try
        {
            await _service.DisconnectFinancialDataItems(uniqueId, financialDataItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple FinancialDataItems records for Report
    /// </summary>
    [HttpGet("{Id}/financialDataItems")]
    public async Task<ActionResult<List<FinancialData>>> FindFinancialDataItems(
        [FromRoute()] ReportWhereUniqueInput uniqueId,
        [FromQuery()] FinancialDataFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindFinancialDataItems(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple FinancialDataItems records for Report
    /// </summary>
    [HttpPatch("{Id}/financialDataItems")]
    public async Task<ActionResult> UpdateFinancialDataItems(
        [FromRoute()] ReportWhereUniqueInput uniqueId,
        [FromBody()] FinancialDataWhereUniqueInput[] financialDataItemsId
    )
    {
        try
        {
            await _service.UpdateFinancialDataItems(uniqueId, financialDataItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple Summaries records to Report
    /// </summary>
    [HttpPost("{Id}/summaries")]
    public async Task<ActionResult> ConnectSummaries(
        [FromRoute()] ReportWhereUniqueInput uniqueId,
        [FromQuery()] SummaryWhereUniqueInput[] summariesId
    )
    {
        try
        {
            await _service.ConnectSummaries(uniqueId, summariesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Summaries records from Report
    /// </summary>
    [HttpDelete("{Id}/summaries")]
    public async Task<ActionResult> DisconnectSummaries(
        [FromRoute()] ReportWhereUniqueInput uniqueId,
        [FromBody()] SummaryWhereUniqueInput[] summariesId
    )
    {
        try
        {
            await _service.DisconnectSummaries(uniqueId, summariesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Summaries records for Report
    /// </summary>
    [HttpGet("{Id}/summaries")]
    public async Task<ActionResult<List<Summary>>> FindSummaries(
        [FromRoute()] ReportWhereUniqueInput uniqueId,
        [FromQuery()] SummaryFindManyArgs filter
    )
    {
        try
        {
            return Ok(await _service.FindSummaries(uniqueId, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Summaries records for Report
    /// </summary>
    [HttpPatch("{Id}/summaries")]
    public async Task<ActionResult> UpdateSummaries(
        [FromRoute()] ReportWhereUniqueInput uniqueId,
        [FromBody()] SummaryWhereUniqueInput[] summariesId
    )
    {
        try
        {
            await _service.UpdateSummaries(uniqueId, summariesId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
