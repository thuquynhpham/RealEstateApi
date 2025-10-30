using MediatR;

namespace RealEstate.Api.Handlers._Shared
{
    //public class DataContext
    //{

    //}

    //public interface IDataContextRequest
    //{
    //    public DataContext DataContext { get; }
    //}

    public interface IQuery<T>: IRequest<T> where T : QueryApiResponse<T>, new() { }

    public interface ICommand: IRequest<CommandApiResponse> { }
    //public interface  IContextQuery<T> : IQuery<T>, IDataContextRequest where T: QueryApiResponse<T>, new(){}

    //public interface IContextCommand : ICommand, IDataContextRequest { }
}
