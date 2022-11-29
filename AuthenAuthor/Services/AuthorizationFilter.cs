using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuthenAuthor.Services;

public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationFilter(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void OnAuthorization(AuthorizationFilterContext filterContext)
    {
        // if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)  
        //     || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))  
        // {  
        //     // Don't check for authorization as AllowAnonymous filter is applied to the action or controller  
        //     return;  
        // }  
  
        // Check for authorization  
        if (_httpContextAccessor.HttpContext.Session.GetString("username") is null)  
        { 
            // filterContext.Result = new RedirectResult("~/Views/Home/Index");  
            filterContext.Result = new RedirectResult("~/Views/Home/Index");  
        }  
    }
} 