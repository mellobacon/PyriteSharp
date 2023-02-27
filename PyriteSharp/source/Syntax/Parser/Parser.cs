using PyriteSharp.source.Syntax.Parser.Expressions;

namespace PyriteSharp.source.Syntax.Parser;

public class Parser
{
    private readonly List<Token> _tokens = new();
    private Token _current;
    private int _position;
    public Parser(string text)
    {
        Lexer.Lexer lexer = new Lexer.Lexer(text);
        while (true)
        {
            Token token = lexer.Lex();
            if (token.Tokentype is TokenType.WHITESPACE or TokenType.BAD_TOKEN)
            {
                continue;
            }
            if (token.Tokentype == TokenType.EOF_TOKEN) break;
            _tokens.Add(token);
        }

        _current = _tokens[0];
    }

    private void Advance()
    {
        _position++;
        _current = _position >= _tokens.Count ? _tokens[^1] : _tokens[_position];
    }
    private Token GetNextToken()
    {
        Token current = _current;
        Advance();
        return current;
    }

    public Ast Parse()
    {
        Expression expression = ParseExpression();
        return new Ast(expression);
    }

    private Expression ParseExpression()
    {
        return ParseBinaryExpression();
    }

    private Expression ParseBinaryExpression(int precedence = 0)
    {
        Expression left = ParseLiteralExpression();
        while (true)
        {
            int currentprecedence = SyntaxInfo.GetBinaryPrecedence(_current.Tokentype);
            if (currentprecedence <= precedence)
            {
                break;
            }

            Token op = GetNextToken();
            Expression right = ParseBinaryExpression(currentprecedence);
            left = new BinaryExpression(left, op, right);
        }

        return left;
    }

    private Expression ParseLiteralExpression()
    {
        if (_current.Tokentype == TokenType.LEFT_PAREN)
        {
            Token leftparen = GetNextToken();
            Expression expression = ParseExpression();
            Token rightparen = GetNextToken();
            return new GroupedExpression(leftparen, expression, rightparen);
        }
        Token literal = GetNextToken();
        return new LiteralExpression(literal, literal.Value);
    }
}