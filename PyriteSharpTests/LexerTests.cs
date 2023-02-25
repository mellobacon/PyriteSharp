using System.Collections.Generic;
using PyriteSharp.source.Syntax;
using PyriteSharp.source.Syntax.Lexer;
using Xunit;

namespace PyriteSharpTests;

public class LexerTests
{
    private static IEnumerable<object[]> GetTokens()
    {
        (string, TokenType)[] tokens =
        {
            ("1", TokenType.NUMBER),
            ("100", TokenType.NUMBER),
            ("+", TokenType.PLUS),
            ("-", TokenType.MINUS),
            ("*", TokenType.STAR),
            ("%", TokenType.MOD),
            ("/", TokenType.SLASH),
            ("=", TokenType.EQUAL),
            ("(", TokenType.LEFT_PAREN),
            (")", TokenType.RIGHT_PAREN),
            ("test", TokenType.BAD_TOKEN)
        };
        foreach ((string text, TokenType type) in tokens)
        {
            yield return new object[] { text, type };
        }
    }
    [Theory]
    [MemberData(nameof(GetTokens))]
    public void LexerReadsTokens(string text, TokenType type)
    {
        Lexer lexer = new Lexer(text);
        while (true)
        {
            Token token = lexer.Lex();
            if (token.tokentype == TokenType.EOF_TOKEN) break;
            if (token.tokentype == TokenType.BAD_TOKEN)
            {
                Assert.NotEqual(text, token.text);
                Assert.Equal(type, token.tokentype);
            }
            else
            {
                Assert.Equal(text, token.text);
                Assert.Equal(type, token.tokentype);
            }
        }
    }
}