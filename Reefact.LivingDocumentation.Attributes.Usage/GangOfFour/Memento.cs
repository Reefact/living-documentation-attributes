#region Usings declarations

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.MementoSample {

    // Undo on a drawing canvas, without letting anyone else read the captured state.

    [Memento.Memento]
    public sealed record CanvasSnapshot {

        internal CanvasSnapshot(IReadOnlyList<string> shapes) { Shapes = shapes; }

        internal IReadOnlyList<string> Shapes { get; }

    }

    [Memento.Originator(Memento = typeof(CanvasSnapshot))]
    public sealed class Canvas {

        private List<string> _shapes = new();

        public void Draw(string shape) => _shapes.Add(shape);

        public CanvasSnapshot Capture()                     => new(_shapes.ToArray());
        public void           Restore(CanvasSnapshot state) => _shapes = state.Shapes.ToList();

    }

    [Memento.Caretaker(Memento = typeof(CanvasSnapshot))]
    public sealed class UndoStack {

        private readonly Stack<CanvasSnapshot> _snapshots = new();

        // Keeps the snapshots, and never looks inside them.
        public void Push(CanvasSnapshot snapshot) => _snapshots.Push(snapshot);

        public CanvasSnapshot? Pop() => _snapshots.Count == 0 ? null : _snapshots.Pop();

    }

}
