using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace RealEstate.Api.Validation
{
    public static class ValidationExtensions
    {
        public static IEnumerable<RealEstateValidationError> ToValidationErrors(this ModelStateDictionary modelState)
        {
            return modelState.Keys.SelectMany(key => modelState[key].Errors.Select(x => new RealEstateValidationError(key, x.ErrorMessage)));
        }

        public static IEnumerable<RealEstateValidationError> ToValidationErrors(this AggregateException exception)
        {
            return exception.InnerExceptions.Select(x => x as ValidationException).Select(x => new RealEstateValidationError(x.Value as string, x.Message));
        }
    }
}
