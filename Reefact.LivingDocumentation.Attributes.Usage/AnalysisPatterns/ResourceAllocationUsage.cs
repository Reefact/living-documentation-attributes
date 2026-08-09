#region Usings declarations

using System;
using System.Collections.Generic;

using Reefact.LivingDocumentation.Attributes.AnalysisPatterns;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.AnalysisPatterns.ResourceAllocationSample {

    // The plan books a berth for eighteen days, two welders for sixty hours and four hundred litres of
    // coating. What the refit actually draws is twenty-two berth days, forty-one welder hours and five hundred
    // and ten litres. Both sets of numbers matter, and they are not the same fact.
    //
    // RESOURCE ALLOCATION makes each claim an object, and keeps what a proposal books apart from what the work
    // used — which is the only way the next quote gets better.

    /// <summary>What kind of thing an action can call on.</summary>
    /// <remarks>
    ///     A type object, so adding a resource to the yard is configuration rather than a class.
    /// </remarks>
    [ResourceAllocation.ResourceType]
    public sealed class YardResource {

        public YardResource(string name, string unit, bool isAsset) {
            Name    = name;
            Unit    = unit;
            IsAsset = isAsset;
        }

        /// <summary>"Dry dock 2", "welder", "epoxy coating".</summary>
        public string Name { get; }

        public string Unit { get; }

        /// <summary>An asset is occupied rather than consumed, so its quantity is a duration.</summary>
        public bool IsAsset { get; }

    }

    /// <summary>One claim on a resource, carrying a quantity.</summary>
    /// <remarks>
    ///     The constraint the book states: if the resource is an asset, the quantity is in time units — you
    ///     allocate a dock for days, never four hundred litres of it.
    /// </remarks>
    [ResourceAllocation.ResourceAllocation(ResourceType = typeof(YardResource))]
    public abstract class Claim {

        protected Claim(YardResource resource, decimal quantity, string unit) {
            if (resource.IsAsset && unit != "day" && unit != "hour") {
                throw new ArgumentException($"{resource.Name} is an asset: allocate it in time", nameof(unit));
            }
            Resource = resource;
            Quantity = quantity;
            Unit     = unit;
        }

        public YardResource Resource { get; }

        public decimal Quantity { get; }

        public string Unit { get; }

    }

    /// <summary>A claim on a type rather than on a thing.</summary>
    /// <remarks>
    ///     Two welders, not those two. It is what a plan can state before anybody knows who will be free.
    /// </remarks>
    [ResourceAllocation.GeneralAllocation]
    public sealed class BookedByType : Claim {

        public BookedByType(YardResource resource, decimal quantity, string unit)
            : base(resource, quantity, unit) { }

    }

    /// <summary>A claim on the very asset.</summary>
    /// <remarks>
    ///     It says more, and can therefore fail where a general claim would not — which is why the two are
    ///     separate rather than one with a nullable asset.
    /// </remarks>
    [ResourceAllocation.SpecificAllocation]
    public sealed class BookedAsset : Claim {

        public BookedAsset(YardResource resource, string asset, decimal quantity, string unit)
            : base(resource, quantity, unit) {
            Asset = asset;
        }

        /// <summary>"Dry dock 2", "welder Kowalski".</summary>
        public string Asset { get; }

    }

    /// <summary>What a proposal claims, and what the work drew.</summary>
    public sealed class RefitResources {

        private readonly List<Claim> _booked = new List<Claim>();
        private readonly List<Claim> _used   = new List<Claim>();

        /// <summary>
        ///     What was booked.
        /// </summary>
        /// <remarks>
        ///     A booking can be refused, moved or dropped without anything having happened.
        /// </remarks>
        [ResourceAllocation.Books]
        public IReadOnlyList<Claim> Books => _booked;

        /// <summary>
        ///     What was actually drawn.
        /// </summary>
        /// <remarks>
        ///     Held apart from what was booked, because the difference is the figure anybody quoting again
        ///     wants.
        /// </remarks>
        [ResourceAllocation.Uses]
        public IReadOnlyList<Claim> Uses => _used;

        public void Book(Claim claim) => _booked.Add(claim);

        public void Use(Claim claim) => _used.Add(claim);

    }

}
