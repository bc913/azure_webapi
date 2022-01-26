using System.Collections.Generic;
using Bcan.Backend.SharedKernel;

namespace Bcan.Backend.Core.ValueObjects
{
    public class EventPolicy : ValueObject
    {
        private EventPolicy(){}

        public EventPolicy(bool dressCode, bool partner, bool danceShoes, string description)
        {
            DressCode = dressCode;
            Partner = partner;
            DanceShoes = danceShoes;
            Description = description;
        }

        public bool DressCode       { get; private set; } = false;
        public bool Partner         { get; private set; } = false;
        public bool DanceShoes      { get; private set; } = false;
        public string Description   { get; private set; }

        public static EventPolicy NoPolicy()
        {
            return new EventPolicy();
        }

        public static EventPolicy DressCodeRequired(string description = "")
        {
            return new EventPolicy(true, false, false, description);
        }

        public static EventPolicy PartnerRequired(string description = "")
        {
            return new EventPolicy(false, true, false, description);
        }

        public static EventPolicy DressCodeAndDanceShoesRequired(string description = "")
        {
            return new EventPolicy(true, false, true, description);
        }

        public static EventPolicy DressCodeAndPartnerRequired(string description = "")
        {
            return new EventPolicy(true, true, false, description);
        }

        public static EventPolicy AllRequired(string description = "")
        {
            return new EventPolicy(true, true, true, description);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DressCode;
            yield return Partner;
            yield return DanceShoes;
        }
    }
}