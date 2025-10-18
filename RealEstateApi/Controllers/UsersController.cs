//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using RealEstate.Domain.DBI;
//using RealEstate.Domain.Models;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace RealEstate.Api.Controllers
//{
//    [Route("api/users")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        RealEstateDbContext _dbContext = new RealEstateDbContext();
//        IConfiguration _configuration;

//        public UsersController(IConfiguration configuration)
//        {
//            _configuration = configuration;
//        }

//        [HttpPost("[action]")]
//        public IActionResult Register([FromBody] User user)
//        {
//            var existedUser = _dbContext.Users.FirstOrDefault(x => x.Email == user.Email);
//            if (existedUser != null)
//            {
//                return BadRequest("User with the same email is already exist");
//            }

//            _dbContext.Users.Add(user);
//            _dbContext.SaveChanges();
//            return StatusCode(StatusCodes.Status201Created);
//        }

//        [HttpPost("[action]")]
//        public IActionResult Login([FromBody] User user)
//        {
//            var currentUser = _dbContext.Users.FirstOrDefault(x => x.Email == user.Email);

//            if (currentUser == null)
//            {
//                return NotFound("User email is not existed");
//            }

//            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
//            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var claims = new[]
//            {
//                new Claim(ClaimTypes.Email, user.Email)
//            };

//            var token = new JwtSecurityToken(
//                issuer: _configuration["JWT:Issuer"],
//                audience: _configuration["JWT:Audience"],
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(60),
//                signingCredentials: signingCredentials
//                );
//            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

//            return Ok(jwtToken);
//        }
//    }
//}
