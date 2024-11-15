using FinancialReportSummaryService.APIs;
using FinancialReportSummaryService.APIs.Common;
using FinancialReportSummaryService.APIs.Dtos;
using FinancialReportSummaryService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FinancialReportSummaryService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class SummariesControllerBase : ControllerBase
{
    protected readonly ISummariesService _service;

    public SummariesControllerBase(ISummariesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Summary
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Summary>> CreateSummary(SummaryCreateInput input)
    {
        var summary = await _service.CreateSummary(input);

        return CreatedAtAction(nameof(Summary), new { id = summary.Id }, summary);
    }

    /// <summary>
    /// Delete one Summary
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteSummary([FromRoute()] SummaryWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteSummary(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Summaries
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Summary>>> Summaries(
        [FromQuery()] SummaryFindManyArgs filter
    )
    {
        return Ok(await _service.Summaries(filter));
    }

    /// <summary>
    /// Meta data about Summary records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> SummariesMeta(
        [FromQuery()] SummaryFindManyArgs filter
    )
    {
        return Ok(await _service.SummariesMeta(filter));
    }

    /// <summary>
    /// Get one Summary
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Summary>> Summary([FromRoute()] SummaryWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Summary(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Summary
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateSummary(
        [FromRoute()] SummaryWhereUniqueInput uniqueId,
        [FromQuery()] SummaryUpdateInput summaryUpdateDto
    )
    {
        try
        {
            await _service.UpdateSummary(uniqueId, summaryUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Report record for Summary
    /// </summary>
    [HttpGet("{Id}/report")]
    public async Task<ActionResult<List<Report>>> GetReport(
        [FromRoute()] SummaryWhereUniqueInput uniqueId
    )
    {
        var report = await _service.GetReport(uniqueId);
        return Ok(report);
    }
}
