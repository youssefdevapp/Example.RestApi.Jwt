using Asp.Versioning;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Users.Application.Interfaces;
using Users.Core;

namespace Users.WebApi.Controllers
{
    [Authorize]
    [ApiVersion(1.0)]
    [RoutePrefix("api/v{version:apiVersion}/users")]
    public class UsersController : ApiController
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            this._usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [HttpGet]
        [Route]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _usersService.GetAll());
        }

        [HttpGet]
        [Route("{userId:long}")]
        public HttpResponseMessage Get([FromUri] long userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _usersService.GetById(userId));
        }

        [HttpPost]
        public HttpResponseMessage CreateUser([FromBody] User user)
        {
            return Request.CreateResponse(HttpStatusCode.Created, _usersService.Create(user));
        }

        [HttpPut]
        [Route("{userId:long}")]
        public HttpResponseMessage UpdateUser([FromUri] long userId, [FromBody] User user)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _usersService.Update(user));
        }

        [HttpDelete]
        [Route("{userId:long}")]
        public HttpResponseMessage DeleteUser([FromUri] long userId)
        {
            if (_usersService.Delete(userId))
            {
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }
    }
}