using System;
using System.Collections.Generic;

namespace MockBank.Application.Dto.Berkeley
{
    public class BKCardInfoData
    {
        public IList<BKCardInfo> data { get; set; }
    }
    public class BKCardInfo
    {
        public DateTime? activation_date { get; set; }
        public string order_shipping_method { get; set; }
        public string order_status { get; set; }
        public string order_tracking_number { get; set; }
        public DateTime? registration_date { get; set; }
        public DateTime? shipping_date { get; set; }
    }
}