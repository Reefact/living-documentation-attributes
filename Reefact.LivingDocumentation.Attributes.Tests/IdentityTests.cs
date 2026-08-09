#region Usings declarations

using Reefact.LivingDocumentation.Attributes.Usage;

using Xunit;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Tests {

    /// <summary>
    ///     What <see cref="PatternInfo.IdentityOf(Type)" /> answers for each shape the generator emits.
    /// </summary>
    /// <remarks>
    ///     Identity is the one reading rule a consumer cannot guess and the one everything else rests on: a count, a
    ///     grouping and an architecture rule are all wrong together when it is wrong. It is also the rule that has no
    ///     compiler behind it — nothing here fails to build when the walk climbs one step too far.
    /// </remarks>
    public sealed class IdentityTests {

        [Fact]
        public void Every_role_of_a_pattern_answers_the_pattern_s_role_base() {
            Assert.Equal(typeof(Shapes.Pattern.Role), PatternInfo.IdentityOf(typeof(Shapes.Pattern.ComponentAttribute)));
            Assert.Equal(typeof(Shapes.Pattern.Role), PatternInfo.IdentityOf(typeof(Shapes.Pattern.LeafAttribute)));
        }

        [Fact]
        public void A_single_role_pattern_answers_its_own_attribute() {
            Assert.Equal(typeof(Shapes.FlatAttribute), PatternInfo.IdentityOf(typeof(Shapes.FlatAttribute)));
        }

        [Fact]
        public void A_specialisation_answers_itself_and_stays_a_pattern_of_its_own() {
            Assert.Equal(typeof(Shapes.Narrowed.Role), PatternInfo.IdentityOf(typeof(Shapes.Narrowed.ComponentAttribute)));
            Assert.Equal(typeof(Shapes.Narrowed.Role), PatternInfo.IdentityOf(typeof(Shapes.Narrowed.LeafAttribute)));
            Assert.Equal(typeof(Shapes.FlatNarrowedAttribute), PatternInfo.IdentityOf(typeof(Shapes.FlatNarrowedAttribute)));
        }

        /// <summary>
        ///     The failure the same-pattern test exists to prevent, stated as its own case.
        /// </summary>
        /// <remarks>
        ///     A multi-role specialisation inherits one abstract role base from another, so a walk that climbs through
        ///     any abstract base passes through both and reports the specialisation as the pattern it narrows. Nothing
        ///     about that fails to compile, and the count it produces looks entirely plausible — it is simply one
        ///     pattern short.
        /// </remarks>
        [Fact]
        public void A_specialisation_is_never_absorbed_into_the_pattern_it_narrows() {
            Assert.NotEqual(typeof(Shapes.Pattern.Role), PatternInfo.IdentityOf(typeof(Shapes.Narrowed.ComponentAttribute)));
            Assert.NotEqual(typeof(Shapes.Pattern.Role), PatternInfo.IdentityOf(typeof(Shapes.Narrowed.LeafAttribute)));
            Assert.NotEqual(typeof(Shapes.FlatAttribute), PatternInfo.IdentityOf(typeof(Shapes.FlatNarrowedAttribute)));
        }

        /// <summary>
        ///     Counting is the simplest thing a consumer does, and the whole point of an identity is that it counts
        ///     right. Four attributes, two patterns: every role of one pattern folds into it, and the specialisation
        ///     counts on its own rather than as the pattern it narrows.
        /// </summary>
        [Fact]
        public void The_shapes_count_as_the_patterns_they_are() {
            Type[] annotations = [
                typeof(Shapes.Pattern.ComponentAttribute), typeof(Shapes.Pattern.LeafAttribute),
                typeof(Shapes.Narrowed.ComponentAttribute), typeof(Shapes.Narrowed.LeafAttribute)
            ];

            Assert.Equal(2, annotations.Select(PatternInfo.IdentityOf).Distinct().Count());
        }

        [Fact]
        public void A_specialisation_still_answers_to_the_pattern_it_narrows() {
            // The other half of what inheritance buys: the identities differ, and a rule written for the broader
            // pattern still reaches the narrower one, because every role of it derives from the broader role base.
            Assert.True(typeof(Shapes.Pattern.Role).IsAssignableFrom(typeof(Shapes.Narrowed.LeafAttribute)));
            Assert.True(typeof(Shapes.FlatAttribute).IsAssignableFrom(typeof(Shapes.FlatNarrowedAttribute)));
        }

    }

}
