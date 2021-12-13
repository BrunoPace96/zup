using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZupTeste.DataContracts.Results;

namespace ZupTeste.API.Common.Controllers
{
    [ApiController]
    [Authorize]
    [ProducesResponseType(typeof(ValidationFailedResult), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(MessageResult), StatusCodes.Status401Unauthorized)]
    public abstract class BaseController : ControllerBase {}
}