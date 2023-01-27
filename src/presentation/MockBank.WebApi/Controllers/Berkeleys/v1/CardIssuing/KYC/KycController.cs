using System;
using Microsoft.AspNetCore.Mvc;
 

namespace MockBank.WebApi.Controllers.Berkeleys.v1.CardIssuing.KYC
{
    public class KycController : BerkeleysController
    {
        [HttpGet]
        [Route("v1/card_issuing/kyc_results")]
        public ActionResult GetListsKycResult()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        [Route("v1/card_issuing/kyc_results/{id}")]
        public ActionResult GetKycResultById(int id )
        {
            throw new NotImplementedException();
        }
        
        [HttpPost]
        [Route("v1/card_issuing/kyc_results")]
        public ActionResult GetKycResultByTransactionId([FromBody] Object vm )
        {
            throw new NotImplementedException();
        }
    }
}