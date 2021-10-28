using System.Threading;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Services.Repositories;

using MediatR;

namespace ExoftOfficeManager.Application.WorkPlaces.Commands.UpdateWorkPlace
{
    public class UpdateWorkPlaceCommandHandler : IRequestHandler<UpdateWorkPlaceCommand>
    {
        private readonly IWorkPlaceRepository _repository;

        public UpdateWorkPlaceCommandHandler(IWorkPlaceRepository repo)
        {
            _repository = repo;
        }

        public async Task<Unit> Handle(UpdateWorkPlaceCommand request, CancellationToken cancellationToken)
        {
            _repository.UpdateWorkPlace(request.WorkPlace);
            await _repository.Commit();

            return Unit.Value;
        }
    }
}
