using FinancialReportSummaryService.APIs;
using FinancialReportSummaryService.APIs.Common;
using FinancialReportSummaryService.APIs.Dtos;
using FinancialReportSummaryService.APIs.Errors;
using FinancialReportSummaryService.APIs.Extensions;
using FinancialReportSummaryService.Infrastructure;
using FinancialReportSummaryService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialReportSummaryService.APIs;

public abstract class SummariesServiceBase : ISummariesService
{
    protected readonly FinancialReportSummaryServiceDbContext _context;

    public SummariesServiceBase(FinancialReportSummaryServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Summary
    /// </summary>
    public async Task<Summary> CreateSummary(SummaryCreateInput createDto)
    {
        var summary = new SummaryDbModel
        {
            CreatedAt = createDto.CreatedAt,
            GeneratedDate = createDto.GeneratedDate,
            SummaryContent = createDto.SummaryContent,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            summary.Id = createDto.Id;
        }
        if (createDto.Report != null)
        {
            summary.Report = await _context
                .Reports.Where(report => createDto.Report.Id == report.Id)
                .FirstOrDefaultAsync();
        }

        _context.Summaries.Add(summary);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<SummaryDbModel>(summary.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Summary
    /// </summary>
    public async Task DeleteSummary(SummaryWhereUniqueInput uniqueId)
    {
        var summary = await _context.Summaries.FindAsync(uniqueId.Id);
        if (summary == null)
        {
            throw new NotFoundException();
        }

        _context.Summaries.Remove(summary);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Summaries
    /// </summary>
    public async Task<List<Summary>> Summaries(SummaryFindManyArgs findManyArgs)
    {
        var summaries = await _context
            .Summaries.Include(x => x.Report)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return summaries.ConvertAll(summary => summary.ToDto());
    }

    /// <summary>
    /// Meta data about Summary records
    /// </summary>
    public async Task<MetadataDto> SummariesMeta(SummaryFindManyArgs findManyArgs)
    {
        var count = await _context.Summaries.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Summary
    /// </summary>
    public async Task<Summary> Summary(SummaryWhereUniqueInput uniqueId)
    {
        var summaries = await this.Summaries(
            new SummaryFindManyArgs { Where = new SummaryWhereInput { Id = uniqueId.Id } }
        );
        var summary = summaries.FirstOrDefault();
        if (summary == null)
        {
            throw new NotFoundException();
        }

        return summary;
    }

    /// <summary>
    /// Update one Summary
    /// </summary>
    public async Task UpdateSummary(SummaryWhereUniqueInput uniqueId, SummaryUpdateInput updateDto)
    {
        var summary = updateDto.ToModel(uniqueId);

        if (updateDto.Report != null)
        {
            summary.Report = await _context
                .Reports.Where(report => updateDto.Report == report.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(summary).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Summaries.Any(e => e.Id == summary.Id))
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
    /// Get a Report record for Summary
    /// </summary>
    public async Task<Report> GetReport(SummaryWhereUniqueInput uniqueId)
    {
        var summary = await _context
            .Summaries.Where(summary => summary.Id == uniqueId.Id)
            .Include(summary => summary.Report)
            .FirstOrDefaultAsync();
        if (summary == null)
        {
            throw new NotFoundException();
        }
        return summary.Report.ToDto();
    }
}
