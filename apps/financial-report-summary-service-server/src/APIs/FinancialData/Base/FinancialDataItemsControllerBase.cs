using FinancialReportSummaryService.APIs;
using FinancialReportSummaryService.APIs.Common;
using FinancialReportSummaryService.APIs.Dtos;
using FinancialReportSummaryService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace FinancialReportSummaryService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class FinancialDataItemsControllerBase : ControllerBase
{
    protected readonly IFinancialDataItemsService _service;

    public FinancialDataItemsControllerBase(IFinancialDataItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one FinancialData
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<FinancialData>> CreateFinancialData(
        FinancialDataCreateInput input
    )
    {
        var financialData = await _service.CreateFinancialData(input);

        return CreatedAtAction(nameof(FinancialData), new { id = financialData.Id }, financialData);
    }

    /// <summary>
    /// Delete one FinancialData
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteFinancialData(
        [FromRoute()] FinancialDataWhereUniqueInput uniqueId
    )
    {
        try
        {
            await _service.DeleteFinancialData(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many FinancialDataItems
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<FinancialData>>> FinancialDataItems(
        [FromQuery()] FinancialDataFindManyArgs filter
    )
    {
        return Ok(await _service.FinancialDataItems(filter));
    }

    /// <summary>
    /// Meta data about FinancialData records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> FinancialDataItemsMeta(
        [FromQuery()] FinancialDataFindManyArgs filter
    )
    {
        return Ok(await _service.FinancialDataItemsMeta(filter));
    }

    /// <summary>
    /// Get one FinancialData
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<FinancialData>> FinancialData(
        [FromRoute()] FinancialDataWhereUniqueInput uniqueId
    )
    {
        try
        {
            return await _service.FinancialData(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one FinancialData
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateFinancialData(
        [FromRoute()] FinancialDataWhereUniqueInput uniqueId,
        [FromQuery()] FinancialDataUpdateInput financialDataUpdateDto
    )
    {
        try
        {
            await _service.UpdateFinancialData(uniqueId, financialDataUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Get a Report record for FinancialData
    /// </summary>
    [HttpGet("{Id}/report")]
    public async Task<ActionResult<List<Report>>> GetReport(
        [FromRoute()] FinancialDataWhereUniqueInput uniqueId
    )
    {
        var report = await _service.GetReport(uniqueId);
        return Ok(report);
    }
}
