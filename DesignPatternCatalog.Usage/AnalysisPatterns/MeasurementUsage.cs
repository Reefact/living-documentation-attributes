#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.MeasurementSample {

    // A national livestock recording scheme. Eleven thousand herds send in weights, milk yields, somatic cell
    // counts, backfat depths — and every few months the breed societies want a new trait recorded, because a
    // genetic evaluation is only as good as what was measured.
    //
    // Figures 3.1 and 3.2 are the before and after of one class; figure 3.6 is the before and after of the
    // model. A class with one property per recordable trait must be edited, migrated and redeployed to record a
    // twelfth, and the person who wants the twelfth is a geneticist, not a developer.
    //
    // MEASUREMENT splits what may be measured from what was. The phenomenon type is configured; the measurement
    // refers to it. A new trait is a row, and every query that ranges over measurements of a kind keeps working
    // because the kind is a reference rather than a name.
    //
    // The subject is a party rather than an animal on purpose: a herd has measurable properties too — a bulk
    // tank cell count is a measurement of the herd — and admitting that costs the model nothing.

    /// <summary>
    ///     What kind of thing may be measured, and in what unit.
    /// </summary>
    /// <remarks>
    ///     The knowledge level. Adding a trait is adding one of these, which is the whole return on the
    ///     indirection.
    /// </remarks>
    [Measurement.PhenomenonType]
    public sealed class Trait {

        public Trait(string name, string unit) {
            Name = name;
            Unit = unit;
        }

        /// <summary>Liveweight, milk yield, somatic cell count.</summary>
        public string Name { get; }

        /// <summary>The unit its measurements must be in.</summary>
        public string Unit { get; }

    }

    /// <summary>
    ///     One recorded quantity of one trait, for one subject, on one day.
    /// </summary>
    [Measurement.Measurement(PhenomenonType = typeof(Trait))]
    public sealed class Recording {

        public Recording(Trait trait, IRecordingSubject subject, decimal amount, string unit, DateOnly on) {
            if (unit != trait.Unit) {
                throw new ArgumentException($"{trait.Name} is recorded in {trait.Unit}, not {unit}", nameof(unit));
            }

            Trait   = trait;
            Subject = subject;
            Amount  = amount;
            Unit    = unit;
            On      = on;
        }

        /// <summary>What was measured.</summary>
        public Trait Trait { get; }

        /// <summary>What or who it was measured on.</summary>
        [Measurement.Subject]
        public IRecordingSubject Subject { get; }

        /// <summary>The amount.</summary>
        public decimal Amount { get; }

        /// <summary>Its unit, which the trait decides.</summary>
        public string Unit { get; }

        /// <summary>When it was taken.</summary>
        public DateOnly On { get; }

    }

    /// <summary>
    ///     Anything the scheme can measure: an animal, or a herd.
    /// </summary>
    [Party]
    public interface IRecordingSubject {

        /// <summary>How the scheme identifies it.</summary>
        string Identifier { get; }

    }

    /// <summary>One animal.</summary>
    public sealed class Animal : IRecordingSubject {

        public Animal(string eartag) {
            Identifier = eartag;
        }

        /// <inheritdoc />
        public string Identifier { get; }

    }

    /// <summary>One herd, which has measurable properties of its own.</summary>
    public sealed class Herd : IRecordingSubject {

        private readonly List<Animal> _animals = new();

        public Herd(string holdingNumber) {
            Identifier = holdingNumber;
        }

        /// <inheritdoc />
        public string Identifier { get; }

        /// <summary>The animals in it.</summary>
        public IReadOnlyList<Animal> Animals => _animals;

        /// <summary>Adds an animal to the herd.</summary>
        public void Add(Animal animal) {
            _animals.Add(animal);
        }

    }

}
