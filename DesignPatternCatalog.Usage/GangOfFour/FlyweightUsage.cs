#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.FlyweightSample {

    // Thousands of map markers, a handful of distinct icons.

    [Flyweight.Flyweight]
    public interface IMarkerIcon {

        void DrawAt(int x, int y);

    }

    [Flyweight.ConcreteFlyweight(Flyweight = typeof(IMarkerIcon))]
    public sealed class SharedMarkerIcon : IMarkerIcon {

        private readonly byte[] _bitmap;

        public SharedMarkerIcon(byte[] bitmap) { _bitmap = bitmap; }

        // x and y are the extrinsic state: they are passed in, never stored.
        public void DrawAt(int x, int y) { }

    }

    [Flyweight.UnsharedConcreteFlyweight(Flyweight = typeof(IMarkerIcon))]
    public sealed class HighlightedMarkerIcon : IMarkerIcon {

        // Deliberately not shared: it carries per-instance animation state.
        private int _pulse;

        public void DrawAt(int x, int y) => _pulse++;

    }

    [Flyweight.FlyweightFactory(Flyweight = typeof(IMarkerIcon))]
    public sealed class MarkerIcons {

        private readonly Dictionary<string, IMarkerIcon> _shared = new();

        public IMarkerIcon Of(string kind) {
            if (_shared.TryGetValue(kind, out IMarkerIcon? icon)) { return icon; }

            icon           = new SharedMarkerIcon(Array.Empty<byte>());
            _shared[kind]  = icon;

            return icon;
        }

    }

}
