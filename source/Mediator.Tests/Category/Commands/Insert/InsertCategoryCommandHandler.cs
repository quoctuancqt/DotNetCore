using DotNetCore.Results;
using System.Threading.Tasks;

namespace DotNetCore.Mediator.Tests
{
    public sealed class InsertCategoryCommandHandler : IHandler<InsertCategoryCommand, long>
    {
        public Task<IResult<long>> HandleAsync(InsertCategoryCommand request)
        {
            return Result<long>.SuccessAsync(1);
        }
    }
}
