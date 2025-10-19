using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Api.Handlers._Shared;
using RealEstate.Api.Validation;
using RealEstate.Core;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Api.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        private readonly IRequestContextProvider _requestContextProvider;

        protected ApiControllerBase(ILogger logger, IMediator mediator, IRequestContextProvider requestContextProvider)
        {
            _logger = logger;
            _mediator = mediator;
            _requestContextProvider = requestContextProvider;
        }

        protected async Task<IActionResult> HandleQueryAsync<T>(IRequest<T> request, CancellationToken ct) where T: ApiResponse<T>, new()
        {
            return await HandleRequest(request, result => Ok(result), ct);
        }

        protected async Task<IActionResult> HandleCommandAsync<T>(IRequest<T> request, CancellationToken ct) where T : ApiResponse<T>, new()
        {
            return await HandleRequest(request, result => Ok(result), ct);
        }

        private async Task<IActionResult> HandleRequest<T>(IRequest<T> request, Func<T, IActionResult> successRequestFunc, CancellationToken ct) where T : IApiResponse
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState.ToValidationErrors());
                }

                var result = await _mediator.Send(request, ct);

                if (result.ServerError)
                    return StatusCode(StatusCodes.Status500InternalServerError, result.Message);

                if (result.NotFound) 
                    return NotFound(result.Message);

                if (result.ValidationFailed)
                    return BadRequest(result.ToValidationErrors());

                if(result.Unauthorized)
                    return Unauthorized(result.Message);

                if (result.Forbidden)
                    return Forbid();

                if (result.MissingAccess)
                    return StatusCode(StatusCodes.Status451UnavailableForLegalReasons);

                return successRequestFunc(result);
            }
            catch (Exception ex)
            {
                if(ex is AggregateException validationException && ((AggregateException)ex).InnerExceptions.All(ie => ie is ValidationException))
                {
                    return BadRequest(validationException.ToValidationErrors());
                }
                if(ex is OperationCanceledException)
                {
                    return StatusCode(StatusCodes.Status204NoContent, ex.Message);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            //void SetHttpContextData()
            //{
            //    if(request is IDataContextRequest dataContextRequest)
            //    {
            //        _requestContextProvider
            //    }
            //}
        }

        protected DataContext GetDataContext()
        {
            return new DataContext();
        }
    }
}
