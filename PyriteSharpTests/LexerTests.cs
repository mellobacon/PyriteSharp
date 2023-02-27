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
            ("1_0_0", TokenType.NUMBER),
            ("1_0___0", TokenType.NUMBER),
            ("1.00", TokenType.NUMBER),
            ("10.0", TokenType.NUMBER),
            (".100", TokenType.NUMBER),
            ("1_0_0f", TokenType.NUMBER),
            ("100d", TokenType.NUMBER),
            ("100f", TokenType.NUMBER),
            ("1.00d", TokenType.NUMBER),
            ("10.0f", TokenType.NUMBER),
            ("0b00100", TokenType.NUMBER),
            ("0b_00100", TokenType.NUMBER),
            ("+", TokenType.PLUS),
            ("-", TokenType.MINUS),
            ("*", TokenType.STAR),
            ("%", TokenType.MOD),
            ("/", TokenType.SLASH),
            ("=", TokenType.EQUAL),
            (">>", TokenType.DOUBLE_MORE_THAN),
            ("<<", TokenType.DOUBLE_LESS_THAN),
            ("|", TokenType.PIPE),
            ("&", TokenType.AND),
            ("^", TokenType.HAT),
            ("||", TokenType.DOUBLE_PIPE),
            ("&&", TokenType.DOUBLE_AND),
            ("(", TokenType.LEFT_PAREN),
            ("==", TokenType.DOUBLE_EQUAL),
            ("!=", TokenType.BANG_EQUAL),
            ("!", TokenType.BANG),
            (")", TokenType.RIGHT_PAREN),
            ("1.0.0", TokenType.BAD_TOKEN),
            ("..100", TokenType.BAD_TOKEN),
            ("1.", TokenType.BAD_TOKEN),
            ("_100", TokenType.BAD_TOKEN),
            ("100_", TokenType.BAD_TOKEN),
            ("0b10010f", TokenType.BAD_TOKEN),
            ("true", TokenType.TRUE_KEYWORD),
            ("false", TokenType.FALSE_KEYWORD),
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
            if (token.Tokentype == TokenType.EOF_TOKEN) break;
            Assert.Equal(text, token.Text);
            Assert.Equal(type, token.Tokentype);
        }
    }
}