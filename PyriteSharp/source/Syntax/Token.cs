using PyriteSharp.source.Syntax.Parser;

namespace PyriteSharp.source.Syntax;

public class Token : Node
{
    public readonly string Text;
    public readonly TokenType Tokentype;
    public readonly object? Value;
    public Token(string text, TokenType tokentype, object? value)
    {
        Text = text;
        Tokentype = tokentype;
        Value = value;
        Type = Tokentype;
    }

    public override TokenType Type { get; }
    public override IEnumerable<Node> GetChildren()
    {
        return Enumerable.Empty<Node>();
    }
}