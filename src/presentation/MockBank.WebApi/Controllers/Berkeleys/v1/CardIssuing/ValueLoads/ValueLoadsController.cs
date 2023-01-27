

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.Features.Berkeleys.CardIssuing.ValueLoads.Commands.CreateValueLoad;
using MockBank.Application.Features.Berkeleys.CardIssuing.ValueLoads.Queries.GetValueLoadDetails;
using MockBank.Application.Features.Berkeleys.CardIssuing.ValueLoads.Queries.ListValueLoads;

namespace MockBank.WebApi.Controllers.Berkeleys.v1.CardIssuing.ValueLoads
{
    public class ValueLoadsController : BerkeleysController
    {
        [HttpGet]
        [Route("v1/card_issuing/value_loads/{id}")]
        public async Task<BKLoadFundsResponse> GetValueLoadDetails(int id)
        {
            // throw new NotImplementedException();
            return await Mediator.Send(new GetValueLoadDetailQuery {transactionId = id});
        }

        [HttpGet]
        [Route("v1/card_issuing/value_loads")] // syc for transaction history
        public async Task<BKListValueLoadsResponse> GetListValueLoad([FromQuery]int program_id, [FromQuery]string external_tag, [FromQuery]int limit,
            [FromQuery]int offset)
        {
             var resultData = await Mediator.Send(new GetListValueLoadsQuery
                 {program_id = program_id, external_tag = external_tag, limit = limit, offset = offset});
             return resultData;
        }

        [HttpPost]
        [Route("v1/card_issuing/value_loads/load")]
        public async Task<BKLoadFundsResponse> CreateValueLoad(CreateValueLoadCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpPost]
        [Route("v1/card_issuing/value_loads/unload")]
        public ActionResult CreateValueUnLoad([FromBody] object pageObject)
        {
            throw new NotImplementedException();
        }
    }
}