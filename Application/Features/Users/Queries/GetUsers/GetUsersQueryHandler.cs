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
                new User(Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}"), new FullName("Burak", " Can"),   "bc913"),
                new User(Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}"), new FullName("Eren ", "Bekar"),  "eb926"),
                new User(Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}"), new FullName("Basar", " Ozkurt"), "bo916")
            });

            return _mapper.Map<List<UserLiteDto>>(allUsers);
        }
    }
}