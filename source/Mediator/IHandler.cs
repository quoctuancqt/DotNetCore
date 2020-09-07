using DotNetCore.Results;
using System.Threading.Tasks;

namespace DotNetCore.Mediator
{
    public interface IHandler<TRequest>
    {
        Task<IResult> HandleAsync(TRequest request);
    }

    public interface IHandler<TRequest, TResponse>
    {
        Task<IResult<TResponse>> HandleAsync(TRequest request);
    }
}
