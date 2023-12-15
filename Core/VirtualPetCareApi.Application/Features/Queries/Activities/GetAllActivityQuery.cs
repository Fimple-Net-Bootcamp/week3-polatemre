using VirtualPetCareApi.Application.Features.Models;
using VirtualPetCareApi.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VirtualPetCareApi.Application.Features.Queries.Activities
{
    public class GetAllActivityQuery : PageableQueryRequest, IRequest<GetAllActivityQueryResponse>
    {

        public class GetAllActivityQueryHandler : IRequestHandler<GetAllActivityQuery, GetAllActivityQueryResponse>
        {
            readonly IActivityReadRepository _activityReadRepository;
            readonly ILogger<GetAllActivityQueryHandler> _logger;
            public GetAllActivityQueryHandler(IActivityReadRepository activityReadRepository, ILogger<GetAllActivityQueryHandler> logger)
            {
                _activityReadRepository = activityReadRepository;
                _logger = logger;
            }
            public async Task<GetAllActivityQueryResponse> Handle(GetAllActivityQuery request, CancellationToken cancellationToken)
            {
                //_logger.LogInformation("Get all activities...");
                //throw new Exception("Hata alındııı...");
                var totalCount = _activityReadRepository.GetAll(false).Count();
                var activities = _activityReadRepository.GetAll(false)
                    .Include(p => p.ActivityImageFiles)
                    .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.IsConstAmount,
                    p.ConstAmount,
                    p.CreatedDate,
                    p.UpdatedDate,
                    p.ActivityImageFiles
                }).Skip(request.Page * request.Size).Take(request.Size);

                return new()
                {
                    Activities = activities,
                    TotalCount = totalCount
                };
            }
        }
    }

    public class GetAllActivityQueryResponse
    {
        public int TotalCount { get; set; }
        public object Activities { get; set; }
    }
}
