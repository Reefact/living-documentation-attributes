#region Usings declarations

using DesignPatternCatalog.Posa2;

#endregion

namespace DesignPatternCatalog.Usage.Posa2.ExtensionInterfaceSample {

    // A berth is a berth: it can be reserved and released. Then the container terminal wanted crane
    // scheduling on its berths, the tanker jetty wanted bunkering, and the cruise terminal wanted a
    // passenger gangway booking — and none of them wanted the other two.
    //
    // Adding all three to IBerth gave every implementation nine methods, six of which threw
    // NotSupportedException. Client code grew tests for which berth it had, written as string comparisons
    // on the berth's name, because that was the only thing that distinguished them.
    //
    // EXTENSION INTERFACE lets a berth export what it actually does. A client asks, and a berth that
    // gained bunkering last month breaks nobody who does not ask.

    /// <summary>
    ///     What every berth answers, and the way to ask what else it does.
    /// </summary>
    /// <remarks>
    ///     A client's whole ability to discover what a berth can do is this one operation. That is the
    ///     trade the pattern makes: the set of interfaces may grow freely, and in exchange nothing in the
    ///     type system tells a client whether a given berth supports one — only asking at run time does.
    /// </remarks>
    [ExtensionInterface.RootInterface(Component = typeof(ContainerBerth))]
    public interface IBerth {

        void Reserve(string vesselId);

        void Release();

        T? Extension<T>() where T : class;

    }

    /// <summary>
    ///     Crane scheduling, for the berths that have cranes.
    /// </summary>
    /// <remarks>
    ///     Adding another interface like this one breaks nothing and recompiles no client — which is the
    ///     claim. The cost lands on the client that wants it: it must handle the berth that says no.
    /// </remarks>
    [ExtensionInterface.ExtensionInterface(RootInterface = typeof(IBerth))]
    public interface ICraneScheduling {

        void ScheduleCrane(int craneNumber, string window);

    }

    /// <summary>
    ///     Bunkering, added for the tanker jetty without touching <see cref="IBerth" />.
    /// </summary>
    [ExtensionInterface.ExtensionInterface(RootInterface = typeof(IBerth))]
    public interface IBunkering {

        void OrderFuel(int tonnes);

    }

    /// <summary>
    ///     A container berth: reservable, with cranes, without bunkering.
    /// </summary>
    /// <remarks>
    ///     No client ever holds this type, which is what lets the set of interfaces it implements change
    ///     without anything being recompiled. A client that reaches for the concrete type has removed that
    ///     property for itself, and nothing but this remark says so.
    /// </remarks>
    [ExtensionInterface.Component]
    public sealed class ContainerBerth : IBerth, ICraneScheduling {

        private string? _reservedFor;

        public void Reserve(string vesselId) {
            _reservedFor = vesselId;
        }

        public void Release() {
            _reservedFor = null;
        }

        public T? Extension<T>() where T : class {
            return this as T;
        }

        public void ScheduleCrane(int craneNumber, string window) {
            if (_reservedFor is null) { throw new InvalidOperationException("the berth is not reserved"); }
        }

    }

    /// <summary>
    ///     Creates berths and hands back the root interface.
    /// </summary>
    /// <remarks>
    ///     Where a client's only reference to a concrete berth would otherwise be. Returning
    ///     <see cref="IBerth" /> rather than <see cref="ContainerBerth" /> is the whole of what keeps the
    ///     extension set free to change.
    /// </remarks>
    [ExtensionInterface.ComponentFactory(Component = typeof(ContainerBerth))]
    public sealed class BerthFactory {

        public IBerth CreateContainerBerth() {
            return new ContainerBerth();
        }

    }

}
