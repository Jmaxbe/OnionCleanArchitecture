using Application.Identity.Queries.GetToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StaffTimeTableAPI.Controllers;

public class IdentityController : ApiControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthenticateResponseDto), 200)]
    public async Task<ActionResult<AuthenticateResponseDto>> Authenticate([FromBody] AuthenticateResponseQuery query, CancellationToken cancellationToken)
    {
        return await Mediator.Send(query, cancellationToken);
    }
}