using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ratting.WepAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController: ControllerBase
    {
        private IMediator m_mediator;
        protected IMediator Mediator => m_mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal Guid UserId => !User.Identity.IsAuthenticated ? 
            Guid.Empty :
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
