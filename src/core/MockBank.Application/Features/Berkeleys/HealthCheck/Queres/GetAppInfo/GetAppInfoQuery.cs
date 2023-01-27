using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MockBank.Application.IRepository;
using MockBank.Domain.Entities.Berkeleys;

namespace MockBank.Application.Features.Berkeleys.HealthCheck.Queres.GetAppInfo
{
    public class GetAppInfoQuery : IRequest<AppInfoResponse>
    {
    }

    public class GetAppInfoQueryHandler : IRequestHandler<GetAppInfoQuery, AppInfoResponse>
    {
        // private readonly IUnitOfWork _unitOfWork;
        //
        // public GetAppInfoQueryHandler(IUnitOfWork unitOfWork)
        // {
        //     _unitOfWork = unitOfWork;
        // }

        public async Task<AppInfoResponse> Handle(GetAppInfoQuery request, CancellationToken cancellationToken)
        {
            // var result = await _unitOfWork.AddressRepository.AddAsync(new Address());
            // _unitOfWork.Complete();
            // var reAddress = await _unitOfWork.AddressRepository.GetByIdAsync(1);
            var appInfo = new AppInfoResponse
            {
                data = new AppInfoDto
                {
                    Version = "1.01",
                    Message = "BK Mock: Pungle - Teller API"
                }
            };
            return await Task.FromResult(appInfo);
        }
    }
}