namespace PyriteSharp.source.Syntax;

public static class SyntaxInfo
{
    public static int GetBinaryPrecedence(TokenType type)
    {
        return type switch
        {
            TokenType.DOUBLE_MORE_THAN => 10,
            TokenType.DOUBLE_LESS_THAN => 10,
            TokenType.AND => 9,
            TokenType.HAT => 8,
            TokenType.PIPE => 7,
            TokenType.STAR => 6,
            TokenType.SLASH => 6,
            TokenType.MOD => 6,
            TokenType.PLUS => 5,
            TokenType.MINUS => 5,
            TokenType.LESS_THAN => 4,
            TokenType.MORE_THAN => 4,
            TokenType.DOUBLE_EQUAL => 4,
            TokenType.BANG_EQUAL => 3,
            TokenType.DOUBLE_AND => 2,
            TokenType.DOUBLE_PIPE => 1,
            _ => 0
        };
    }

    public static void TryParseKeyword(string text, out TokenType type, out bool? value)
    {
        value = null;
        switch (text)
        {
            case "false":
                type = TokenType.FALSE_KEYWORD;
                value = false;
                break;
            case "true":
                type = TokenType.TRUE_KEYWORD;
                value = true;
                break;
            default:
                type = TokenType.VARIABLE;
                break;
        }
    }
}