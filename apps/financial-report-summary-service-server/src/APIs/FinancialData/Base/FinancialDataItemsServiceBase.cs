using FinancialReportSummaryService.APIs;
using FinancialReportSummaryService.APIs.Common;
using FinancialReportSummaryService.APIs.Dtos;
using FinancialReportSummaryService.APIs.Errors;
using FinancialReportSummaryService.APIs.Extensions;
using FinancialReportSummaryService.Infrastructure;
using FinancialReportSummaryService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialReportSummaryService.APIs;

public abstract class FinancialDataItemsServiceBase : IFinancialDataItemsService
{
    protected readonly FinancialReportSummaryServiceDbContext _context;

    public FinancialDataItemsServiceBase(FinancialReportSummaryServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one FinancialData
    /// </summary>
    public async Task<FinancialData> CreateFinancialData(FinancialDataCreateInput createDto)
    {
        var financialData = new FinancialDataDbModel
        {
            CreatedAt = createDto.CreatedAt,
            DataPoint = createDto.DataPoint,
            Description = createDto.Description,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            financialData.Id = createDto.Id;
        }
        if (createDto.Report != null)
        {
            financialData.Report = await _context
                .Reports.Where(report => createDto.Report.Id == report.Id)
                .FirstOrDefaultAsync();
        }

        _context.FinancialDataItems.Add(financialData);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<FinancialDataDbModel>(financialData.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one FinancialData
    /// </summary>
    public async Task DeleteFinancialData(FinancialDataWhereUniqueInput uniqueId)
    {
        var financialData = await _context.FinancialDataItems.FindAsync(uniqueId.Id);
        if (financialData == null)
        {
            throw new NotFoundException();
        }

        _context.FinancialDataItems.Remove(financialData);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many FinancialDataItems
    /// </summary>
    public async Task<List<FinancialData>> FinancialDataItems(
        FinancialDataFindManyArgs findManyArgs
    )
    {
        var financialDataItems = await _context
            .FinancialDataItems.Include(x => x.Report)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return financialDataItems.ConvertAll(financialData => financialData.ToDto());
    }

    /// <summary>
    /// Meta data about FinancialData records
    /// </summary>
    public async Task<MetadataDto> FinancialDataItemsMeta(FinancialDataFindManyArgs findManyArgs)
    {
        var count = await _context.FinancialDataItems.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one FinancialData
    /// </summary>
    public async Task<FinancialData> FinancialData(FinancialDataWhereUniqueInput uniqueId)
    {
        var financialDataItems = await this.FinancialDataItems(
            new FinancialDataFindManyArgs
            {
                Where = new FinancialDataWhereInput { Id = uniqueId.Id }
            }
        );
        var financialData = financialDataItems.FirstOrDefault();
        if (financialData == null)
        {
            throw new NotFoundException();
        }

        return financialData;
    }

    /// <summary>
    /// Update one FinancialData
    /// </summary>
    public async Task UpdateFinancialData(
        FinancialDataWhereUniqueInput uniqueId,
        FinancialDataUpdateInput updateDto
    )
    {
        var financialData = updateDto.ToModel(uniqueId);

        if (updateDto.Report != null)
        {
            financialData.Report = await _context
                .Reports.Where(report => updateDto.Report == report.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(financialData).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.FinancialDataItems.Any(e => e.Id == financialData.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Report record for FinancialData
    /// </summary>
    public async Task<Report> GetReport(FinancialDataWhereUniqueInput uniqueId)
    {
        var financialData = await _context
            .FinancialDataItems.Where(financialData => financialData.Id == uniqueId.Id)
            .Include(financialData => financialData.Report)
            .FirstOrDefaultAsync();
        if (financialData == null)
        {
            throw new NotFoundException();
        }
        return financialData.Report.ToDto();
    }
}
