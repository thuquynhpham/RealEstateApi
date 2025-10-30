using Couchbase.Core.Compatibility;

namespace RealEstate.Api.Infrastructure.Logging
{
    public static partial class UserLogger
    {
        [LoggerMessage(
            EventId = (int)LogClasses.User + 0,
            Level = LogLevel.Error,
            Message = "[{ClassName}.{MethodName}]: No user available")]
        public static partial void LogMissingUser(this ILogger logger, string? className, string? methodName);
    }
}
