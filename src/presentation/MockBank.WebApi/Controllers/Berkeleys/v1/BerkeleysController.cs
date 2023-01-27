using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace MockBank.WebApi.Controllers.Berkeleys.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("berkeleys/v{version:apiVersion}/api")]  // template disable with the Berkeley version
    //[Route("/api")]
    public abstract class BerkeleysController : ControllerBase
    {
         
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}