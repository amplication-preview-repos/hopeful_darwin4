using FinancialReportSummaryService.APIs;
using FinancialReportSummaryService.APIs.Common;
using FinancialReportSummaryService.APIs.Dtos;
using FinancialReportSummaryService.APIs.Errors;
using FinancialReportSummaryService.APIs.Extensions;
using FinancialReportSummaryService.Infrastructure;
using FinancialReportSummaryService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FinancialReportSummaryService.APIs;

public abstract class ReportsServiceBase : IReportsService
{
    protected readonly FinancialReportSummaryServiceDbContext _context;

    public ReportsServiceBase(FinancialReportSummaryServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Report
    /// </summary>
    public async Task<Report> CreateReport(ReportCreateInput createDto)
    {
        var report = new ReportDbModel
        {
            Content = createDto.Content,
            CreatedAt = createDto.CreatedAt,
            PublishedDate = createDto.PublishedDate,
            Title = createDto.Title,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            report.Id = createDto.Id;
        }
        if (createDto.FinancialDataItems != null)
        {
            report.FinancialDataItems = await _context
                .FinancialDataItems.Where(financialData =>
                    createDto.FinancialDataItems.Select(t => t.Id).Contains(financialData.Id)
                )
                .ToListAsync();
        }

        if (createDto.Summaries != null)
        {
            report.Summaries = await _context
                .Summaries.Where(summary =>
                    createDto.Summaries.Select(t => t.Id).Contains(summary.Id)
                )
                .ToListAsync();
        }

        _context.Reports.Add(report);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ReportDbModel>(report.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Report
    /// </summary>
    public async Task DeleteReport(ReportWhereUniqueInput uniqueId)
    {
        var report = await _context.Reports.FindAsync(uniqueId.Id);
        if (report == null)
        {
            throw new NotFoundException();
        }

        _context.Reports.Remove(report);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Reports
    /// </summary>
    public async Task<List<Report>> Reports(ReportFindManyArgs findManyArgs)
    {
        var reports = await _context
            .Reports.Include(x => x.Summaries)
            .Include(x => x.FinancialDataItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return reports.ConvertAll(report => report.ToDto());
    }

    /// <summary>
    /// Meta data about Report records
    /// </summary>
    public async Task<MetadataDto> ReportsMeta(ReportFindManyArgs findManyArgs)
    {
        var count = await _context.Reports.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Report
    /// </summary>
    public async Task<Report> Report(ReportWhereUniqueInput uniqueId)
    {
        var reports = await this.Reports(
            new ReportFindManyArgs { Where = new ReportWhereInput { Id = uniqueId.Id } }
        );
        var report = reports.FirstOrDefault();
        if (report == null)
        {
            throw new NotFoundException();
        }

        return report;
    }

    /// <summary>
    /// Update one Report
    /// </summary>
    public async Task UpdateReport(ReportWhereUniqueInput uniqueId, ReportUpdateInput updateDto)
    {
        var report = updateDto.ToModel(uniqueId);

        if (updateDto.FinancialDataItems != null)
        {
            report.FinancialDataItems = await _context
                .FinancialDataItems.Where(financialData =>
                    updateDto.FinancialDataItems.Select(t => t).Contains(financialData.Id)
                )
                .ToListAsync();
        }

        if (updateDto.Summaries != null)
        {
            report.Summaries = await _context
                .Summaries.Where(summary => updateDto.Summaries.Select(t => t).Contains(summary.Id))
                .ToListAsync();
        }

        _context.Entry(report).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Reports.Any(e => e.Id == report.Id))
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
    /// Connect multiple FinancialDataItems records to Report
    /// </summary>
    public async Task ConnectFinancialDataItems(
        ReportWhereUniqueInput uniqueId,
        FinancialDataWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Reports.Include(x => x.FinancialDataItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .FinancialDataItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.FinancialDataItems);

        foreach (var child in childrenToConnect)
        {
            parent.FinancialDataItems.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple FinancialDataItems records from Report
    /// </summary>
    public async Task DisconnectFinancialDataItems(
        ReportWhereUniqueInput uniqueId,
        FinancialDataWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Reports.Include(x => x.FinancialDataItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .FinancialDataItems.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.FinancialDataItems?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple FinancialDataItems records for Report
    /// </summary>
    public async Task<List<FinancialData>> FindFinancialDataItems(
        ReportWhereUniqueInput uniqueId,
        FinancialDataFindManyArgs reportFindManyArgs
    )
    {
        var financialDataItems = await _context
            .FinancialDataItems.Where(m => m.ReportId == uniqueId.Id)
            .ApplyWhere(reportFindManyArgs.Where)
            .ApplySkip(reportFindManyArgs.Skip)
            .ApplyTake(reportFindManyArgs.Take)
            .ApplyOrderBy(reportFindManyArgs.SortBy)
            .ToListAsync();

        return financialDataItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple FinancialDataItems records for Report
    /// </summary>
    public async Task UpdateFinancialDataItems(
        ReportWhereUniqueInput uniqueId,
        FinancialDataWhereUniqueInput[] childrenIds
    )
    {
        var report = await _context
            .Reports.Include(t => t.FinancialDataItems)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (report == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .FinancialDataItems.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        report.FinancialDataItems = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Summaries records to Report
    /// </summary>
    public async Task ConnectSummaries(
        ReportWhereUniqueInput uniqueId,
        SummaryWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Reports.Include(x => x.Summaries)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Summaries.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Summaries);

        foreach (var child in childrenToConnect)
        {
            parent.Summaries.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Summaries records from Report
    /// </summary>
    public async Task DisconnectSummaries(
        ReportWhereUniqueInput uniqueId,
        SummaryWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Reports.Include(x => x.Summaries)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Summaries.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Summaries?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Summaries records for Report
    /// </summary>
    public async Task<List<Summary>> FindSummaries(
        ReportWhereUniqueInput uniqueId,
        SummaryFindManyArgs reportFindManyArgs
    )
    {
        var summaries = await _context
            .Summaries.Where(m => m.ReportId == uniqueId.Id)
            .ApplyWhere(reportFindManyArgs.Where)
            .ApplySkip(reportFindManyArgs.Skip)
            .ApplyTake(reportFindManyArgs.Take)
            .ApplyOrderBy(reportFindManyArgs.SortBy)
            .ToListAsync();

        return summaries.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Summaries records for Report
    /// </summary>
    public async Task UpdateSummaries(
        ReportWhereUniqueInput uniqueId,
        SummaryWhereUniqueInput[] childrenIds
    )
    {
        var report = await _context
            .Reports.Include(t => t.Summaries)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (report == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Summaries.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        report.Summaries = children;
        await _context.SaveChangesAsync();
    }
}
