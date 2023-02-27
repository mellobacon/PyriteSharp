namespace PyriteSharp.source.Syntax;

public static class SyntaxInfo
{
    public static int GetBinaryPrecedence(TokenType type)
    {
        return type switch
        {
            TokenType.DOUBLE_MORE_THAN => 6,
            TokenType.DOUBLE_LESS_THAN => 6,
            TokenType.AND => 5,
            TokenType.HAT => 4,
            TokenType.PIPE => 3,
            TokenType.STAR => 2,
            TokenType.SLASH => 2,
            TokenType.MOD => 2,
            TokenType.PLUS => 1,
            TokenType.MINUS => 1,
            _ => 0
        };
    }
}