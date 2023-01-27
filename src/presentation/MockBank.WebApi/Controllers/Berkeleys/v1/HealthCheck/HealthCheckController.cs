using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MockBank.Application.Features.Berkeleys.HealthCheck.Queres.GetAppInfo;
using MockBank.Application.IRepository;
using MockBank.Data;

namespace MockBank.WebApi.Controllers.Berkeleys.v1.HealthCheck
{
    public class HealthCheckController : BerkeleysController
    {
        [HttpGet]
        [Route("info")]
        public async Task<AppInfoResponse> Info()
        {
            return await Mediator.Send(new GetAppInfoQuery());
        }
    }
}