using System.Linq;
using Bcan.Backend.Core.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace Bcan.Backend.Core.UnitTests.ValueObjects
{
    public class FeeOptionTests
    {
        [Fact]
        public void CtorShouldSucceedWithValidArgs()
        {
            decimal value = 34.3m;
            var individual = IndividualType.Regular;
            var payment = PaymentType.OneTime;
            var description = "dummy description";

            var sut = new FeeOption(value, individual, payment, description);

            sut.Value.Should().Be(value);
            sut.Individual.Should().Be(individual);
            sut.Payment.Should().Be(payment);
            sut.Description.Should().Be(description);
        }

        [Fact]
        public void CtorShouldThrowForUndefinedIndividual()
        {
            Action act = () => new FeeOption(435.6m, IndividualType.Undefined, PaymentType.OneTime);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void CtorShouldThrowForUndefinedPaymentType()
        {
            Action act = () => new FeeOption(345.6m, IndividualType.Student, PaymentType.Undefined);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [Fact]
        public void FreeForStudentsFactoryMethodShouldSucceed()
        {
            var sut = FeeOption.FreeForStudents();

            sut.Value.Should().Be(0.0m);
            sut.Individual.Should().Be(IndividualType.Student);
            sut.Payment.Should().Be(PaymentType.NoPayment);
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void FreeForAllFactoryMethodShouldSucceed()
        {
            var sut = FeeOption.FreeForAll();

            sut.Value.Should().Be(0.0m);
            sut.Individual.Should().Be(IndividualType.All);
            sut.Payment.Should().Be(PaymentType.NoPayment);
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void RegularAndStudentWithDiscountForOneTimePaymentFactoryMethodShouldSucceed()
        {
            decimal baseValue = 100.0m;
            float discountPercentage = 40.0f;

            var feeOptions = FeeOption.RegularAndStudentWithDiscountForOneTimePayment(baseValue, discountPercentage);

            feeOptions.Should().HaveCount(2);
            // Regular
            var regular = feeOptions.FirstOrDefault(fo => fo.Individual == IndividualType.Regular);
            regular.Should().NotBeNull();
            regular.Value.Should().Be(baseValue);
            regular.Individual.Should().Be(IndividualType.Regular);
            regular.Payment.Should().Be(PaymentType.OneTime);

            // Student
            var student = feeOptions.FirstOrDefault(fo => fo.Individual == IndividualType.Student);
            student.Should().NotBeNull();
            student.Value.Should().Be(60.0m);
            student.Individual.Should().Be(IndividualType.Student);
            student.Payment.Should().Be(PaymentType.OneTime);
        }

        [Theory]
        [InlineData(-12.0f)]
        [InlineData(101.0f)]
        public void RegularAndStudentWithDiscountForOneTimePaymentFactoryMethodShouldFailForInvalidDiscountValue(float discountPercentage)
        {
            Action act = () => FeeOption.RegularAndStudentWithDiscountForOneTimePayment(100.00m, discountPercentage);
            act.Should().ThrowExactly<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void SameForAllWithSinglePaymentFactoryMethodShouldSucceed()
        {
            decimal value = 34.6m;

            var sut = FeeOption.SameForAllWithSinglePayment(value);

            sut.Value.Should().Be(value);
            sut.Individual.Should().Be(IndividualType.All);
            sut.Payment.Should().Be(PaymentType.OneTime);
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void EqualityIgnoresDescription()
        {
            var sut1 = FeeOption.FreeForAll();
            var sut2 = FeeOption.FreeForAll("desc");

            sut1.Should().Be(sut2);
            sut2.Should().Be(sut1);
            (sut1 == sut2).Should().BeTrue();
            (sut1 != sut2).Should().BeFalse();
        }

        [Fact]
        public void EqualityShouldFailForDifferentValuedInstances()
        {
            var sut1 = FeeOption.FreeForStudents();
            var sut2 = FeeOption.FreeForAll();

            sut1.Should().NotBe(sut2);
            sut2.Should().NotBe(sut1);
            (sut1 == sut2).Should().BeFalse();
            (sut1 != sut2).Should().BeTrue();
        }
    }
}
