using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.Features.Berkeleys.CardIssuing.CardHolders.Commands.CreateCardholder;
using MockBank.Application.Features.Berkeleys.CardIssuing.CardHolders.Queries.GetCardholderDetailsQuery;

namespace MockBank.WebApi.Controllers.Berkeleys.v1.CardIssuing.Cardholders
{
    public class CardHoldersController : BerkeleysController
    {
        #region create new user account

        [HttpPost]
        [Route("v1/card_issuing/cardholders")]
        public async Task<ActionResult<BKCreateAccountResponse>> Create(CreateCardholderCommand command)
        {
            var result = await Mediator.Send(command);
            return result;
        }

        #endregion

        #region Get cardholder details

        [HttpGet]
        [Route("v1/card_issuing/cardholders/{id}")]
        public async Task<BKAccountHolder> GetCardHolderDetails( int id)
        {
            // CardHolderDetailListRepondDto cardHolderDetailsDict = new CardHolderDetailListRepondDto(id);
            BKAccountHolder cardholderDetail = await Mediator.Send(new GetCardholderDetailQuery { CardholderId = id });
            return cardholderDetail;
        }

        #endregion

        #region  Update Cardholder

        [HttpPost]
        [Route("v1/card_issuing/cardholders/{id}")]
        public ActionResult UpdateCardHolders([FromBody] object cardHolderVm)
        {
            throw new NotImplementedException();
        }

        #endregion
       
        
    }
}