using DotNetCore.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetCore.AspNetCore
{
    public sealed class ApiResult : IActionResult
    {
        private readonly IResult _result;

        private ApiResult(IResult result)
        {
            _result = result;
        }

        private ApiResult(object data)
        {
            _result = Result<object>.Success(data);
        }

        public static IActionResult Create(IResult result)
        {
            return new ApiResult(result);
        }

        public static IActionResult Create(object data)
        {
            return new ApiResult(data);
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            object value = null;

            if (_result.Failed)
            {
                value = _result.Message;
            }

            if (_result.GetType().IsGenericType && _result.GetType().GetGenericTypeDefinition() == typeof(Result<>))
            {
                value = (_result as dynamic)?.Data;
            }

            var objectResult = new ObjectResult(value)
            {
                StatusCode = _result.Succeeded ? StatusCodes.Status200OK : StatusCodes.Status422UnprocessableEntity
            };

            return objectResult.ExecuteResultAsync(context);
        }
    }
}
