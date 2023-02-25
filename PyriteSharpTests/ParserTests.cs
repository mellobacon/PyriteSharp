using System.Collections.Generic;
using PyriteSharp.source.Syntax;
using PyriteSharp.source.Syntax.Parser;
using PyriteSharp.source.Syntax.Parser.Expressions;
using Xunit;

namespace PyriteSharpTests;

class ParseTree
{
    private readonly List<Node> _tree =  new();
    public List<Node> Tree => _tree;

    public ParseTree(Node n)
    {
        CreateTree(n);
    }

    private void CreateTree(Node node)
    {
        _tree.Add(node);
        foreach (Node child in node.GetChildren())
        {
            CreateTree(child);
        }
    }
}

public class ParserTests
{
    private void CheckParse(string text, IReadOnlyList<TokenType> expectedtree)
    {
        Ast tree = Ast.Parse(text);
        Expression expression = tree.root;
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
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
    }
    [Fact]
    private void ParserParsesBinaryExpression()
    {
        CheckParse("1 + 2", new []
        {
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.PLUS,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
        CheckParse("1 - 2", new []
        {
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.MINUS,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER
        });
    }

    [Fact]
    private void ParserRespectsBinaryPrecedence()
    {
        CheckParse("1 + 2 * 5", new []
        {
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
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.SLASH,
            TokenType.BINARY_EXPRESSION,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
            TokenType.STAR,
            TokenType.LITERAL_EXPRESSION,
            TokenType.NUMBER,
        });
    }
}