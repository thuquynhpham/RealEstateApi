using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Handlers.Category;
using RealEstate.Api.Handlers.Category.Dtos;
using RealEstate.Core;

namespace RealEstate.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ApiControllerBase
    {
        public CategoriesController(ILogger<CategoriesController> logger, IMediator mediator, IRequestContextProvider requestContextProvider) : base(logger, mediator, requestContextProvider)
        {

        }

        [HttpGet]
        [Route("getAll")]
        [Produces(typeof(CategoriesDto))]
        [Authorize]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> GetCategories(CancellationToken ct)
        {
            return await HandleQueryAsync(new GetCategoriesQuery(), ct);
        }
    }
}
