namespace PyriteSharp.source.Syntax;

public static class SyntaxInfo
{
    public static int GetBinaryPrecedence(TokenType type)
    {
        return type switch
        {
            TokenType.DOUBLE_MORE_THAN => 8,
            TokenType.DOUBLE_LESS_THAN => 8,
            TokenType.AND => 7,
            TokenType.HAT => 6,
            TokenType.PIPE => 5,
            TokenType.STAR => 4,
            TokenType.SLASH => 4,
            TokenType.MOD => 4,
            TokenType.PLUS => 3,
            TokenType.MINUS => 3,
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
                type = TokenType.BAD_TOKEN;
                break;
        }
    }
}