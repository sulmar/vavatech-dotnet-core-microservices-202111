using AuthService.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService.Api.Controllers
{
    public class AuthDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
    
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly Domain.IAuthorizationService authorizationService;
        private readonly ITokenService tokenService;

        public AuthController(Domain.IAuthorizationService authorizationService, ITokenService tokenService)
        {
            this.authorizationService = authorizationService;
            this.tokenService = tokenService;
        }

        
        [AllowAnonymous]
        [Route("token/create")]
        [HttpPost]
        public ActionResult<string> CreateToken([FromBody] AuthDto auth)
        {
            if (authorizationService.TryAuthorize(auth.Login, auth.Password, out User user))
            {
                string token = tokenService.CreateToken(user);
                return Ok(token);
            }

            return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}
