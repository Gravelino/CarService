﻿using Application.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories;

public class FeedbackRepository : SoftDeletableRepository<Feedback>, IFeedbackRepository
{
    public FeedbackRepository(CarServiceDbContext dbContext) : base(dbContext)
    {
        
    }

    public async Task<IEnumerable<Feedback>> GetFeedbackForVisitAsync(int visitId)
    {
        return await _context.Feedbacks
            .Where(f => f.VisitId == visitId)
            .ToListAsync();
    }

    public async Task<double> GetAverageRatingForServiceAsync(int serviceId)
    {
        return await _context.Feedbacks
            .Where(f => f.Visit != null && f.Visit.VisitServices.Any(vs => vs.ServiceId == serviceId))
            .AverageAsync(f => f.Rating);
    }
}