﻿using Application.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories;

public class ServiceRepository : SoftDeletableRepository<Service>, IServiceRepository
{
    public ServiceRepository(CarServiceDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<IEnumerable<Service>> GetServicesByCategoryIdAsync(int categoryId)
    {
        return await _dbSet
            .Where(s => s.CategoryId == categoryId && s.DeletedAt == null)
            .ToListAsync();
    }

    public async Task<IEnumerable<Service>> GetMostPopularServicesAsync(int limit)
    {
        return await _context.VisitServices
            .Where(vs => vs.Service.DeletedAt == null)
            .GroupBy(vs => vs.ServiceId)
            .Select(g => new { ServiceId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(limit)
            .Join(_dbSet, x => x.ServiceId, s => s.Id, (x, s) => s)
            .ToListAsync();
    }
}