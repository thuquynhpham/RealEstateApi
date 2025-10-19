using RealEstate.Api.Handlers._Shared;

namespace RealEstate.Api.Handlers.User.Dtos
{
    public class UserDto: QueryApiResponse<UserDto>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string? JWTToken { get; set; }

        public static UserDto Create(string token)
        {
            return new UserDto
            {
                JWTToken = token
            };
        }
    }
}
