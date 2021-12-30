using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Application.Contracts.Repositories;

using System;
using Bcan.Backend.Core.ValueObjects;

namespace Bcan.Backend.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserLiteDto>>
    {
        private readonly IReadRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IMapper mapper, IReadRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserLiteDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            //var allUsers = await _userRepository.ListAsync(cancellationToken);
            var allUsers = await Task.FromResult(new List<User>()
            {
                new User(Guid.NewGuid(), new FullName("Burak", " Can"),   "bc913"),
                new User(Guid.NewGuid(), new FullName("Eren ", "Bekar"),  "eb926"),
                new User(Guid.NewGuid(), new FullName("Basar", " Ozkurt"), "bo916"),
                new User(Guid.NewGuid(), new FullName("Emre ", "Yuksek"),  "ey906"),
                new User(Guid.NewGuid(), new FullName("Burco", " Tiftikci"), "bt1296")
            });

            return _mapper.Map<List<UserLiteDto>>(allUsers);
        }
    }
}