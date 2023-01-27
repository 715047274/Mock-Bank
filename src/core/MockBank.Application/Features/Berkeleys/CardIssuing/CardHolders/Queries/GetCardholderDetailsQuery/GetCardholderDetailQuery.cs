using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.IRepository;

namespace MockBank.Application.Features.Berkeleys.CardIssuing.CardHolders.Queries.GetCardholderDetailsQuery
{
    public class GetCardholderDetailQuery : IRequest<BKAccountHolder>
    {
        public int CardholderId { get; set; }
    }

    public class GetCardholderDetailQueryHandler : IRequestHandler<GetCardholderDetailQuery, BKAccountHolder>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetCardholderDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BKAccountHolder> Handle(GetCardholderDetailQuery request, CancellationToken cancellationToken)
        {
            var CardHolderDetail = await _unitOfWork.CardholderRepository.GetByIdAsync(request.CardholderId);
            CardHolderDetail.accounts = await _unitOfWork.AccountRepository.QueryAccountByCardHolderId(request.CardholderId);
            var detail = _mapper.Map<AccountHolderData>(CardHolderDetail);
            // detail.accounts = _mapper.Map<List<BKCardholderAccount>>(accounts);

            
            return new BKAccountHolder
            {
                data=detail
            };
        }
    }
}