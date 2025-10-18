using MediatR;

namespace RealEstate.Api.Handlers._Shared
{
    public abstract class CommandHandlerBase<TCommand>: IRequestHandler<TCommand, CommandApiResponse> where TCommand : ICommand
    {
        public abstract Task<CommandApiResponse> Handle(TCommand command, CancellationToken ct);
    }

    public abstract class QueryHandlerBase<TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse> where TResponse: QueryApiResponse<TResponse>, new()
    {
        public abstract Task<TResponse> Handle(TQuery request, CancellationToken ct);
    }
}
