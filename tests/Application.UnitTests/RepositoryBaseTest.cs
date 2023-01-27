using System;
using System.Threading.Tasks;
using Application.UnitTests.Dto;
using MockBank.Data;
using MockBank.Domain.Entities.Berkeleys;
using NUnit.Framework;

namespace Application.UnitTests
{
    [TestFixture]
    public class RepositoryBaseTest
    {
        private TestUtility _tu;
        private UnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _tu = new TestUtility();
            _unitOfWork = new UnitOfWork(_tu._configuration);
        }

        [TearDown]
        public void Teardown()
        {
        }

        [Test]
        public async Task RepositoryCreateCardHolderTest()
        {
            
            // STEP: CREATE A CARD HOLDER WITH PROGRAM ID 
            
            // Create Address
            Address MockAddress = _tu.GenerateAddress();
            var addressId = await _unitOfWork.AddressRepository.AddAsync(MockAddress);
            _unitOfWork.Complete();
            
            CardHolder mockUser = _tu.GenerateMockCardHolder(addressId);
            // CardHolder Created
            var cardHolderId = await _unitOfWork.CardholderRepository.AddAsync(mockUser);
            _unitOfWork.Complete();

            var bankId = _tu._faker.Random.Number(1, 7);
            var programId = 2; // virtual card , DAYFORCE program
            
            // Create Account
            Account mockAccount = _tu.GenerateMockAccount(programId, cardHolderId, bankId);
            var accountId = await _unitOfWork.AccountRepository.AddAsync(mockAccount);
            _unitOfWork.Complete();
            
            // Create Credit Card
            Card mockCreditCard = _tu.GenerateCard(accountId);
            var cardId = await _unitOfWork.CardRepository.AddAsync(mockCreditCard);
            _unitOfWork.Complete();

            // STEP ACTIVATE ACCOUNT  (Sending Account ID, last_four_digits, expiry_year, expiry_month)
            
            
           

            // STEP LOAD DOLLAR AMOUNT
            
        }

        [Test]
         public async Task CardActivationWithAccountIdTest()
        {
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(1);
           
        }

        
         [Test]
        public async Task Sy()
        {
       
        // string queryCardHolder =
        //     @"SELECT u.id,u.first_name, u.middle_name, u.last_name, u.date_of_birth, u.emboss_line, u.phone, u.email, u.sin,
        //       a.id, a.address1, a.address2, a.city, a.post_code, a.state, s.id, s.method_name
        //       from berkeley_cardholder as u 
        //       inner join berkeley_address as a on a.id == u.address_id
        //       inner join shipping_method as s on s.id == u.shipping_method_id";
        //
        // var cardHolders = await _connection.QueryAsync<CardHolder, Address, ShippingMethod, CardHolder>(
        //     queryCardHolder, (user, address, shippingMethod) =>
        //     {
        //         user.shipping_address = address;
        //         user.ShippingMethod = shippingMethod;
        //         return user;
        //     });
        // Console.WriteLine("test");
       }
    


    // [Test]
    // public async Task DapperTransactionTest()
    // {
    //     var sMethodId = 1;
    //     var cardHolder = new CardHolderRequestDto
    //     {
    //         first_name = _faker.Person.FirstName,
    //         middle_name = null,
    //         last_name = _faker.Person.LastName,
    //         date_of_brith = _faker.Person.DateOfBirth,
    //         emboss_line = _faker.Person.Random.ToString(),
    //         email = _faker.Person.Email,
    //         sin = _faker.Person.Sin(),
    //         phone = _faker.Person.Phone,
    //         shipping_address = new Address
    //         {
    //             address1 = _faker.Address.StreetAddress(),
    //             address2 = _faker.Address.FullAddress(),
    //             city = _faker.Address.City(),
    //             post_code = _faker.Country().Canada().Place().PostCode,
    //             state = _faker.Address.StateAbbr()
    //         },
    //         shipping_method_id = sMethodId
    //     };
    //     // --> entry point
    //     // transaction steps:
    //     // 1. Create Address with the Address info
    //     // 2. Create Card Holder 
    //     // 3. Create Account with Account information --> Return Account Information {Band Detail: {accountNumber}}
    //     // 4. Load Fund with Amount
    //     // 5. Process Loading Evt with Complete 
    //
    //
    //     _connection.Open();
    //     using (_transaction = _connection.BeginTransaction())
    //     {
    //         try
    //         {
    //             // step 1: get shipping method with shipping method name
    //
    //             // step 2: Create Address with address params
    //             var addressCommand =
    //                 @"INSERT INTO berkeley_address ( address1, address2, city, post_code, state, updated_at)
    //             values (@address1, @address2, @city, @post_code, @state, CURRENT_TIMESTAMP);SELECT last_insert_rowid()";
    //
    //             var addressId =
    //                 await _connection.ExecuteScalarAsync<int>(addressCommand, cardHolder.shipping_address);
    //
    //             var userEntity = new CardHolder
    //             {
    //                 first_name = _faker.Person.FirstName,
    //                 middle_name = null,
    //                 last_name = _faker.Person.LastName,
    //                 date_of_brith = _faker.Person.DateOfBirth,
    //                 emboss_line = _faker.Person.Phone,
    //                 email = _faker.Person.Email,
    //                 sin = _faker.Person.Sin(),
    //                 phone = _faker.Person.Phone,
    //                 shipping_method_id = 1,
    //                 address_id = 1
    //             };
    //
    //             // construct the map data
    //             // step 3: Create User And User Account
    //             var cardHolderCommand =
    //                 @"INSERT INTO berkeley_cardholder 
    //             ( first_name, middle_name, last_name, date_of_birth, emboss_line, phone, email, sin, updated_at, shipping_method_id, address_id) VALUES 
    //             (@first_name, @middle_name, @last_name, date(@date_of_brith), @emboss_line, @phone, @email, @sin, CURRENT_TIMESTAMP, CAST(@shipping_method_id as INT), CAST(@address_id as INT));SELECT last_insert_rowid();";
    //             var userId = await _connection.ExecuteScalarAsync<int>(cardHolderCommand, userEntity);
    //
    //             // step 4: Create Account with Bank information
    //
    //             // step 5: Load Amount with Account
    //
    //
    //             // step 6: return with process number
    //
    //             _transaction.Commit();
    //         }
    //         catch (Exception ext)
    //         {
    //             _transaction.Rollback();
    //         }
    //     }
    // }
    }
}