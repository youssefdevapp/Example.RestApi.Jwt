using Asp.Versioning;
using System.Threading.Tasks;
using System.Web.Http;
using Users.Application.Interfaces;
using Users.Core;
using Users.Core.Common;
using Users.WebApi.Handlers;

namespace Users.WebApi.Controllers
{
    [ApiVersion(1.0)]
    [RoutePrefix("api/v{version:apiVersion}/auth")]
    public class AuthController : ApiController
    {
        private readonly IUsersService usersService;
        private readonly TokenCreationHandler tokenHandler;

        public AuthController(IUsersService userInfoService, TokenCreationHandler tokenHandler)
        {
            this.usersService = userInfoService;
            this.tokenHandler = tokenHandler;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route]
        public Token Login([FromBody] UserLogin userLogin)
        {
            User user = usersService.GetByEmail(userLogin.Email);
            if (user != null)
            {
                var token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenEndDate = token.Expiration.AddMinutes(3);

                usersService.Update(user);

                return token;
            }
            return new Token();
        }
    }
}