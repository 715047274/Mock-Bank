using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Bogus;
using JetBrains.Annotations;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.Common.Helpers
{
    public static class MockHelpers
    {
        public static int GuidToInt()
        {
            return BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 4);
        }

        public static long GuidToLong()
        {
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 8);
        }

        public static Account GenerateMockAccount(int programId, int cardHolderId, int bankId)
        {
            // var bankId = _faker.Random.Number(1, 7);
            return new Account
            {
                program_id = programId,
                cardholder_id = cardHolderId,
                bank_id = bankId,
                account_number = new Faker().Random.Replace("000######"),
                updated_at = DateTime.Now,
                start_date = DateTime.Now,
                balance = "0.00"
            };
        }

        public static Card GenerateCard(int accountId)
        {
            var _faker = new Faker();
            var shippment = new List<int> {3, 4, 8, 9};
            var shippingMethodId = _faker.PickRandom(shippment);
            return new Card
            {
                account_id = accountId,
                card_number = _faker.Random.Replace("######XXXXXX####"),
                cvv = _faker.Finance.CreditCardCvv(),
                expiry_year = _faker.Date.Future().Year.ToString(),
                expiry_month = _faker.Date.Future().Month.ToString(),
                updated_at = DateTime.Now,
                activation_date = _faker.Date.Future().Date,
                registration_date = _faker.Date.Future().Date,
                order_shipping_method_id = shippingMethodId,
                order_tracking_number = _faker.System.AndroidId(),
                order_status = "COMPLETE",
                shipping_date = _faker.Date.Future().Date
                // Setup the reset of the shipping data for mock
            };
        }

        public static Transaction GenerateValueLoadTransaction(int accountId, int amount, int processorId,
            string externalTag, [CanBeNull] string idempotencyKey)
        {
            // generate Process 
            // Create Process with Message, action Message, delay timeout, 
            // transaction status: Approved
            var loadValueTransactionCodeId = 14; // code_type: PP1, description: VALUE LOAD 
            var currencyCode = "124";
            var merchantId = 1;

            return new Transaction
            {
                account_id = accountId,
                type_id = loadValueTransactionCodeId,
                processor_reference_id = processorId,
                transaction_amount = FormatValue(amount),
                transaction_currency = currencyCode,
                idempotency_key = idempotencyKey,
                external_tag = externalTag,
                merchant_id = merchantId,
                updated_at = DateTime.Now
            };
        }

        public static string FormatValue(int valueAsCents)
        {
            var result = (double) valueAsCents / 100;
            return $"{Convert.ToDecimal(result):#0.00}";
        }

        public static ProcessorEvent GenerateLoadFundEvt(string loadFunMsg, string actionName = "load", string status = "INPROGRESS" )
        {
            // Steps 1 : Create a process Evt with Approved
            // Steps 2 : Update the Evt with Processing
            // Steps 3 : Update the Evt with Complete --> notified Account with Balance , Transcation record
            // Steps 4 : Iteration with Decline , --> notified Account with Balance , Transcation record
            return new ProcessorEvent
            {
                message = loadFunMsg,
                status = status,
                action_name = actionName,
                delay_millisecond = 10000,
                updated_at = DateTime.Now,
                reference_id = Guid.NewGuid().ToString()
            };
        }
        
        public static ProcessorEvent GenerateAccountCreateEvt(string external_tag)
        {
            return new ProcessorEvent
            {
                message = external_tag,
                status = ProcessStatusEnum.Completed.ToString(),
                action_name = "Create Cardholder",
                delay_millisecond = 10000,
                updated_at = DateTime.Now,
                reference_id = Guid.NewGuid().ToString()
            };
        }
    
        
        // Calculate decimal balance 
        public static string AccountBalanceCalculator(string balance,int amount )
        {     double amt = (double) amount / 100;
              var b = Convert.ToDecimal(balance);
              var balanceDecimal = Math.Round(b + (decimal) amt, 2);
            return String.Format("{0:#.##}", balanceDecimal);
        }

        public static Pagination PageFilterHelper (int limit, int page, string startDate, string endDate)
        {
            int offsetConvert = limit * page - limit;
            var endDateString=  DateTime.ParseExact(endDate, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");
            var startDateString=  DateTime.ParseExact(startDate, "dd-MM-yyyy", null).ToString("yyyy-MM-dd");

            return new Pagination
            {
                limit = limit, offset = offsetConvert, startDate = startDateString, endDate = endDateString, page = page
            };
        }
        
        
    }

    public struct Pagination
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int page { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }

}