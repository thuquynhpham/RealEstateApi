#nullable enable
using RealEstate.Api.Validation;
using System.Text.Json.Serialization;

namespace RealEstate.Api.Handlers._Shared
{
    public interface IApiResponse
    {
        bool NotFound { get; }
        bool ValidationFailed { get; }
        bool Unauthorized { get; }
        string? Message { get; }
        bool Forbidden { get; }
        bool MissingAccess { get; }
        bool ServerError { get; }
        IEnumerable<RealEstateValidationError> ToValidationErrors();
    }

    public abstract class ApiResponse<T> : IApiResponse where T : ApiResponse<T>, new()
    {
        [JsonIgnore]
        public bool NotFound { get; private set; }
        [JsonIgnore]
        public bool ValidationFailed { get; private set; }
        [JsonIgnore]
        public bool Unauthorized { get; private set; }
        [JsonIgnore]
        public bool MissingAccess { get; private set; }
        [JsonIgnore]
        public bool ServerError { get; private set; }
        [JsonIgnore]
        public bool Forbidden { get; private set; }
        [JsonIgnore]
        public string? Message { get; private set; }
        [JsonIgnore]
        public RealEstateValidationError? ValidationError { get; private set; }

        public static T CreateNotFound(string message) { return new T { NotFound = true, Message = message }; }

        public static T CreateForbidden(string message) { return new T { Forbidden = true, Message = message }; }

        public static T CreateUnauthorized(string message) { return new T { Unauthorized = true, Message = message }; }
        public static T CreateValidationFailed(string message) { return new T { ValidationFailed = true, Message = message }; }
        public static T CreateValidationError(string name, string error, int? key = null)
        {
            return new T { ValidationFailed = true, ValidationError = new RealEstateValidationError(name, error, key) };
        }
        public static T CreateMissingAccess(string message) { return new T { MissingAccess = true, Message = message }; }
        public static T CreateServerError(string message) { return new T { ServerError = true, Message = message }; }
        public IEnumerable<RealEstateValidationError> ToValidationErrors()
        {
            return new List<RealEstateValidationError> { ValidationError! };
        }
    }

    public class QueryApiResponse<T> : ApiResponse<T> where T : ApiResponse<T>, new()
    {
    }

    public class CommandApiResponse : ApiResponse<CommandApiResponse>
    {
        public int? Id { get; set; }
        public static CommandApiResponse Success() { return new CommandApiResponse(); }
        public static CommandApiResponse Success(int id) { return new CommandApiResponse { Id = id }; }
    }

}
