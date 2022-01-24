using Bcan.Backend.Application.Contracts.Repositories;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.TestHelpers.FakeGenerators;
using Moq;
using System;
using System.Collections.Generic;

namespace Bcan.Backend.Application.UnitTests.Features.Events
{
    public class MockEventRepository
    {
        public static Mock<IReadRepository<ShineEvent>> Instance()
        {
            var event_1_Guid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var event_2_Guid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var event_3_Guid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var event_4_Guid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");

            var events = new List<ShineEvent>
            {
                new ShineEvent(event_1_Guid, "Pittsburgh Socials",          ShineEventType.Social,    FakeLocation.Instance  ),
                new ShineEvent(event_2_Guid, "Depo Dans Bachata Level 1",   ShineEventType.Class,     FakeLocation.Instance  ),
                new ShineEvent(event_3_Guid, "DC Bachata Festival",         ShineEventType.Festival,  FakeLocation.Instance  ),
                new ShineEvent(event_4_Guid, "NYC Latin Workshop",          ShineEventType.Workshop,  FakeLocation.Instance  )
            };

            var mockRepo = new Mock<IReadRepository<ShineEvent>>();
            mockRepo.Setup(repo => repo.ListAsync(default)).ReturnsAsync(events);

            // mockRepo.Setup(repo => repo.AddAsync(It.IsAny<User>(), default)).ReturnsAsync(
            //     (User u) =>
            //     {
            //         users.Add(u);
            //         return u;
            //     }
            // );

            return mockRepo;
        }
    }
}
