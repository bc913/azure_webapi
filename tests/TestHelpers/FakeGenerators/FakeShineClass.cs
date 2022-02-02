using Bcan.Backend.Core;
using Bcan.Backend.Core.Entities;
using Bcan.Backend.Core.ValueObjects;
using Bcan.Backend.SharedKernel;
using System;
using System.Collections.Generic;

namespace Bcan.Backend.TestHelpers.FakeGenerators
{
    public static class FakeShineClass
    {
        private static DanceInfo _info = new DanceInfo(DanceLevel.Advanced, new List<DanceType> { DanceType.Salsa, DanceType.Bachata });
        private static DateTimeOffsetRange _time = DateTimeOffsetRange.CreateOneDayRange(DateTimeOffset.UtcNow);
        private static EventPolicy _policy = EventPolicy.DressCodeAndDanceShoesRequired();
        private static Fee _fee = Fee.RegularAndStudentWithDiscountForOneTimePayment(100.0m, 35.0f);

        public static ShineClass Instance => new ShineClass(Guid.NewGuid(), "Fake class",
            _info, FakeLocation.Instance, _time, _policy, _fee, "This is a fake class");
    }
}