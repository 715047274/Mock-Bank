using System;
using System.Collections.Generic;
using System.IO;
using Bogus;
using Bogus.Extensions.Canada;
using CountryData.Bogus;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using MockBank.Domain.Entities.Berkeleys;

namespace Application.UnitTests
{
    public class TestUtility
    {
        public IConfiguration _configuration;
        public string ProjectDirectory { get; set; }
        public Faker _faker { get; set; }

        public TestUtility()
        {
            var workingDirectory = Environment.CurrentDirectory;
            // BerkeleyApiProjectPath
            var BerkeleyApiProjectPath = Directory.GetParent(workingDirectory).Parent.Parent.Parent.Parent.FullName;
            ProjectDirectory = Path.Combine(BerkeleyApiProjectPath, "src", "presentation", "MockBank.WebApi");
            _configuration = new ConfigurationBuilder()
                .SetBasePath(ProjectDirectory)
                .AddJsonFile(@"appsettings.Test.json", false, true)
                .AddEnvironmentVariables()
                .Build();
            _faker = new Faker();
        }

        #region MockData Generation

        public Address GenerateAddress()
        {
            return new Address
            {
                address1 = _faker.Address.StreetAddress(),
                address2 = _faker.Address.FullAddress(),
                city = _faker.Address.City(),
                postal_code = _faker.Random.Replace("?#? #?#"),
                // state = _faker.Country().Canada().Province().Name,
                // country = _faker.Country().Canada().Name,
                updated_at = DateTime.Now
            };
        }

        public CardHolder GenerateMockCardHolder(int addressId)
        {
            var shippment = new List<int> {3, 4, 8, 9};
            var shippingMethodId = _faker.PickRandom(shippment);
            return new CardHolder
            {
                first_name = _faker.Person.FirstName,
                middle_name = null,
                last_name = _faker.Person.LastName,
                date_of_birth = _faker.Person.DateOfBirth.ToString("MM/dd/yyyy"),
                emboss_line = _faker.Person.Phone,
                email = _faker.Person.Email,
                sin = _faker.Person.Sin(),
                phone = _faker.Person.Phone,
                address_id = addressId,
                shipping_method_id = shippingMethodId,
                updated_at = DateTime.Now
            };
        }

        public Account GenerateMockAccount(int programId, int cardHolderId, int bankId)
        {
            // var bankId = _faker.Random.Number(1, 7);
            return new Account
            {
                program_id = programId,
                cardholder_id = cardHolderId,
                bank_id = bankId,
                account_number = _faker.Random.Replace("000######"),
                updated_at = DateTime.Now,
                start_date = DateTime.Now,
                balance = String.Format("0.00")
            };
        }


        public Card GenerateCard(int accountId)
        {
            var shippingMethodId = _faker.Random.Number(1, 7);
            //card_number = _faker.Random.Replace("######XXXXXX####"),
            return new Card
            {
                account_id = accountId,
                card_number = _faker.Random.Replace("################"),
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

        public Transaction GenerateValueLoadTransaction(int accountId, int amount, int processorId, string externalTag,
             string? idempotencyKey)
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
        

        public ProcessorEvent GenerateLoadFundEvt(string loadFunMsg)
        {
            // Steps 1 : Create a process Evt with Approved
            // Steps 2 : Update the Evt with Processing
            // Steps 3 : Update the Evt with Complete --> notified Account with Balance , Transcation record
            // Steps 4 : Iteration with Decline , --> notified Account with Balance , Transcation record
            return new ProcessorEvent
            {
                message = loadFunMsg,
                status = ProcessStatusEnum.Completed.ToString(),
                action_name = "load",
                delay_millisecond = 10000,
                updated_at = DateTime.Now,
                reference_id = Guid.NewGuid().ToString()
            };
        }
        // $"{Convert.ToDecimal(amount/100):#0.00}"

        public ProcessorEvent GenerateAccountCreateEvt(string external_tag)
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


        public string FormatValue(int valueAsCents)
        {
            var result = (double) valueAsCents / 100;
            return $"{Convert.ToDecimal(result):#0.00}";
        }

        #endregion
    }
}