using VirtualPetCareApi.Application.Repositories;
using VirtualPetCareApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace VirtualPetCareApi.Application.Features.Queries.Activities
{
    public class GetByPetIdAcitivityQuery : IRequest<GetByPetIdAcitivityQueryResponse>
    {
        public int PetId { get; set; }
        public class GetByPetIdActivityQueryHandler : IRequestHandler<GetByPetIdAcitivityQuery, GetByPetIdAcitivityQueryResponse>
        {
            readonly IActivityReadRepository _activityReadRepository;
            readonly IPetReadRepository _petReadRepository;

            public GetByPetIdActivityQueryHandler(IActivityReadRepository activityReadRepository, IPetReadRepository petReadRepository)
            {
                _activityReadRepository = activityReadRepository;
                _petReadRepository = petReadRepository;
            }
            public async Task<GetByPetIdAcitivityQueryResponse> Handle(GetByPetIdAcitivityQuery request, CancellationToken cancellationToken)
            {
                Pet? pet = await _petReadRepository.Table.Include(x => x.Activities).Where(x => x.Id == request.PetId).FirstOrDefaultAsync();
                return new()
                {
                    Name = pet.Name,
                    Activities = pet.Activities.ToList()
                };
            }
        }
    }

    public class GetByPetIdAcitivityQueryResponse
    {
        public string Name { get; set; }
        public List<Activity> Activities { get; set; }
    }
}
