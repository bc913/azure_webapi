using Ardalis.GuardClauses;
using Bcan.Backend.SharedKernel;
using System;
using System.Collections.Generic;

namespace Bcan.Backend.Core.ValueObjects
{
    public class FeeOption : ValueObject
    {
        private FeeOption() {}

        public FeeOption(decimal value, IndividualType individual, PaymentType payment, string description = null)
        {
            if(individual == IndividualType.Undefined)
                throw new ArgumentException("FeeOption requires a valid IndividualType other than Undefined.", nameof(individual));
            
            if(payment == PaymentType.Undefined)
                throw new ArgumentException("FeeOption requires a valid PaymentType other than Undefined.", nameof(payment));

            Value = Guard.Against.Negative(value, nameof(value), "Fee option value argument should have a non-negative value.");
            Individual = individual;
            Payment = payment;
            Description = description;
        }

        public decimal Value                { get; private set; }
        public IndividualType Individual    { get; private set; }
        public PaymentType Payment          { get; private set; }
        public string Description           { get; private set; }

        public static FeeOption FreeForStudents(string description = null)
        {
            return new FeeOption(decimal.Zero, IndividualType.Student, PaymentType.NoPayment, description);
        }

        public static FeeOption FreeForAll(string description = null)
        {
            return new FeeOption(decimal.Zero, IndividualType.All, PaymentType.NoPayment, description);
        }

        public static IReadOnlyCollection<FeeOption> RegularAndStudentWithDiscountForOneTimePayment(decimal baseValue, float discountPercantage)
        {
            Guard.Against.OutOfRange<float>(discountPercantage, nameof(discountPercantage), 0.0f, 100.0f);

            decimal discountMultiplier = (decimal)(discountPercantage / 100.0f);
            var discountAmount = decimal.Multiply(baseValue, discountMultiplier);
            var discountedFee = decimal.Subtract(baseValue, discountAmount);

            return new List<FeeOption>
            {
                new FeeOption(baseValue, IndividualType.Regular, PaymentType.OneTime),
                new FeeOption(discountedFee, IndividualType.Student, PaymentType.OneTime)
            };
        }

        public static FeeOption SameForAllWithSinglePayment(decimal value, string description = null)
        {
            return new FeeOption(value, IndividualType.All, PaymentType.OneTime, description);
        }        

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return Individual;
            yield return Payment;
        }
    }
}