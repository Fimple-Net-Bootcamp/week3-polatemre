using VirtualPetCareApi.Application.Repositories;
using VirtualPetCareApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPetCareApi.Application.Features.Queries.Activities
{
    public class GetByIdAcitivityQuery : IRequest<GetByIdAcitivityQueryResponse>
    {
        public string Id { get; set; }
        public class GetByIdActivityQueryHandler : IRequestHandler<GetByIdAcitivityQuery, GetByIdAcitivityQueryResponse>
        {
            readonly IActivityReadRepository _activityReadRepository;

            public GetByIdActivityQueryHandler(IActivityReadRepository activityReadRepository)
            {
                _activityReadRepository = activityReadRepository;
            }
            public async Task<GetByIdAcitivityQueryResponse> Handle(GetByIdAcitivityQuery request, CancellationToken cancellationToken)
            {
                Activity activity = await _activityReadRepository.GetByIdAsync(request.Id, false);
                return new()
                {
                    ActivityTypeId = activity.ActivityTypeId,
                    ConstAmount = activity.ConstAmount,
                    Description = activity.Description,
                    IsConstAmount = activity.IsConstAmount,
                    Name = activity.Name
                };
            }
        }
    }

    public class GetByIdAcitivityQueryResponse
    {
        public Guid ActivityTypeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? IsConstAmount { get; set; }
        public float? ConstAmount { get; set; }
        //public ActivityType ActivityType { get; set; }
        //public ICollection<Donation> Donations { get; set; }
        //public ICollection<ActivityImageFile> ActivityImageFiles { get; set; }
    }
}
