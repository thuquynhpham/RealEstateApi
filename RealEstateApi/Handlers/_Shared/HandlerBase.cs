using MediatR;

namespace RealEstate.Api.Handlers._Shared
{
    public abstract class CommandHandlerBase<T>: IRequestHandler<T, CommandApiResponse> where T : ICommand
    {
        public abstract Task<CommandApiResponse> Handle(T command, CancellationToken ct);
    }

    public abstract class QueryHandlerBase<T, TResponse> : IRequestHandler<T, TResponse> where T : IQuery<TResponse> where TResponse: QueryApiResponse<TResponse>, new()
    {
        public abstract Task<TResponse> Handle(T request, CancellationToken ct);
    }
}
