using Ardalis.GuardClauses;
using Bcan.Backend.SharedKernel;
using System;
using System.Collections.Generic;

namespace Bcan.Backend.Core.ValueObjects
{
    public class FeeOption : ValueObject
    {
        public FeeOption() {}

        public FeeOption(decimal value, IndividualType individual, PaymentType payment, string description = "")
        {
            if(individual == IndividualType.Undefined)
                throw new ArgumentException("FeeOption requires a valid IndividualType other than Undefined.", nameof(individual));
            
            if(payment == PaymentType.Undefined)
                throw new ArgumentException("FeeOption requires a valid PaymentType other than Undefined.", nameof(payment));

            Value = Guard.Against.Negative(value, nameof(value), "Fee option value argument should have a non-negative value.");
            Individual = individual;
            Payment = payment;
        }

        public decimal Value                { get; private set; }
        public IndividualType Individual    { get; private set; }
        public PaymentType Payment          { get; private set; }
        public string Description           { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Individual;
            yield return Payment;
        }
    }
}