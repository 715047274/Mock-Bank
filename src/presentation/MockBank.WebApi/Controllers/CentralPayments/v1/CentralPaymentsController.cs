using Microsoft.AspNetCore.Mvc;

namespace MockBank.WebApi.Controllers.CentralPayments
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("CentralPayments/v{version:apiVersion}")]
    public abstract class CentralPaymentsController : ControllerBase
    {
    }
    
}