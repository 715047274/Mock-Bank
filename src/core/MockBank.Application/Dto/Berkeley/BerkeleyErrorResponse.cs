using System.Collections.Generic;

namespace MockBank.Application.Dto.Berkeley
{
    public class BerkeleyErrorResponse
    {
        public BerkeleyError error { get; set; }

    }
    public class BerkeleyError
    {
        public string code { get; set; }
        public List<FieldErrors> field_errors { get; set; }
        public string message { get; set; }
        public string tracking_code { get; set; }
        public IList<KycResultCode> kyc_result_codes { get; set; }
        public string kyc_transaction_id { get; set; }
    }

    public class KycResultCode
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class FieldErrors
    {
        public string code { get; set; }
        public List<string> invalid_elements { get; set; }
        public string message { get; set; }
        public List<string> position { get; set; }
        public string value { get; set; }
    }

}