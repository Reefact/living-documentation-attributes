#region Usings declarations

using System.Collections;

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.IteratorSample {

    // Walking a playlist without exposing how the tracks are stored.

    [Iterator.Iterator]
    public interface ITrackCursor {

        bool   MoveNext();
        string Current { get; }

    }

    [Iterator.Aggregate]
    public interface IPlaylist {

        ITrackCursor Browse();

    }

    [Iterator.ConcreteAggregate(Aggregate = typeof(IPlaylist))]
    public sealed class ShuffledPlaylist : IPlaylist {

        internal readonly string[] Tracks;

        public ShuffledPlaylist(params string[] tracks) { Tracks = tracks; }

        public ITrackCursor Browse() => new ShuffledCursor(this);

    }

    [Iterator.ConcreteIterator(Iterator = typeof(ITrackCursor), ConcreteAggregate = typeof(ShuffledPlaylist))]
    public sealed class ShuffledCursor : ITrackCursor {

        private readonly ShuffledPlaylist _playlist;
        private          int              _index = -1;

        public ShuffledCursor(ShuffledPlaylist playlist) { _playlist = playlist; }

        public string Current => _playlist.Tracks[_index];

        public bool MoveNext() => ++_index < _playlist.Tracks.Length;

    }

}
