using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.UnitTests.Dto;
using Dapper;
using MockBank.Application.Common.Helpers;
using MockBank.Data;
using MockBank.Domain.Entities.Berkeleys;
using NUnit.Framework;
//using MockBank.Application.Dto.Berkeleys.Accounts;

namespace Application.UnitTests.RepositoryTest
{
    public class RepositoryCommandQueryTest
    {
        private TestUtility _tu;
        private UnitOfWork _unitOfWork;
        private readonly int MOCK_PROGRAM_ID = 211;

        #region test case store

        // default getting the first of the account
        private int _mockAccountId { get; set; } = 1;
        private int _mockCardId { get; set; } = 1;
        private int _mockCardHolderId { get; set; } = 1;

        #endregion


        [SetUp]
        public void Setup()
        {
            _tu = new TestUtility();
            _unitOfWork = new UnitOfWork(_tu._configuration);
        }

        [TearDown]
        public void Teardown()
        {
            _unitOfWork.Dispose();
        }

        [Test]
        public async Task CreateCardHolderCommandTest()
        {
            // STEP: CREATE A CARD HOLDER WITH PROGRAM ID 

            // Create Address
            var MockAddress = _tu.GenerateAddress();
            var addressId = await _unitOfWork.AddressRepository.AddAsync(MockAddress);
            //_unitOfWork.Complete();

            var mockUser = _tu.GenerateMockCardHolder(addressId);
            // CardHolder Created
            var cardHolderId = await _unitOfWork.CardholderRepository.AddAsync(mockUser);

            // _unitOfWork.Complete();
            var bankId = _tu._faker.Random.Number(1, 7);
            var programId = MOCK_PROGRAM_ID; // virtual card , DAYFORCE program
            var isActiveProgram = await _unitOfWork.ProgramRepository.CheckActiveProgramById(programId);
            // Create Account
            var mockAccount = _tu.GenerateMockAccount(programId, cardHolderId, bankId);
            var accountId = await _unitOfWork.AccountRepository.AddAsync(mockAccount);
            // _unitOfWork.Complete();

            // Create Credit Card
            var mockCreditCard = _tu.GenerateCard(accountId);
            var cardId = await _unitOfWork.CardRepository.AddAsync(mockCreditCard);

            // Create Transaction With the record should filter by History Transaction and Value Load Transaction 
            if (isActiveProgram) // if OnAnyFailure with not commit
                // if Step Success Commit Result
                _unitOfWork.Complete();
            // Assert.Fail("Program is not active");

            // update latest Created
            _mockCardHolderId = cardHolderId;
            _mockCardId = cardId;
            _mockAccountId = accountId;

            // TODO: Assert verify the result 

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            //Console.WriteLine(account);
        }

        [Test]
        public async Task GetAccountBalanceById()
        {
            int accountId = 1;
            var account = await _unitOfWork.AccountRepository.GetAccountBalanceById(accountId);
            Console.WriteLine(account);
        }

        [Test]
        public async Task GetAccountDetailsById()
        {   int accountId = 1;
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            Console.WriteLine(account);
        }

        [Test]
        public async Task ActiveCardCommandTest()
        {
            // 1. Steps Get Account Information with Account Id 
            // 2. Get Banks Card with Assertion (last_four_digits, expiry_year, expiry_month, cvv, status_code) 
            // 3. Update Card Status to "ACTIVE"
            var account = await _unitOfWork.AccountRepository.GetByIdAsync(_mockAccountId);
            var mockCard = account.cards.Where(x => x.id == 1 && x.status_code == "not_active").FirstOrDefault();
            if (mockCard != null)
            {
                mockCard.status_code = "active";
                mockCard.activation_date = DateTime.Now;
                mockCard.updated_at = DateTime.Now;
                var isUpdated = await _unitOfWork.CardRepository.UpdateCardStatus(mockCard);
                _unitOfWork.Complete();
                if (isUpdated)
                    Console.WriteLine($"{mockCard.card_number} is Active Now");
                else
                    throw new Exception();
            }
            else
            {
                Console.WriteLine("No Card able to Active");
            }
        }

        [Test]
        public async Task LoadFundTest()
        {
            for (var i = 0; i <= 10; i++)
            {
                // 1. Steps Get Account Information with Account ID 
                // 2. Verify the Card Active Status 
                // 3. Create ProcessEvt with Params 
                // 4. Create Transaction With Data 
                // 5. Update Account Balance, ProcessEvtId 
                var mockLoadValueRequest = new LoadFundRequestDto
                {
                    external_tag = Guid.NewGuid().ToString(),
                    account_id = 2,
                    amount = _tu._faker.Random.Number(1000, 9999),
                    message = _tu._faker.Lorem.Word(),
                    idempotency_key = Guid.NewGuid().ToString()
                };


                var processEvt = _tu.GenerateLoadFundEvt(mockLoadValueRequest.message);
                var processId = await _unitOfWork.ProcessorEventRepository.AddAsync(processEvt);
                // When evt Create if the evt status Approved and Completed 
                // Steps 1: Create Transaction Table 
                // Steps 2: Update Account Table with Balance --> program_id, cardholder_id, processor_reference 
                var processor = await _unitOfWork.ProcessorEventRepository.GetByIdAsync(processId);

                if (processor.status == "Completed")
                {
                    var mockAccount =
                        await _unitOfWork.AccountRepository.GetByIdAsync(mockLoadValueRequest.account_id);
                    mockAccount.balance =
                        MockHelpers.AccountBalanceCalculator(mockAccount.balance, mockLoadValueRequest.amount);
                    mockAccount.processor_reference_id = processor.reference_id;
                    mockAccount.updated_at = DateTime.Now;
                    var isChangedAccount = await _unitOfWork.AccountRepository.UpdateAccountBalance(mockAccount);
                    Console.WriteLine($"{isChangedAccount}--> the account balance is updated");
                }

                var mockTransaction = _tu.GenerateValueLoadTransaction(mockLoadValueRequest.account_id,
                    mockLoadValueRequest.amount, processId,
                    mockLoadValueRequest.external_tag, mockLoadValueRequest.idempotency_key);
                var mockTransactionId = _unitOfWork.TransactionRepository.AddAsync(mockTransaction);
                _unitOfWork.Complete();
            }
            //     Console.WriteLine(processId);
        }

        [Test]
        public async Task ValueloadFund()
        {
            // Case 1: amt >=1410 && amt <=1550 --> Payment Network has failed to process transaction 503 not Transaction
            // Case 2: amt >=1551 && amt <=1590 --> Payment Network has failed to process transaction with successful Transaction delay 104000 millseconds
            // with value 

            var mockLoadValueRequest = new LoadFundRequestDto
            {
                external_tag = Guid.NewGuid().ToString(),
                account_id = 1,
                amount = _tu._faker.Random.Number(1000, 9999),
                message = _tu._faker.Lorem.Word(),
                idempotency_key = Guid.NewGuid().ToString()
            };

            // start begin Event
            var processEvt = MockHelpers.GenerateLoadFundEvt(mockLoadValueRequest.message);
            var processId = await _unitOfWork.ProcessorEventRepository.AddAsync(processEvt);
            // When evt Create if the evt status Approved and Completed 
            // Steps 1: Create Transaction Table 
            // Steps 2: Update Account Table with Balance --> program_id, cardholder_id, processor_reference 
            var processor = await _unitOfWork.ProcessorEventRepository.GetByIdAsync(processId);
            Console.WriteLine(processor.status);
            var mockTransaction = _tu.GenerateValueLoadTransaction(mockLoadValueRequest.account_id,
                mockLoadValueRequest.amount, processId,
                mockLoadValueRequest.external_tag, mockLoadValueRequest.idempotency_key);
            var mockTransactionId = _unitOfWork.TransactionRepository.AddAsync(mockTransaction);
            _unitOfWork.Complete();
            _ = Task.Run(async () =>
            {
                Console.WriteLine("------------------>>");
                await Task.Delay(1000);
                try
                {
                    //await Task.Delay(1000);
                    var isChanged = await _unitOfWork.ProcessorEventRepository.UpdateEvtStatus(processId, "FINISHED");
                    Console.WriteLine("------------" + isChanged);
                    var mockAccount =
                        await _unitOfWork.AccountRepository.GetByIdAsync(mockLoadValueRequest.account_id);
                    mockAccount.balance =
                        MockHelpers.AccountBalanceCalculator(mockAccount.balance, mockLoadValueRequest.amount);
                    mockAccount.processor_reference_id = processor.reference_id;
                    mockAccount.updated_at = DateTime.Now;
                    var isChangedAccount = await _unitOfWork.AccountRepository.UpdateAccountBalance(mockAccount);
                    Console.WriteLine($"{isChangedAccount}--> the account balance is updated");
                    _unitOfWork.Complete();
                    var processor3= await _unitOfWork.ProcessorEventRepository.GetByIdAsync(processId);
                    Console.WriteLine(processor3.status);
                }
                catch (Exception er)
                {
                    Console.WriteLine(er);
                }

            });
            var processor2 = await _unitOfWork.ProcessorEventRepository.GetByIdAsync(processId);
            Console.WriteLine(processor2.status);
            // var processEvt = MockHelpers.GenerateLoadFundEvt(mockLoadValueRequest.message, "INPROGRESS", "load");
            // var isChanged = await _unitOfWork.ProcessorEventRepository.UpdateEvtStatus(26, "PENDING");
            // _unitOfWork.Complete();
        }
 

        [Test]
        public async Task GetTransactionByIdTest()
        {
            var mockTransactionId = 1;
            var t = await _unitOfWork.TransactionRepository.GetByIdAsync(mockTransactionId);
            Console.WriteLine(t.id);
        }

        [Test]
        public async Task GetAccountTransactions()
        {
            var transactionParams = new
            {
                accountId = 2,
                startDate = "1980-01-01",
                endDate = "2022-06-26",
                limit = 100,
                page =1,
            };
            
            int  offset =(transactionParams.limit * transactionParams.page) - transactionParams.limit;
                
            var transcationList = await _unitOfWork.TransactionRepository.GetTransactionsByAccountId(
                transactionParams.accountId, transactionParams.startDate, transactionParams.endDate,
                transactionParams.limit, offset);
            Console.WriteLine(transcationList);
        }

        [Test]
        public async Task GetListValueLoadsTest()
        {
            var programParams = new
            {
                programId = 211,
                externalTag = "batch-1",
                limit=50, 
                offset=0
            };
            var transactionList = await _unitOfWork.TransactionRepository.GetListValueLoadsByProgramId(
                programParams.programId, programParams.externalTag, programParams.limit, programParams.offset);
            Console.WriteLine(transactionList);
        }

        
        [Test]
        public async Task GetListValueLoadsCountTest()
        {
            var programParams = new
            {
                programId = 211,
                externalTag = "batch-1",
                limit=50, 
                offset=0
            };
            var transactionList = await _unitOfWork.TransactionRepository.GetListValueLoadsCountByProgramId(
                programParams.programId, programParams.externalTag);
            Console.WriteLine(transactionList);
        }
        
        [Test]
        public void DateTimeConvertTest()
        {
            var datetime = Convert.ToDateTime("26-01-1980");
            Console.WriteLine(datetime.ToString("yyyy-MM-dd"));
            var datetime2 = Convert.ToDateTime("26-01-1980").ToString("yyyy-MM-dd");
            Console.WriteLine(datetime2);

        }


        [Test]
        public async Task demo()
        {
            var UpdateCardStatusByIdCommand =
                @"UPDATE berkeley_card SET status_code=@status_code, activation_date=date(@activation_date) WHERE id = @id";

            var changedRowNum = await _unitOfWork._connection.ExecuteAsync(UpdateCardStatusByIdCommand, new
            {
                status_code = "not_active", activation_date = DateTime.Now, id = 1
            });
            Console.WriteLine($"{changedRowNum}------->");
            // string UpdateCardStatusByIdCommand = @"UPDATE berkeley_card SET status_code=@status_code WHERE id = @id";
            // var num = await _unitOfWork._connection.ExecuteScalarAsync<int>(UpdateCardStatusByIdCommand, new {
            // status_code= "ACTIVE", id = 1});
            _unitOfWork.Complete();
            Console.WriteLine(changedRowNum);
        }
    }
}