using DotNetCore.Results;
using FluentValidation;
using System.Threading.Tasks;

namespace DotNetCore.Validation
{
    public static class Extensions
    {
        public static async Task<IResult> ValidationAsync<T>(this IValidator<T> validator, T instance)
        {
            if (instance is null)
            {
                return await Result.FailAsync().ConfigureAwait(false);
            }

            var result = await validator.ValidateAsync(instance).ConfigureAwait(false);

            if (result.IsValid)
            {
                return await Result.SuccessAsync().ConfigureAwait(false);
            }

            return await Result.FailAsync(result.ToString()).ConfigureAwait(false);
        }
    }
}
