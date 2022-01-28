using Ardalis.GuardClauses;
using Bcan.Backend.SharedKernel;
using System.Collections.Generic;

namespace Bcan.Backend.Core.ValueObjects
{
    public class DanceInfo : ValueObject
    {
        private DanceInfo(){}

        public DanceInfo(DanceLevel level, IReadOnlyCollection<DanceType> types)
        {
            Level = level;
            Types = (IReadOnlyCollection<DanceType>)Guard.Against.NullOrEmpty<DanceType>(
                types, 
                nameof(types), 
                "There should be at least one dance type for a dance info instance.");
        }

        public DanceLevel Level { get; private set; }
        public IReadOnlyCollection<DanceType> Types { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Level;
            foreach(var dt in Types)
                yield return dt;            
        }
    }
}