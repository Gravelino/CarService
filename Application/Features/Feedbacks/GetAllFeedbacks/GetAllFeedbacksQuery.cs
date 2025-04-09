﻿using Application.Models;
using MediatR;

namespace Application.Features.Feedbacks.GetAllFeedbacks;

public class GetAllFeedbacksQuery : IRequest<PagedResult<Feedback>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SortField { get; set; }
    public string? SortOrder { get; set; }
    public string? NameFilter { get; set; }
}