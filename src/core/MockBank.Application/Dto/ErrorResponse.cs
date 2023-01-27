using System.Collections.Generic;

namespace MockBank.Application.Dto
{
    public class ErrorResponse
    {
        public string response_code { get; set; }
        public string title { get; set; }
        public int http_status { get; set; }
        public string message { get; set; }
        public IList<Errors> fieldErrors { get; set; }
        public Dictionary<string, string> ErrorDict { get; set; }
    }

    public class Errors
    {
        public string objectName { get; set; }
        public string field { get; set; }
        public string message { get; set; }
    }

    public class BerkeleyErrors
    {
        public string code { get; set; }
        public string message { get; set; }
        public string tracking_code { get; set; }
    }
    
    
    
     // {
    // "error": {
    //     "code": "transitory_failure",
    //     "message": "Bad Gateway: Sorry, due to technical difficulties we are unable to process your request at this time. Please try again later.",
    //     "tracking_code": "b03b1c49-ddc6-45ff-b8fb-5697b5e9eb51"
    // }
}