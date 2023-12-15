using VirtualPetCareApi.Application.Abstractions.Hubs;
using VirtualPetCareApi.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPetCareApi.Application.Features.Commands.Activities
{
    public class CreateActivityCommand : IRequest<CreateActivityCommandResponse>
    {
        public Guid ActivityTypeId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        //public bool? IsConstAmount { get; set; }
        //public float? ConstAmount { get; set; }
        //public ActivityType ActivityType { get; set; }
        //public ICollection<Donation> Donations { get; set; }

        public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, CreateActivityCommandResponse>
        {
            readonly IActivityWriteRepository _activityWriteRepository;
            readonly IActivityHubService _activityHubService;
            public CreateActivityCommandHandler(IActivityWriteRepository activityWriteRepository, IActivityHubService activityHubService)
            {
                _activityWriteRepository = activityWriteRepository;
                _activityHubService = activityHubService;
            }
            public async Task<CreateActivityCommandResponse> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
            {
                await _activityWriteRepository.AddAsync(new()
                {
                    //ConstAmount = model.ConstAmount,
                    Description = request.Description,
                    //IsConstAmount = model.IsConstAmount,
                    Name = request.Name,
                    ActivityTypeId = request.ActivityTypeId
                });
                await _activityWriteRepository.SaveAsync();
                await _activityHubService.ActivityAddedMessageAsync($"{request.Name} isminde aktivite eklenmiştir.");
                return new CreateActivityCommandResponse() { };
            }
        }

    }

    public class CreateActivityCommandResponse
    {

    }
}
