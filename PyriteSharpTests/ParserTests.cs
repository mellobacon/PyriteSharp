using System.Collections.Generic;
using PyriteSharp.source.Syntax;
using PyriteSharp.source.Syntax.Parser;
using PyriteSharp.source.Syntax.Parser.Expressions;
using Xunit;

namespace PyriteSharpTests;

internal class ParseTree
{
    public List<Node> Tree { get; } = new();

    public ParseTree(Node n)
    {
        CreateTree(n);
    }

    private void CreateTree(Node node)
    {
        Tree.Add(node);
        foreach (Node child in node.GetChildren())
        {
            CreateTree(child);
        }
    }
}

public class ParserTests
{
    private static void CheckParse(string text, IReadOnlyList<TokenType> expectedtree)
    {
        Ast tree = Ast.Parse(text);
        Statement expression = tree.Root;
        ParseTree t = new ParseTree(expression);
        for (int i = 0; i < t.Tree.Count; i++)
        {
            Assert.Equal(expectedtree[i], t.Tree[i].Type);
        }
    }

    [Fact]
    private void ParserParsesLiteralExpression()
    {
        CheckParse("100", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("false", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.LITERAL_EXPRESSION,
            TokenType.FALSE_KEYWORD
        });
        CheckParse("true", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.LITERAL_EXPRESSION,
            TokenType.TRUE_KEYWORD
        });
        CheckParse("\"thing\"", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.LITERAL_EXPRESSION,
            TokenType.STRING
        });
    }
    [Fact]
    private void ParserParsesBinaryExpression()
    {
        CheckParse("1 + 2", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.PLUS,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("1 - 2", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.MINUS,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("1 == 2", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.DOUBLE_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("1 != 2", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.BANG_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("1 << 2", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.DOUBLE_LESS_THAN,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("1 >> 2", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.DOUBLE_MORE_THAN,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("1 ^ 2", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.HAT,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("true && false", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.TRUE_KEYWORD,
            TokenType.DOUBLE_AND,
            TokenType.LITERAL_EXPRESSION,
            TokenType.FALSE_KEYWORD
        });
        CheckParse("true || false", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.TRUE_KEYWORD,
            TokenType.DOUBLE_PIPE,
            TokenType.LITERAL_EXPRESSION,
            TokenType.FALSE_KEYWORD
        });
        CheckParse("1 & 0", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.AND,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("1 | 0", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.PIPE,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("1 % 0", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.MOD,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("1 > 0", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.MORE_THAN,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("1 < 0", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.LESS_THAN,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("1 >= 2", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.MORE_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("1 <= 2", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.LESS_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
    }

    [Fact]
    private void ParserRespectsBinaryPrecedence()
    {
        CheckParse("1 + 2 * 5", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.PLUS,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.STAR,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("1 / 2 * 5", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.SLASH,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.STAR,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("1 / 2 == 5", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.SLASH,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.DOUBLE_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("1 / 2 != 5", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.SLASH,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.BANG_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
    }

    [Fact]
    private void ParserParsesGroupedExpressions()
    {
        CheckParse("(1)", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.GROUPED_EXPRESSION,
            TokenType.LEFT_PAREN,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.RIGHT_PAREN
        });
        
        CheckParse("(1 + 2) * 3", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.GROUPED_EXPRESSION,
            TokenType.LEFT_PAREN,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.PLUS,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.RIGHT_PAREN,
            TokenType.STAR,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
    }

    [Fact]
    private void ParserParsesAssignments()
    {
        CheckParse("x = 1", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.ASSIGNMENT_EXPRESSION,
            TokenType.VARIABLE,
            TokenType.EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("y = 1 + 2 * 5 == 5", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.ASSIGNMENT_EXPRESSION,
            TokenType.VARIABLE,
            TokenType.EQUAL,
            TokenType.BINARY_EXPRESSION,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.PLUS,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.STAR,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.DOUBLE_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("x += 1", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.ASSIGNMENT_EXPRESSION,
            TokenType.VARIABLE,
            TokenType.PLUS_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("x -= 1", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.ASSIGNMENT_EXPRESSION,
            TokenType.VARIABLE,
            TokenType.MINUS_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("x *= 1", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.ASSIGNMENT_EXPRESSION,
            TokenType.VARIABLE,
            TokenType.STAR_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("x /= 1", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.ASSIGNMENT_EXPRESSION,
            TokenType.VARIABLE,
            TokenType.SLASH_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
        CheckParse("x %= 1", new []
        {
            TokenType.EXPRESSION_STATEMENT,
            TokenType.ASSIGNMENT_EXPRESSION,
            TokenType.VARIABLE,
            TokenType.MOD_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
    }

    [Fact]
    private void ParserParsesStatements()
    {
        CheckParse("{ }", new []
        {
            TokenType.STATEMENT,
            TokenType.LEFT_BRACKET,
            TokenType.RIGHT_BRACKET,
        });
        CheckParse("{ 7 }", new []
        {
            TokenType.STATEMENT,
            TokenType.LEFT_BRACKET,
            TokenType.EXPRESSION_STATEMENT,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.RIGHT_BRACKET,
        });
        CheckParse("{ (1 + 2) * 3 }", new []
        {
            TokenType.STATEMENT,
            TokenType.LEFT_BRACKET,
            TokenType.EXPRESSION_STATEMENT,
            TokenType.BINARY_EXPRESSION,
            TokenType.GROUPED_EXPRESSION,
            TokenType.LEFT_PAREN,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.PLUS,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.RIGHT_PAREN,
            TokenType.STAR,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.RIGHT_BRACKET,
        });
        CheckParse("{ x = 1 }", new []
        {
            TokenType.STATEMENT,
            TokenType.LEFT_BRACKET,
            TokenType.EXPRESSION_STATEMENT,
            TokenType.ASSIGNMENT_EXPRESSION,
            TokenType.VARIABLE,
            TokenType.EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.RIGHT_BRACKET,
        });
        CheckParse("{ x = 1 { x += 2 } }", new []
        {
            TokenType.STATEMENT,
            TokenType.LEFT_BRACKET,
            TokenType.EXPRESSION_STATEMENT,
            TokenType.ASSIGNMENT_EXPRESSION,
            TokenType.VARIABLE,
            TokenType.EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.STATEMENT,
            TokenType.LEFT_BRACKET,
            TokenType.EXPRESSION_STATEMENT,
            TokenType.ASSIGNMENT_EXPRESSION,
            TokenType.VARIABLE,
            TokenType.PLUS_EQUAL,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.RIGHT_BRACKET,
            TokenType.RIGHT_BRACKET,
        });
    }
}