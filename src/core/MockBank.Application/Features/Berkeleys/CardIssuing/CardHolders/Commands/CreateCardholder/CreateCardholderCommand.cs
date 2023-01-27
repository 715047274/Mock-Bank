using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Bogus;
using MediatR;
using MockBank.Application.Common.Helpers;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;
using Address = MockBank.Domain.Entities.Berkeleys.Address;

namespace MockBank.Application.Features.Berkeleys.CardIssuing.CardHolders.Commands.CreateCardholder
{
    public class CreateCardholderCommand : BKCreateCardholder, IRequest<BKCreateAccountResponse>
    {
    }
    public class CreateCardholderCommandHandler : IRequestHandler<CreateCardholderCommand, BKCreateAccountResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCardholderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BKCreateAccountResponse> Handle(CreateCardholderCommand request,
            CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();

            var address = _mapper.Map<Address>(request);
            var tempAddressId = await _unitOfWork.AddressRepository.AddAsync(address);
            var newUser = new CardHolder
            {
                first_name = request.first_name,
                last_name = request.last_name,
                date_of_birth = request.date_of_birth,
                email = request.email,
                phone = request.phone,
                shipping_method_id = int.Parse(request.shipping_method),
                address_id = tempAddressId,
                updated_at = DateTime.Now
            };

            var cardHolderId = await _unitOfWork.CardholderRepository.AddAsync(newUser);
            var mockBankId = new Faker().Random.Number(1, 7);
            var programId = request.program_id; // virtual card , DAYFORCE program

            // Create wrapper to Generate Account with cardholder Id
            var isActiveProgram = await _unitOfWork.ProgramRepository.CheckActiveProgramById(programId);

            var tempAccount = MockHelpers.GenerateMockAccount(programId, cardHolderId, mockBankId);
            var accountId = await _unitOfWork.AccountRepository.AddAsync(tempAccount);
            var mockCreditCard = MockHelpers.GenerateCard(accountId);
            var cardId = await _unitOfWork.CardRepository.AddAsync(mockCreditCard);

            // Steps Create Card Holder with Param Information 
            if (isActiveProgram) // if OnAnyFailure with not commit
                // if Step Success Commit Result
                _unitOfWork.Complete();
            // Assert.Fail("Program is not active");
            var response = new BKCreateAccountResponse();

            var account = await _unitOfWork.AccountRepository.GetByIdAsync(accountId);
            var result = _mapper.Map<BKCreateAccountData>(account);
            result.external_tag = request.external_tag;
            result.primary_processor_reference = Guid.NewGuid().ToString();
            response.data = result;

            return response;
        }
    }
}