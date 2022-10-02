namespace UM.REST.Infrastructure.Attribute
{
    //public class CheckRoleFilter : IAsyncActionFilter
    //{
    //    private readonly IAuthenticatedUser _authenticatedUser;
    //    private readonly Role[] _roles;

    //    public CheckRoleFilter(Role[] roles, IAuthenticatedUser authenticatedUser)
    //    {
    //        _roles = roles;
    //        _authenticatedUser = authenticatedUser;
    //    }

    //    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    {
    //        bool hasAuthority = false;
    //        foreach (var role in _roles)
    //        {
    //            if (_authenticatedUser.User.Role == role)
    //            {
    //                hasAuthority = true;
    //                break;
    //            }
    //        }

    //        if (hasAuthority)
    //        {
    //            await next();
    //        }
    //        else
    //        {
    //            context.Result = new ObjectResult(BaseResponseCollection.GetBaseResponse(RequestResult.NotAllowThisOperation))
    //            {
    //                StatusCode = 403
    //            };
    //        }
    //    }
    //}
}
