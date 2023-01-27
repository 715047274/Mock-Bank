using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MockBank.Application.Dto.Berkeley;
using MockBank.Application.IRepository;

namespace MockBank.Application.Features.Berkeleys.CardIssuing.ValueLoads.Commands.CreateValueLoad
{
    public class UpdateEvtStatusCommand: IRequest<int>
    {
        public int id { get; set; }
    }

    class UpdateEvtStatusCommandHandler : IRequestHandler<UpdateEvtStatusCommand, int>
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public UpdateEvtStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<int> Handle(UpdateEvtStatusCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(3000); 
            var result = await _unitOfWork.ProcessorEventRepository.UpdateEvtStatus(request.id, "FINISHED");
           return (result) ? 1 : 0;
        }
    }
}