using System.Collections.Generic;

namespace MockBank.Application.Dto
{
    public class ResponseStatus
    {
        public string Message { get; set; }
        public IList<string> Errors { get; set; }
        public Dictionary<string, string> ErrorDict { get; set; }
        public int StatusCode { get; set; }
    }
}