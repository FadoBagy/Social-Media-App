namespace Social_Media_App.Controllers.Api
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Social_Media_App.Infrastructure.Extensions;

    [ApiController]
    [Authorize]
    public class UsersApiController : ControllerBase
    {
        [HttpGet("/api/user/current")]
        public ActionResult<string> GetCurrentUserId()
        {
            var currentUserId = User.Id();

            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized();
            }

            return Ok(currentUserId);
        }
    }
}
