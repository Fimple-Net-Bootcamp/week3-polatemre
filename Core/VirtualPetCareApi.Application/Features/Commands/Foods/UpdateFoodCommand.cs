using MediatR;
using Microsoft.Extensions.Logging;
using VirtualPetCareApi.Application.Repositories;
using VirtualPetCareApi.Domain.Entities;

namespace VirtualFoodCareApi.Application.Features.Commands.Foods
{
    public class UpdateFoodCommand : IRequest<UpdateFoodCommandResponse>
    {
        public int PetId { get; set; }
        public int FoodId { get; set; }

        public class UpdateFoodCommandHandler : IRequestHandler<UpdateFoodCommand, UpdateFoodCommandResponse>
        {
            readonly IPetReadRepository _petReadRepository;
            readonly IPetWriteRepository _petWriteRepository;
            readonly IHealthReadRepository _healthReadRepository;
            readonly IHealthWriteRepository _healthWriteRepository;
            readonly IFoodWriteRepository _foodWriteRepository;
            readonly IFoodReadRepository _foodReadRepository;
            readonly ILogger<UpdateFoodCommandHandler> _logger;
            public UpdateFoodCommandHandler(IFoodWriteRepository foodWriteRepository, IFoodReadRepository foodReadRepository, IHealthWriteRepository healthWriteRepository, IHealthReadRepository healthReadRepository, IPetWriteRepository petWriteRepository, IPetReadRepository petReadRepository)
            {
                _foodWriteRepository = foodWriteRepository;
                _foodReadRepository = foodReadRepository;
                _healthWriteRepository = healthWriteRepository;
                _healthReadRepository = healthReadRepository;
                _petWriteRepository = petWriteRepository;
                _petReadRepository = petReadRepository;
            }
            public async Task<UpdateFoodCommandResponse> Handle(UpdateFoodCommand request, CancellationToken cancellationToken)
            {
                var food = await _foodReadRepository.GetByIdAsync(request.FoodId);
                var pet = await _petReadRepository.GetByIdAsync(request.PetId);
                pet.Health.Value += food.Value;
                _healthWriteRepository.Update(pet.Health);
                await _healthWriteRepository.SaveAsync();

                return new()
                {
                    Id = pet.Id,
                    Name = pet.Name,
                    Health = pet.Health
                };
            }
        }

    }

    public class UpdateFoodCommandResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Health Health { get; set; }
    }
}
