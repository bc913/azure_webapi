using Bcan.Backend.SharedKernel;
using Ardalis.GuardClauses;
using System.Collections.Generic;

namespace Bcan.Backend.Core.ValueObjects
{
    public class EmailContactInfo : ValueObject
    {
        public EmailContactInfo(string emailAddress)
        {
            Address = Guard.Against.NullOrWhiteSpace(emailAddress,
                nameof(emailAddress));
        }

        public string Address { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Address;
        }

        public override string ToString()
        {
            return "EmailContactInfo [Address=" + Address + "]";
        }
    }
}