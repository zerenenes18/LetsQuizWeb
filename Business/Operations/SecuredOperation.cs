using Castle.DynamicProxy;
using Core.Extensions;
using Core.Interceptors;
using Core.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Business.Operations;

public class SecuredOperation : MethodInterception
{
    private string[] _roles;
    private IHttpContextAccessor _httpContextAccessor;

    public SecuredOperation(string roles)
    {
        _roles = roles.Split(',');
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

    }
    protected override void OnBefore(IInvocation invocation)
    {
        var user = _httpContextAccessor.HttpContext.User;
        var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
        var email = _httpContextAccessor.HttpContext.User.ClaimEmail();
        var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
    
        foreach (var role in _roles)
        {
            if (roleClaims.Contains(role))
            {
                return;
            }
        }
        throw new Exception("Error!!");
    }
}