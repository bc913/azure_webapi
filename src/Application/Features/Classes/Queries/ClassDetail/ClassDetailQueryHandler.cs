using AutoMapper;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Application.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Bcan.Backend.Application.Features.Classes.Queries.ShineClassDetail
{
    public class ShineClassDetailQueryHandler : IRequestHandler<ShineClassDetailQuery, ShineClassDetailDto>
    {
        private readonly IReadRepository<ShineClass> _repository;
        private readonly IMapper _mapper;

        public ShineClassDetailQueryHandler(IReadRepository<ShineClass> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ShineClassDetailDto> Handle(ShineClassDetailQuery request, CancellationToken cancellationToken)
        {
            var theClass = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if(theClass is null)
                throw new NotFoundException(nameof(ShineClass), request.Id);

            return _mapper.Map<ShineClassDetailDto>(theClass);
        }
    }
}