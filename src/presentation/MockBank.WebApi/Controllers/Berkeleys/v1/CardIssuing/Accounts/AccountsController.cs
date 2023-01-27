using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MockBank.Application.Common.Helpers;
using MockBank.Application.Configurations.Features.Berkeleys.CardIssuing.Accounts.Commands.UpdateActivateCard;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.Features.Berkeleys.CardIssuing.Accounts.Queries.GetAccountBalanceQuery;
using MockBank.Application.Features.Berkeleys.CardIssuing.Accounts.Queries.GetAccountDetails;
using MockBank.Application.Features.Berkeleys.CardIssuing.Accounts.Queries.GetAccountTransactions;

//using MockBank.Application.Dto.Berkeleys.Accounts;

namespace MockBank.WebApi.Controllers.Berkeleys.v1.CardIssuing.Accounts
{
    public class AccountsController : BerkeleysController
    {
        #region Activate Card

        [HttpPost]
        [Route("v1/card_issuing/accounts/activate_card")]
        public async Task<ActionResult> ActivateCard(ActivateCardCommand command)
        {
            var isActivationSuccess = await Mediator.Send(command);
            if (isActivationSuccess) return Ok(new { });

            var respond = new
            {
                error = new
                {
                    code = "already_activated",
                    message = "Card already activated"
                }
            };
            return Ok(respond);
        }

        #endregion


        [HttpGet]
        [Route("v1/card_issuing/accounts/{id}/balance")]
        public async Task<BKAccountBalanceResponse> GetAccountBalance(int id)
        {
            var result = await Mediator.Send(new GetAccountBalanceQuery
            {
                accountId = id
            });
            return result;
        }

        #region Get Account Transactions

        [HttpGet]
        [Route("v1/card_issuing/accounts/{id}/transactions")]
        public async Task<BKTransactionHistoryResponse> GetAccountTransactions(int id, int page,
            int limit, string start_date, string end_date)
        {
            var pageHelper = MockHelpers.PageFilterHelper(limit, page, start_date, end_date);
            var transactionList = await Mediator.Send(new GetAccountTransactionQuery
            {
                id = id, limit = limit, startDate = pageHelper.startDate, endDate = pageHelper.endDate,
                offset = pageHelper.offset
            });
            return new BKTransactionHistoryResponse
                {end_date = end_date, limit = limit, page = page, start_date = start_date, data = transactionList};
        }

        #endregion


        #region Get Account Details

        [HttpGet]
        [Route("v1/card_issuing/accounts/{id}")]
        public async Task<BKAccount> GetAccountDetails(int id)
        {
            // throw new NotImplementedException();
            var result = await Mediator.Send(new GetAccountDetailsQuery
            {
                accountId = id
            });
            return result;
        }

        #endregion


        // [HttpPost]
        // [Route("v1/card_issuing/accounts/{id}/card_order_status")]
        // public ActionResult GetCardOrderStatus(int id)
        // {
        //     throw new NotImplementedException();
        // }

        // [HttpGet]
        // [Route("v1/card_issuing/accounts")]
        // public ActionResult GetAccountByProcessorReference([FromQuery] string processor_reference)
        // {
        //     throw new NotImplementedException();
        // }

        // [HttpPost]
        // [Route("v1/card_issuing/accounts/{id}/")]
        // public ActionResult ModifyAccountStatus([FromBody] object vm)
        // {
        //     throw new NotImplementedException();
        // }

        // [HttpPost]
        // [Route("v1/card_issuing/accounts/{id}/get_pin")]
        // public ActionResult RetrievePin([FromBody] object vm)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // [HttpPost]
        // [Route("v1/card_issuing/accounts/{id}/sensitive_data")]
        // public ActionResult GenerateOneTimeToken(int id)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // [HttpPost]
        // [Route("v1/card_issuing/accounts/sensitive_data")]
        // public ActionResult RetrieveSensitiveData(int id)
        // {
        //     throw new NotImplementedException();
        // }
    }
}