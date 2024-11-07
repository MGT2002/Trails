using BlazingTrails.Api.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#if DEBUG

namespace BlazingTrails.Api.Tests
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly BlazingTrailsContext dbContext;

        public TestController(BlazingTrailsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // Simple response to confirm the API is working
            return Ok("API is working!");
        }

        [HttpGet(nameof(DeleteDbData))]
        public IActionResult DeleteDbData()
        {
            dbContext.RemoveRange(dbContext.Waypoints);
            dbContext.RemoveRange(dbContext.Trails);
            dbContext.SaveChanges();

            return Ok("Done!");
        }

        // GET: api/test/userinfo
        [HttpGet("userinfo")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            // Retrieve the user's email from claims
            var emailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "email");
            var nameClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "name");

            if (emailClaim != null)
            {
                return Ok(new
                {
                    Email = emailClaim.Value,
                    Name = nameClaim?.Value
                });
            }

            return NotFound("User email not found.");
        }

        [HttpGet(nameof(GetClaims))]
        [Authorize]
        public IActionResult GetClaims()
        {
            var claims = HttpContext.User.Claims
                .Select(c => $"{c.Type} -> {c.Value}")
                .ToList();
            var res = string.Join(",\n", claims);

            return Ok(res);
        }
    }
}

#endif
