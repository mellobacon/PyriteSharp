namespace PyriteSharp.source.Syntax.Parser;

public abstract class Node
{
    public abstract TokenType Type { get; }
    public abstract IEnumerable<Node> GetChildren();
}