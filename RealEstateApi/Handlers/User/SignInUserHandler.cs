using Microsoft.IdentityModel.Tokens;
using RealEstate.Api.Handlers._Shared;
using RealEstate.Api.Handlers.User.Dtos;
using RealEstate.Api.Infrastructure.Logging;
using RealEstate.Core;
using RealEstate.Domain.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate.Api.Handlers.User
{
    public class SignInUserHandler(IUnitOfWork unitOfWork, IRequestContextProvider contextProvider, IConfiguration configuration, ILogger<SignInUserHandler> logger) : QueryHandlerBase<SignInUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository = unitOfWork.Users;
        private readonly IRequestContextProvider _contextProvider = contextProvider;
        private readonly ILogger _logger = logger;

        public override async Task<UserDto> Handle(SignInUserQuery request, CancellationToken ct)
        {
            var existingUser = await _userRepository.FindAsync(x => x.Email == request.Model.Email, ct);
            if (existingUser == null)
            {
                _logger.LogMissingUser(className: nameof(SignInUserHandler), methodName: nameof(Handle));
                return UserDto.CreateNotFound("User Email is not existed");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, request.Model.Email)
            };

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signingCredentials
            );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return UserDto.Create(jwtToken);
        }
    }

    public record SignInUserQuery(UserDto Model) : IQuery<UserDto>;
}
