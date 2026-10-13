using System.Security.Claims;
using Application.Common.Interfaces;

namespace API.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid? UserId
    {
        get
        {
            var user = httpContextAccessor.HttpContext?.User;
            var value = user?.FindFirstValue(ClaimTypes.NameIdentifier) ?? user?.FindFirstValue("sub");
            return Guid.TryParse(value, out var id) ? id : null;
        }
    }
}
