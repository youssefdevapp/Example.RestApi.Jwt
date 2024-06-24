using Asp.Versioning;
using System.Web.Http;

namespace Users.WebApi.Controllers
{
    /// <summary>
    /// Health Check controller
    /// </summary>
    [ApiVersion(1.0)]
    [RoutePrefix("api/v{version:apiVersion}/health/check")]
    public class HealthController : ApiController
    {
        [Route]
        public IHttpActionResult Get(ApiVersion apiVersion) =>
            Ok(new { controller = GetType().Name, version = apiVersion.ToString() });
    }
}