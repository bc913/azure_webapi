using Ardalis.GuardClauses;
using Bcan.Backend.SharedKernel;
using System.Collections.Generic;

namespace Bcan.Backend.Core.ValueObjects
{
    public class Fee : ValueObject
    {
        private Fee() {}
        
        public Fee(IReadOnlyCollection<FeeOption> options, string description)
        {
            Options = (IReadOnlyCollection<FeeOption>)Guard.Against.NullOrEmpty<FeeOption>(options, nameof(options));
            Description = description;
        }

        public IReadOnlyCollection<FeeOption> Options { get; private set; }
        public string Description                     { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Options;
        }
    }
}