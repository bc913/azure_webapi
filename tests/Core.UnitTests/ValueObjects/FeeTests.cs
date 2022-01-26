using Bcan.Backend.Core.ValueObjects;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace Bcan.Backend.Core.UnitTests.ValueObjects
{
    public class FeeTests
    {
        [Fact]
        public void CtorShouldSucceed()
        {
            var options = FeeOption.RegularAndStudentWithDiscountForOneTimePayment(100.0m, 50.0f);
            var description = "Some fee description";

            var sut = new Fee(options, description);

            sut.Options.Should().BeEquivalentTo(options);
            sut.Description.Should().Be(description);
        }

        [Fact]
        public void FreeForStudentsFactoryMethodShouldSucceed()
        {
            var options = new List<FeeOption> { FeeOption.FreeForStudents() };

            var sut = Fee.FreeForStudents();

            sut.Options.Should().BeEquivalentTo(options);
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void FreeForAllFactoryMethodShouldSucceed()
        {
            var options = new List<FeeOption> { FeeOption.FreeForAll() };

            var sut = Fee.FreeForAll();

            sut.Options.Should().BeEquivalentTo(options);
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void RegularAndStudentWithDiscountForOneTimePaymentFactoryMethodShouldSucceed()
        {
            var options = FeeOption.RegularAndStudentWithDiscountForOneTimePayment(100.0m, 40.0f);

            var sut = Fee.RegularAndStudentWithDiscountForOneTimePayment(100.0m, 40.0f);

            sut.Options.Should().BeEquivalentTo(options);
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void SameForAllWithSinglePaymentFactoryMethodShouldSucceed()
        {
            var options = new List<FeeOption> { FeeOption.SameForAllWithSinglePayment(100.0m) };

            var sut = Fee.SameForAllWithSinglePayment(100.0m);

            sut.Options.Should().BeEquivalentTo(options);
            sut.Description.Should().BeNull();
        }

        [Fact]
        public void EqualityIgnoresDescription()
        {
            var sut1 = Fee.FreeForStudents();
            var sut2 = Fee.FreeForStudents("desc");

            sut1.Should().Be(sut2);
            sut2.Should().Be(sut1);
            (sut1 == sut2).Should().BeTrue();
            (sut1 != sut2).Should().BeFalse();
        }

        [Fact]
        public void DifferentValuedInstancesShouldNotBeEqual()
        {
            var sut1 = Fee.FreeForAll();
            var sut2 = Fee.FreeForStudents();

            sut1.Should().NotBe(sut2);
            sut2.Should().NotBe(sut1);
            (sut1 == sut2).Should().BeFalse();
            (sut1 != sut2).Should().BeTrue();
        }
    }
}