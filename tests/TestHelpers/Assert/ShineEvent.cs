using Bcan.Backend.Core.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace TestHelpers.Assert
{
    public class ShineEventAssertions : ReferenceTypeAssertions<ShineEvent, ShineEventAssertions>
    {
        public ShineEventAssertions(ShineEvent instance) : base(instance)
        {            
        }

        protected override string Identifier => "ShineEvent";

        public AndConstraint<ShineEventAssertions> BeValueEqual(ShineEvent expected, string because = "", string stepName = "")
        {
            Subject.Should().NotBeNull($"actual instance for step {stepName} is null");
            Subject.Should().NotBeNull($"expected instance for step {stepName} is null");

            using(new AssertionScope(stepName))
            {
                Subject.Id.Should().Be(expected.Id);
                Subject.Title.Should().Be(expected.Title);
                Subject.Type.Should().Be(expected.Type);
            }

            return new AndConstraint<ShineEventAssertions>(this);
        }
    }

    public static class ShineEventExtensions
    {
        public static ShineEventAssertions Should(this ShineEvent instance)
        {
            return new ShineEventAssertions(instance);
        }
    }
}