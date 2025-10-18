//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using RealEstate.Domain.DBI;
//using RealEstate.Domain.Models;
//using System.Security.Claims;

//namespace RealEstate.Api.Controllers
//{
//    [Route("api/properties")]
//    [ApiController]
//    public class PropertiesController : ControllerBase
//    {
//        private readonly RealEstateDbContext _dbContext = new();

//        [HttpGet("property-list")]
//        [Authorize]
//        public IActionResult GetProperties(int categoryId)
//        {
//            var properties = _dbContext.Properties.Where(x => x.CategoryId == categoryId);
//            if(properties == null)
//            {
//                return NotFound();
//            }

//            return Ok(properties);
//        }

//        [HttpGet("trending-properties")]
//        [Authorize]
//        public IActionResult GetTrendingProperties()
//        {
//            var properties = _dbContext.Properties.Where(x => x.IsTrending);
//            if (properties == null)
//            {
//                return NotFound();
//            }

//            return Ok(properties);
//        }

//        [HttpGet("search-properties")]
//        [Authorize]
//        public IActionResult GetSearchProperties(string address)
//        {
//            var properties = _dbContext.Properties.Where(x => x.Address.Contains(address));
//            if (properties == null)
//            {
//                return NotFound();
//            }

//            return Ok(properties);
//        }

//        [HttpGet("property-detail")]
//        [Authorize]
//        public IActionResult GetProperty(int propertyId)
//        {
//            var property = _dbContext.Properties.Where(x => x.PropertyId == propertyId);
//            if (property == null)
//            {
//                return NotFound();
//            }

//            return Ok(property);
//        }

//        [HttpGet]
//        [Authorize]
//        public IActionResult GetPropertiesPaging(int pageNumber, int pageSize)
//        {
//            var properties = _dbContext.Properties;

//            return Ok(properties.Skip((pageNumber - 1) * pageSize).Take(pageSize));
//        }

//        [HttpPost]
//        [Authorize]
//        public IActionResult Post([FromBody] Property property)
//        {
//            if (property == null)
//                return NoContent();

//            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
//            var user = _dbContext.Users.FirstOrDefault(x => x.Email == userEmail);
//            if (user == null)
//                return NotFound("User is not existed");

//            property.IsTrending = false;
//            property.UserId = user.UserId;

//            _dbContext.Properties.Add(property);
//            _dbContext.SaveChanges();

//            return StatusCode(StatusCodes.Status201Created);
//        }

//        [HttpPut("{id}")]
//        [Authorize]
//        public IActionResult Put(int id, [FromBody] Property updatingProperty)
//        {
//            var property = _dbContext.Properties.FirstOrDefault(x => x.PropertyId == id);
//            if (property == null)
//                return NotFound("Property is not existed");

//            if (updatingProperty == null)
//                return NoContent();

//            // userNameIdentifier work for Auth0
//            // var userNameIdentifier = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
//            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
//            var user = _dbContext.Users.FirstOrDefault(x => x.Email == userEmail);
//            if (user == null)
//                return NotFound("User is not exist");

//            if(property.UserId == user.UserId)
//            {
//                property.Name = updatingProperty.Name;
//                property.Detail = updatingProperty.Detail;
//                property.Address = updatingProperty.Address;
//                property.Price = updatingProperty.Price;
//                property.IsTrending = updatingProperty.IsTrending;
//                property.ImageUrl = updatingProperty.ImageUrl;
//                property.CategoryId = updatingProperty.CategoryId;
//                property.UserId = user.UserId;

//                _dbContext.SaveChanges();

//                return Ok("Record updated successfully");
//            }
//            else
//            {
//                return BadRequest("User is not allowed to update record");
//            }
            
//        }

//        [HttpDelete("{id}")]
//        [Authorize]
//        public IActionResult Delete(int id)
//        {
//            var property = _dbContext.Properties.FirstOrDefault(x => x.PropertyId == id);
//            if (property == null)
//                return NotFound("Property is not existed");

//            _dbContext.Properties.Remove(property);
//            _dbContext.SaveChanges();

//            return Ok("Record deleted successfully");
//        }
//    }
//}
