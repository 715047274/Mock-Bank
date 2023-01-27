using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.IRepository;

namespace MockBank.Application.Features.Berkeleys.CardIssuing.ValueLoads.Queries.ListValueLoads
{
    
    // retry syndication use for GW 
    
    public class GetListValueLoadsQuery :IRequest<BKListValueLoadsResponse>
    {
        public int program_id { get; set; }
        public string external_tag { get; set; }
        public int limit { get; set; } = 50;
        public int offset { get; set; } = 0;
    }

    public class GetListValueLoadsQueryHandler :IRequestHandler<GetListValueLoadsQuery, BKListValueLoadsResponse>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public GetListValueLoadsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<BKListValueLoadsResponse> Handle(GetListValueLoadsQuery request, CancellationToken cancellationToken)
        {
            
            var transactionList = await _unitOfWork.TransactionRepository.GetListValueLoadsByProgramId(request.program_id,
               request.external_tag, request.limit, request.offset);
            var count = await _unitOfWork.TransactionRepository.GetListValueLoadsCountByProgramId(request.program_id,
                request.external_tag);
           var resultSet = _mapper.Map<List<BKListValueLoadTransaction>>(transactionList);
           return   new BKListValueLoadsResponse { count= count, data= resultSet, limit = request.limit, offset = request.offset};
        }
    }
}