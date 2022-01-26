using Ardalis.GuardClauses;
using Bcan.Backend.SharedKernel;
using System.Collections.Generic;

namespace Bcan.Backend.Core.ValueObjects
{
    public class Fee : ValueObject
    {
        private Fee() {}
        
        public Fee(IReadOnlyCollection<FeeOption> options, string description = null)
        {
            Options = (IReadOnlyCollection<FeeOption>)Guard.Against.NullOrEmpty<FeeOption>(options, nameof(options));
            Description = description;
        }

        public IReadOnlyCollection<FeeOption> Options { get; private set; }
        public string Description                     { get; private set; }

        public static Fee FreeForStudents(string description = null)
        {
            return new Fee(new List<FeeOption> { FeeOption.FreeForStudents() }, description);
        }

        public static Fee FreeForAll(string description = null)
        {
            return new Fee(new List<FeeOption> { FeeOption.FreeForAll() }, description);
        }

        public static Fee RegularAndStudentWithDiscountForOneTimePayment(decimal baseValue, float discountPercentage, string description = null)
        {
            return new Fee(FeeOption.RegularAndStudentWithDiscountForOneTimePayment(baseValue, discountPercentage), description);
        }

        public static Fee SameForAllWithSinglePayment(decimal value, string description = null)
        {
            return new Fee(new List<FeeOption> { FeeOption.SameForAllWithSinglePayment(value) }, description);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            foreach (var o in Options)
                yield return o;
        }
    }
}