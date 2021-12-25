using Bcan.Backend.SharedKernel;
using Ardalis.GuardClauses;
using System.Collections.Generic;

namespace Bcan.Backend.Core.ValueObjects
{
    public class FullName : ValueObject
    {
        public FullName(string firstName, string lastName)
        {
            First = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
            Last = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
        }

        public string First { get; private set; }
        public string Last { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return First;
            yield return Last;
        }

        #region New
        public FullName NewWithChangedFirstName(string firstName)
        {
            return new FullName(firstName, this.Last);
        }

        public FullName NewWithChangedLastName(string lastName)
        {
            return new FullName(this.First, lastName);
        }
        #endregion

        public string AsFormatted()
        {
            return First + " " + Last;
        }

        public string AsReverseFormatted()
        {
            return Last + ", " + First;
        }

        public override string ToString()
        {
            return "FullName [firstName=" + First + ", lastName=" + Last + "]";
        }
    }
}