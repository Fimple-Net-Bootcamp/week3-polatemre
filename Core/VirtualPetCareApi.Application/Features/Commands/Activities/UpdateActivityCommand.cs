using VirtualPetCareApi.Application.Repositories;
using VirtualPetCareApi.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPetCareApi.Application.Features.Commands.Activities
{
    public class UpdateActivityCommand : IRequest<UpdateActivityCommandResponse>
    {
        public string Id { get; set; }
        public Guid ActivityTypeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool? IsConstAmount { get; set; }
        public float? ConstAmount { get; set; }
        //public ActivityType ActivityType { get; set; }
        //public ICollection<Donation> Donations { get; set; }

        public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, UpdateActivityCommandResponse>
        {
            readonly IActivityWriteRepository _activityWriteRepository;
            readonly IActivityReadRepository _activityReadRepository;
            readonly ILogger<UpdateActivityCommandHandler> _logger;
            public UpdateActivityCommandHandler(IActivityWriteRepository activityWriteRepository, IActivityReadRepository activityReadRepository)
            {
                _activityWriteRepository = activityWriteRepository;
                _activityReadRepository = activityReadRepository;
            }
            public async Task<UpdateActivityCommandResponse> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
            {
                Activity activity = await _activityReadRepository.GetByIdAsync(request.Id);
                activity.Description = request.Description;
                activity.Name = request.Name;
                activity.IsConstAmount = request.IsConstAmount;
                activity.ActivityTypeId = request.ActivityTypeId;
                await _activityWriteRepository.SaveAsync();
                
                return new();
            }
        }

    }

    public class UpdateActivityCommandResponse
    {

    }
}
