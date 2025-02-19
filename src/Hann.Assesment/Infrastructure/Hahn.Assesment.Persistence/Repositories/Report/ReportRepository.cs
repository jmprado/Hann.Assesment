﻿using Hahn.Assesment.Domain.Entities;
using Hahn.Assesment.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Hahn.Assesment.Persistence.Repositories.Report
{
    public class ReportRepository : IReportRepository
    {
        private readonly AppDbContext _context;

        public ReportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddReportAsync(AlertReport report)
        {
            await _context.AlertReports.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AlertReport>> GetAlertReportsAsync(Guid alertId)
        {
            return await _context.AlertReports
                .AsNoTracking()
                .OrderByDescending(r => r.ReportDate)
                .Where(r => r.AlertId == alertId)
                .ToListAsync();
        }
    }
}
