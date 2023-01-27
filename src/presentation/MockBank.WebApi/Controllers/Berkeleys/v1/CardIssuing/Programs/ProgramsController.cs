using System;
using Microsoft.AspNetCore.Mvc;

namespace MockBank.WebApi.Controllers.Berkeleys.v1.CardIssuing.Programs
{
    public class ProgramsController : BerkeleysController
    {
        [HttpGet]
        [Route("v1/card_issuing/programs/{id}")]    
        public ActionResult GetProgramsById(int id)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        [Route("v1/card_issuing/programs")]    
        public ActionResult GetListPrgrams()
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        [Route("v1/card_issuing/programs/{id}/balance")]    
        public ActionResult GetProgramBalance()
        {
            throw new NotImplementedException();
        }
    }
}