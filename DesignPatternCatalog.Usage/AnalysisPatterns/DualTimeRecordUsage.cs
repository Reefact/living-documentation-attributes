#region Usings declarations

using System;
using System.Collections.Generic;

using DesignPatternCatalog.AnalysisPatterns;

#endregion

namespace DesignPatternCatalog.Usage.AnalysisPatterns.DualTimeRecordSample {

    // An electricity supplier's meter readings. A reading is taken on the twelfth, arrives from the data
    // collector on the fifteenth, and is corrected on the twenty-second when the collector re-sends it with the
    // digits transposed.
    //
    // Two entirely different questions are asked of that. The billing team asks what the consumption was in the
    // period — as the world was. The regulator asks what the supplier knew when it issued the bill on the
    // eighteenth — as the records stood. A model with one date can answer one of them, and answers the other
    // wrongly without knowing.
    //
    // DUAL TIME RECORD keeps them apart. Figure 3.11 draws applicability and recording time as separate
    // associations, and the reason the second is load-bearing is that it makes a retrospective correction
    // visible: without it, amending a reading rewrites what the organisation is deemed to have known, and the
    // bill it issued becomes unexplainable.
    //
    // Both members are marked because the failure is reading one where the other was meant, and it never throws.

    /// <summary>
    ///     The two times a reading carries.
    /// </summary>
    /// <remarks>
    ///     Held together because they answer different questions, and separate because a result that arrives on
    ///     Thursday about Tuesday is normal rather than exceptional.
    /// </remarks>
    [DualTimeRecord.TimeRecord]
    public sealed class ReadingTimes {

        public ReadingTimes(DateOnly applicableOn, DateTime recordedAt) {
            ApplicableOn = applicableOn;
            RecordedAt   = recordedAt;
        }

        /// <summary>When the reading was true of the meter.</summary>
        [DualTimeRecord.Applicability]
        public DateOnly ApplicableOn { get; }

        /// <summary>When the supplier came to know it.</summary>
        [DualTimeRecord.RecordingTime]
        public DateTime RecordedAt { get; }

    }

    /// <summary>
    ///     One meter reading, with both of its times.
    /// </summary>
    public sealed class MeterReading {

        public MeterReading(string meterSerial, decimal register, ReadingTimes times) {
            MeterSerial = meterSerial;
            Register    = register;
            Times       = times;
        }

        /// <summary>Which meter.</summary>
        public string MeterSerial { get; }

        /// <summary>What the register showed.</summary>
        public decimal Register { get; }

        /// <summary>When it was true, and when it was known.</summary>
        public ReadingTimes Times { get; }

    }

    /// <summary>
    ///     The readings on record, and the two questions the dual record makes separable.
    /// </summary>
    public sealed class ReadingHistory {

        private readonly List<MeterReading> _readings = new();

        /// <summary>Records a reading. Corrections are added, never substituted.</summary>
        public void Add(MeterReading reading) {
            _readings.Add(reading);
        }

        /// <summary>
        ///     The best reading for a date, as the records stand now — what the billing team means.
        /// </summary>
        public MeterReading? BestFor(string meterSerial, DateOnly applicableOn) {
            MeterReading? best = null;
            foreach (MeterReading reading in _readings) {
                if (reading.MeterSerial != meterSerial || reading.Times.ApplicableOn != applicableOn) {
                    continue;
                }

                if (best is null || reading.Times.RecordedAt > best.Times.RecordedAt) {
                    best = reading;
                }
            }

            return best;
        }

        /// <summary>
        ///     The reading for a date as it stood at a moment in the past — what an auditor means, and what a
        ///     single-dated model cannot produce at all.
        /// </summary>
        public MeterReading? AsKnownAt(string meterSerial, DateOnly applicableOn, DateTime asAt) {
            MeterReading? best = null;
            foreach (MeterReading reading in _readings) {
                if (reading.MeterSerial != meterSerial
                 || reading.Times.ApplicableOn != applicableOn
                 || reading.Times.RecordedAt > asAt) {
                    continue;
                }

                if (best is null || reading.Times.RecordedAt > best.Times.RecordedAt) {
                    best = reading;
                }
            }

            return best;
        }

    }

}
