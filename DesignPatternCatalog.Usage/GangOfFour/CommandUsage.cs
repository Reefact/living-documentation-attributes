#region Usings declarations

using DesignPatternCatalog.GangOfFour;

#endregion

namespace DesignPatternCatalog.Usage.GangOfFour.CommandSample {

    // Editor actions turned into objects, so that they can be queued and undone.

    [Command.Receiver]
    public sealed class Document {

        public string Text { get; private set; } = string.Empty;

        public void Append(string fragment) => Text += fragment;
        public void Truncate(int length)    => Text = Text[..length];

    }

    [Command.Command]
    public interface IEditorCommand {

        [Command.ExecuteMethod]
        void Execute();

        void Undo();

    }

    [Command.ConcreteCommand(Command = typeof(IEditorCommand), Receiver = typeof(Document))]
    public sealed class AppendText : IEditorCommand {

        private readonly Document _document;
        private readonly string   _fragment;
        private          int      _lengthBefore;

        public AppendText(Document document, string fragment) {
            _document = document;
            _fragment = fragment;
        }

        // No annotation here: the role is introduced by IEditorCommand.Execute, and annotated there once.
        public void Execute() {
            _lengthBefore = _document.Text.Length;
            _document.Append(_fragment);
        }

        public void Undo() => _document.Truncate(_lengthBefore);

    }

    [Command.Invoker(Command = typeof(IEditorCommand))]
    public sealed class CommandHistory {

        private readonly Stack<IEditorCommand> _done = new();

        public void Run(IEditorCommand command) {
            command.Execute();
            _done.Push(command);
        }

        public void UndoLast() {
            if (_done.Count > 0) { _done.Pop().Undo(); }
        }

    }

}
