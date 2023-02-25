using PyriteSharp.source.Syntax.Parser;

namespace PyriteSharp.source.Syntax;

public class Token : Node
{
    public string text;
    public TokenType tokentype;
    public object? value;
    public Token(string text, TokenType tokentype, object? value)
    {
        this.text = text;
        this.tokentype = tokentype;
        this.value = value;
        Type = this.tokentype;
    }

    public override TokenType Type { get; }
    public override IEnumerable<Node> GetChildren()
    {
        return Enumerable.Empty<Node>();
    }
}