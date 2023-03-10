namespace PyriteSharp.source.Syntax;

public enum TokenType
{
    NUMBER,
    STRING,
    KEYWORD,
    VARIABLE,
    FALSE_KEYWORD,
    TRUE_KEYWORD,
    
    PLUS,
    MINUS,
    SLASH,
    STAR,
    MOD,
    EQUAL,
    
    LEFT_PAREN,
    RIGHT_PAREN,
    LEFT_BRACKET,
    RIGHT_BRACKET,
    
    PIPE,
    AND,
    DOUBLE_PIPE,
    DOUBLE_AND,
    
    DOUBLE_COLON,
    COLON,
    SEMICOLON,
    
    DOUBLE_EQUAL,
    PLUS_EQUAL,
    MINUS_EQUAL,
    STAR_EQUAL,
    SLASH_EQUAL,
    MOD_EQUAL,
    
    BANG,
    BANG_EQUAL,
    
    HAT,
    
    LESS_THAN,
    MORE_THAN,
    LESS_EQUAL,
    MORE_EQUAL,
    
    DOUBLE_LESS_THAN,
    DOUBLE_MORE_THAN,

    LITERAL_EXPRESSION,
    BINARY_EXPRESSION,
    GROUPED_EXPRESSION,
    ASSIGNMENT_EXPRESSION,

    WHITESPACE,
    EOF_TOKEN,
    BAD_TOKEN
}