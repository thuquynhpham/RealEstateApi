using RealEstate.Api.Handlers._Shared;
using RealEstate.Api.Handlers.User.Dtos;
using RealEstate.Core;
using RealEstate.Domain.Repositories;

namespace RealEstate.Api.Handlers.User
{
    public class RegisterUserHandler : CommandHandlerBase<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRequestContextProvider _contextProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TimeProvider _timeProvider;
        public RegisterUserHandler(IUnitOfWork unitOfWork, IRequestContextProvider contextProvider, TimeProvider timeProvider)
        {
            _userRepository = unitOfWork.Users;
            _contextProvider = contextProvider;
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
        }

        public override async Task<CommandApiResponse> Handle(RegisterUserCommand request, CancellationToken ct)
        {
            var existingUser = await _userRepository.FindAsync(x => x.Email == request.Model.Email, ct);
            if (existingUser != null)
                return CommandApiResponse.CreateValidationFailed("Email is already registered.");

            var newUser = new Domain.Models.User
            {
                Email = request.Model.Email,
                Name = request.Model.Name,
                Password = request.Model.Password,
                Phone = request.Model.Phone
            };
            await _userRepository.AddAsync(newUser, ct);
            await _unitOfWork.CompleteAsync(ct);

            return CommandApiResponse.Success();
        }
    }

    public record RegisterUserCommand(UserDto Model) : ICommand;
}
