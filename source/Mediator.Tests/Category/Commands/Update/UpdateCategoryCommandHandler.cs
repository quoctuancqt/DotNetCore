using DotNetCore.Results;
using System.Threading.Tasks;

namespace DotNetCore.Mediator.Tests
{
    public sealed class UpdateCategoryCommandHandler : IHandler<UpdateCategoryCommand>
    {
        private readonly IMediator _mediator;

        public UpdateCategoryCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IResult> HandleAsync(UpdateCategoryCommand request)
        {
            var category = new Category(request.Id, request.Name);

            var categoryUpdatedEvent = new CategoryUpdatedEvent(category);

            await _mediator.HandleAsync(categoryUpdatedEvent).ConfigureAwait(false);

            return await Result.SuccessAsync().ConfigureAwait(false);
        }
    }
}
