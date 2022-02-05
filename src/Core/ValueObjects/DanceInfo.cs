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
            Types = types;
        }
        #region Fields - Properties

        private DanceLevel _level;
        public DanceLevel Level 
        { 
            get => _level;
            private set => _level = value;
        }
        private IReadOnlyCollection<DanceType> _types;
        public IReadOnlyCollection<DanceType> Types 
        {
            get => _types;
            private set => _types = (IReadOnlyCollection<DanceType>)Guard.Against.NullOrEmpty<DanceType>(
                value, 
                nameof(Types), 
                "There should be at least one dance type for a dance info instance.");
        }
        #endregion

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Level;
            foreach(var dt in Types)
                yield return dt;            
        }
    }
}