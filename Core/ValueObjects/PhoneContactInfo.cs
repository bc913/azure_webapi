using System.Collections.Generic;
using Bcan.Backend.SharedKernel;
using Ardalis.GuardClauses;

namespace Bcan.Backend.Core.ValueObjects
{
    public class PhoneContactInfo : ValueObject
    {   
        public PhoneContactInfo(string phoneNumber)
        {
            Number = Guard.Against.NullOrWhiteSpace(phoneNumber,
                nameof(phoneNumber));
        }
        public string Number { get; private set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }

        public override string ToString()
        {
            return "PhoneContactInfo [Number=" + Number + "]";
        }
    }
}