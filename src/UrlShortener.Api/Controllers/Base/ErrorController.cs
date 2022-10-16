namespace UrlShortener.Api.Controllers.Base;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : BaseController
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ErrorController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult HandleError()
    {
        var context = _httpContextAccessor.HttpContext.Features.Get<IExceptionHandlerFeature>();
        var statusCode = StatusCodes.Status500InternalServerError;
        
        var exception = context.Error;
        var errors = default(List<string>);

        if (exception is ConflictException) {
            errors = new List<string> { exception.Message };
            statusCode = StatusCodes.Status409Conflict;
        }
        else if (exception is NotFoundException)
        {
            errors = new List<string> { exception.Message };
            statusCode = StatusCodes.Status404NotFound;
        }
        else if (exception is ValidationException) {
            errors = ((ValidationException)exception).Errors.Select(x => x.ErrorMessage).ToList();
            statusCode = StatusCodes.Status400BadRequest;
        }

        return new ObjectResult(new { errors }) { StatusCode = statusCode };
    }
}
