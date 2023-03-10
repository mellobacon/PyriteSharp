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

    private Token Peek(int offset = 1)
    {
        int index = _position + offset;
        return index >= _tokens.Count ? _tokens[^1] : _tokens[index];
    }

    public Ast Parse()
    {
        Statement expression = ParseStatement();
        return new Ast(expression);
    }

    private Statement ParseStatement()
    {
        if (_current.Tokentype == TokenType.LEFT_BRACKET)
        {
            Token left = GetNextToken();
            List<Statement> statements = new List<Statement>();
            while (_current.Tokentype != TokenType.RIGHT_BRACKET)
            {
                Statement statement = ParseStatement();
                statements.Add(statement);
            }
            
            Token right = GetNextToken();
            return new BlockStatement(left, statements, right);
        }
        Expression expression = ParseExpression();
        return new ExpressionStatement(expression);
    }
    
    private Expression ParseExpression()
    {
        return ParseAssignmentExpression();
    }

    private Expression ParseAssignmentExpression()
    {
        bool hascompound = Peek().Tokentype is TokenType.PLUS_EQUAL or TokenType.MINUS_EQUAL or 
            TokenType.STAR_EQUAL or TokenType.SLASH_EQUAL or TokenType.MOD_EQUAL;
        if (_current.Tokentype == TokenType.VARIABLE && (Peek().Tokentype is TokenType.EQUAL || hascompound))
        {
            Token variable = GetNextToken();
            Token equals = GetNextToken();
            Expression expression = ParseExpression();
            return new AssignmentExpression(variable, equals, expression, hascompound);
        }

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