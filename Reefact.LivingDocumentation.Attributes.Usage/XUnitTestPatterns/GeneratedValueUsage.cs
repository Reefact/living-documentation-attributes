#region Usings declarations

using System;
using Reefact.LivingDocumentation.Attributes.XUnitTestPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.XUnitTestPatterns.GeneratedValueSample {

    // Half the integration tests need a container number that is not already in the database. Written by
    // hand they collide, and the collision looks like a bug in the gate.
    //
    // GENERATED VALUE makes a fresh one.

    public static class Any {

        /// <summary>
        ///     A container number nothing else in this run will use.
        /// </summary>
        /// <remarks>
        ///     It removes collisions between runs and buys them back as irreproducibility: a failure that
        ///     depends on what was generated cannot be re-run unless what was generated is reported. Being
        ///     able to ask which values are generated is the point of the annotation.
        /// </remarks>
        [GeneratedValue]
        public static string ContainerNumber() {
            return $"MSKU{Guid.NewGuid().ToString("N").Substring(0, 7)}";
        }

    }
}
