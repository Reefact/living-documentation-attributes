#region Usings declarations

using Reefact.LivingDocumentation.Attributes.GangOfFour;

#endregion

namespace Reefact.LivingDocumentation.Attributes.Usage.GangOfFour.CompositeSample {

    // A file tree: a folder and a file answer the same questions.

    [Composite.Component]
    public interface INode {

        string Name { get; }
        long   Size { get; }

    }

    [Composite.Leaf(Component = typeof(INode))]
    public sealed class FileNode : INode {

        public FileNode(string name, long size) {
            Name = name;
            Size = size;
        }

        public string Name { get; }
        public long   Size { get; }

    }

    [Composite.Composite(Component = typeof(INode))]
    public sealed class FolderNode : INode {

        private readonly List<INode> _children = new();

        public FolderNode(string name) { Name = name; }

        public string Name { get; }
        public long   Size => _children.Sum(child => child.Size);

        public void Add(INode child) => _children.Add(child);

    }

}
