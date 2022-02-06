using Bcan.Backend.Core.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Bcan.Backend.TestHelpers.Assert
{
    public class ShineClassAssertions : ReferenceTypeAssertions<ShineClass, ShineClassAssertions>
    {
        public ShineClassAssertions(ShineClass instance) : base(instance)
        {            
        }

        protected override string Identifier => "ShineClass";

        public AndConstraint<ShineClassAssertions> BeValueEqual(ShineClass expected, string because = "", string stepName = "")
        {
            Subject.Should().NotBeNull($"actual instance for step {stepName} is null");
            Subject.Should().NotBeNull($"expected instance for step {stepName} is null");

            using(new AssertionScope(stepName))
            {
                Subject.Id.Should().Be(expected.Id);
                Subject.Title.Should().Be(expected.Title);
                Subject.Type.Should().Be(expected.Type);
                Subject.Time.Should().Be(expected.Time);
                Subject.Info.Should().Be(expected.Info);
                Subject.Fee.Should().Be(expected.Fee);
                Subject.Policy.Should().Be(expected.Policy);
                Subject.Location.Should().Be(expected.Location);
                Subject.Media.Should().Be(expected.Media);
                Subject.Description.Should().Be(expected.Description);
            }

            return new AndConstraint<ShineClassAssertions>(this);
        }
    }

    public static class ShineClassExtensions
    {
        public static ShineClassAssertions Should(this ShineClass instance)
        {
            return new ShineClassAssertions(instance);
        }
    }
}