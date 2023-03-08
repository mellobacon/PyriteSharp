namespace PyriteSharp.source.Syntax.Binder;

public class BoundBinaryOperator
{
    public readonly BoundBinaryType BinaryType;
    private readonly TokenType _optype;
    private readonly Type _lefttype;
    private readonly Type _righttype;
    public readonly Type ExpectedType;
    private BoundBinaryOperator(BoundBinaryType binarytype, TokenType optype, Type lefttype, Type righttype, Type expectedtype)
    {
        BinaryType = binarytype;
        _optype = optype;
        _lefttype = lefttype;
        _righttype = righttype;
        ExpectedType = expectedtype;
    }

    private static readonly BoundBinaryOperator[] _operators =
    {
        new(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(int), typeof(int), typeof(int)),
        new(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(double), typeof(double), typeof(double)),
        new(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(int), typeof(double), typeof(double)),
        new(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(double), typeof(int), typeof(double)),
        new(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(float), typeof(int), typeof(float)),
        new(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(double), typeof(float), typeof(float)),
        new(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(float), typeof(float), typeof(float)),
        new(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(double), typeof(float), typeof(double)),
        new(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(float), typeof(double), typeof(double)),
        new(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(string), typeof(string), typeof(string)),
        
        new(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(int), typeof(int), typeof(int)),
        new(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(double), typeof(double), typeof(double)),
        new(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(double), typeof(int), typeof(double)),
        new(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(int), typeof(double), typeof(double)),
        new(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(float), typeof(float), typeof(float)),
        new(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(float), typeof(int), typeof(float)),
        new(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(int), typeof(float), typeof(float)),
        new(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(double), typeof(float), typeof(double)),
        new(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(float), typeof(double), typeof(double)),

        new(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(int), typeof(int), typeof(int)),
        new(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(double), typeof(double), typeof(double)),
        new(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(double), typeof(int), typeof(double)),
        new(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(int), typeof(double), typeof(double)),
        new(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(float), typeof(float), typeof(float)),
        new(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(float), typeof(int), typeof(float)),
        new(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(int), typeof(float), typeof(float)),
        new(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(float), typeof(double), typeof(double)),
        new(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(double), typeof(float), typeof(double)),
        
        new(BoundBinaryType.STRING_MULTIPLICATION, TokenType.STAR, typeof(string), typeof(int), typeof(string)),
        
        new(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(int), typeof(int), typeof(int)),
        new(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(double), typeof(double), typeof(double)),
        new(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(double), typeof(int), typeof(double)),
        new(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(int), typeof(double), typeof(double)),
        new(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(float), typeof(float), typeof(float)),
        new(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(float), typeof(int), typeof(float)),
        new(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(int), typeof(float), typeof(float)),
        new(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(float), typeof(double), typeof(double)),
        new(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(double), typeof(float), typeof(double)),
        
        new(BoundBinaryType.MOD, TokenType.MOD, typeof(int), typeof(int), typeof(int)),
        new(BoundBinaryType.MOD, TokenType.MOD, typeof(double), typeof(int), typeof(int)),
        new(BoundBinaryType.MOD, TokenType.MOD, typeof(int), typeof(double), typeof(int)),
        new(BoundBinaryType.MOD, TokenType.MOD, typeof(double), typeof(double), typeof(int)),
        new(BoundBinaryType.MOD, TokenType.MOD, typeof(float), typeof(int), typeof(int)),
        new(BoundBinaryType.MOD, TokenType.MOD, typeof(int), typeof(float), typeof(int)),
        new(BoundBinaryType.MOD, TokenType.MOD, typeof(float), typeof(float), typeof(int)),
        new(BoundBinaryType.MOD, TokenType.MOD, typeof(float), typeof(double), typeof(double)),
        new(BoundBinaryType.MOD, TokenType.MOD, typeof(double), typeof(float), typeof(double)),
        
        new(BoundBinaryType.BITSHIFT_LEFT, TokenType.DOUBLE_LESS_THAN, typeof(int), typeof(int), typeof(int)),
        new(BoundBinaryType.BITSHIFT_RIGHT, TokenType.DOUBLE_MORE_THAN, typeof(int), typeof(int), typeof(int)),
        new(BoundBinaryType.BITWISE_OR, TokenType.PIPE, typeof(int), typeof(int), typeof(int)),
        new(BoundBinaryType.BITWISE_AND, TokenType.AND, typeof(int), typeof(int), typeof(int)),
        new(BoundBinaryType.BITWISE_EXCLUSIVE_OR, TokenType.HAT, typeof(int), typeof(int), typeof(int)),
        
        new(BoundBinaryType.LOGICAL_OR, TokenType.DOUBLE_PIPE, typeof(bool), typeof(bool), typeof(bool)),
        new(BoundBinaryType.LOGICAL_AND, TokenType.DOUBLE_AND, typeof(bool), typeof(bool), typeof(bool)),
        
        new(BoundBinaryType.LOGICAL_EQUALS, TokenType.DOUBLE_EQUAL, typeof(bool), typeof(bool), typeof(bool)),
        new(BoundBinaryType.LOGICAL_EQUALS, TokenType.DOUBLE_EQUAL, typeof(double), typeof(double), typeof(bool)),
        new(BoundBinaryType.LOGICAL_EQUALS, TokenType.DOUBLE_EQUAL, typeof(double), typeof(float), typeof(bool)),
        new(BoundBinaryType.LOGICAL_EQUALS, TokenType.DOUBLE_EQUAL, typeof(double), typeof(int), typeof(bool)),
        new(BoundBinaryType.LOGICAL_EQUALS, TokenType.DOUBLE_EQUAL, typeof(float), typeof(float), typeof(bool)),
        new(BoundBinaryType.LOGICAL_EQUALS, TokenType.DOUBLE_EQUAL, typeof(float), typeof(double), typeof(bool)),
        new(BoundBinaryType.LOGICAL_EQUALS, TokenType.DOUBLE_EQUAL, typeof(float), typeof(int), typeof(bool)),
        new(BoundBinaryType.LOGICAL_EQUALS, TokenType.DOUBLE_EQUAL, typeof(int), typeof(float), typeof(bool)),
        new(BoundBinaryType.LOGICAL_EQUALS, TokenType.DOUBLE_EQUAL, typeof(int), typeof(double), typeof(bool)),
        new(BoundBinaryType.LOGICAL_EQUALS, TokenType.DOUBLE_EQUAL, typeof(int), typeof(int), typeof(bool)),
        
        new(BoundBinaryType.LOGICAL_NOT_EQUALS, TokenType.BANG_EQUAL, typeof(bool), typeof(bool), typeof(bool)),
        new(BoundBinaryType.LOGICAL_NOT_EQUALS, TokenType.BANG_EQUAL, typeof(double), typeof(double), typeof(bool)),
        new(BoundBinaryType.LOGICAL_NOT_EQUALS, TokenType.BANG_EQUAL, typeof(double), typeof(float), typeof(bool)),
        new(BoundBinaryType.LOGICAL_NOT_EQUALS, TokenType.BANG_EQUAL, typeof(double), typeof(int), typeof(bool)),
        new(BoundBinaryType.LOGICAL_NOT_EQUALS, TokenType.BANG_EQUAL, typeof(float), typeof(float), typeof(bool)),
        new(BoundBinaryType.LOGICAL_NOT_EQUALS, TokenType.BANG_EQUAL, typeof(float), typeof(double), typeof(bool)),
        new(BoundBinaryType.LOGICAL_NOT_EQUALS, TokenType.BANG_EQUAL, typeof(float), typeof(int), typeof(bool)),
        new(BoundBinaryType.LOGICAL_NOT_EQUALS, TokenType.BANG_EQUAL, typeof(int), typeof(float), typeof(bool)),
        new(BoundBinaryType.LOGICAL_NOT_EQUALS, TokenType.BANG_EQUAL, typeof(int), typeof(double), typeof(bool)),
        new(BoundBinaryType.LOGICAL_NOT_EQUALS, TokenType.BANG_EQUAL, typeof(int), typeof(int), typeof(bool)),
        
        new(BoundBinaryType.LESS_THAN, TokenType.LESS_THAN, typeof(double), typeof(double), typeof(bool)),
        new(BoundBinaryType.LESS_THAN, TokenType.LESS_THAN, typeof(double), typeof(float), typeof(bool)),
        new(BoundBinaryType.LESS_THAN, TokenType.LESS_THAN, typeof(double), typeof(int), typeof(bool)),
        new(BoundBinaryType.LESS_THAN, TokenType.LESS_THAN, typeof(float), typeof(float), typeof(bool)),
        new(BoundBinaryType.LESS_THAN, TokenType.LESS_THAN, typeof(float), typeof(double), typeof(bool)),
        new(BoundBinaryType.LESS_THAN, TokenType.LESS_THAN, typeof(float), typeof(int), typeof(bool)),
        new(BoundBinaryType.LESS_THAN, TokenType.LESS_THAN, typeof(int), typeof(float), typeof(bool)),
        new(BoundBinaryType.LESS_THAN, TokenType.LESS_THAN, typeof(int), typeof(double), typeof(bool)),
        new(BoundBinaryType.LESS_THAN, TokenType.LESS_THAN, typeof(int), typeof(int), typeof(bool)),
        
        new(BoundBinaryType.MORE_THAN, TokenType.MORE_THAN, typeof(double), typeof(double), typeof(bool)),
        new(BoundBinaryType.MORE_THAN, TokenType.MORE_THAN, typeof(double), typeof(float), typeof(bool)),
        new(BoundBinaryType.MORE_THAN, TokenType.MORE_THAN, typeof(double), typeof(int), typeof(bool)),
        new(BoundBinaryType.MORE_THAN, TokenType.MORE_THAN, typeof(float), typeof(float), typeof(bool)),
        new(BoundBinaryType.MORE_THAN, TokenType.MORE_THAN, typeof(float), typeof(double), typeof(bool)),
        new(BoundBinaryType.MORE_THAN, TokenType.MORE_THAN, typeof(float), typeof(int), typeof(bool)),
        new(BoundBinaryType.MORE_THAN, TokenType.MORE_THAN, typeof(int), typeof(float), typeof(bool)),
        new(BoundBinaryType.MORE_THAN, TokenType.MORE_THAN, typeof(int), typeof(double), typeof(bool)),
        new(BoundBinaryType.MORE_THAN, TokenType.MORE_THAN, typeof(int), typeof(int), typeof(bool)),
        
        new(BoundBinaryType.LESS_EQUAL, TokenType.LESS_EQUAL, typeof(double), typeof(double), typeof(bool)),
        new(BoundBinaryType.LESS_EQUAL, TokenType.LESS_EQUAL, typeof(double), typeof(float), typeof(bool)),
        new(BoundBinaryType.LESS_EQUAL, TokenType.LESS_EQUAL, typeof(double), typeof(int), typeof(bool)),
        new(BoundBinaryType.LESS_EQUAL, TokenType.LESS_EQUAL, typeof(float), typeof(float), typeof(bool)),
        new(BoundBinaryType.LESS_EQUAL, TokenType.LESS_EQUAL, typeof(float), typeof(double), typeof(bool)),
        new(BoundBinaryType.LESS_EQUAL, TokenType.LESS_EQUAL, typeof(float), typeof(int), typeof(bool)),
        new(BoundBinaryType.LESS_EQUAL, TokenType.LESS_EQUAL, typeof(int), typeof(float), typeof(bool)),
        new(BoundBinaryType.LESS_EQUAL, TokenType.LESS_EQUAL, typeof(int), typeof(double), typeof(bool)),
        new(BoundBinaryType.LESS_EQUAL, TokenType.LESS_EQUAL, typeof(int), typeof(int), typeof(bool)),
        
        new(BoundBinaryType.MORE_EQUAL, TokenType.MORE_EQUAL, typeof(double), typeof(double), typeof(bool)),
        new(BoundBinaryType.MORE_EQUAL, TokenType.MORE_EQUAL, typeof(double), typeof(float), typeof(bool)),
        new(BoundBinaryType.MORE_EQUAL, TokenType.MORE_EQUAL, typeof(double), typeof(int), typeof(bool)),
        new(BoundBinaryType.MORE_EQUAL, TokenType.MORE_EQUAL, typeof(float), typeof(float), typeof(bool)),
        new(BoundBinaryType.MORE_EQUAL, TokenType.MORE_EQUAL, typeof(float), typeof(double), typeof(bool)),
        new(BoundBinaryType.MORE_EQUAL, TokenType.MORE_EQUAL, typeof(float), typeof(int), typeof(bool)),
        new(BoundBinaryType.MORE_EQUAL, TokenType.MORE_EQUAL, typeof(int), typeof(float), typeof(bool)),
        new(BoundBinaryType.MORE_EQUAL, TokenType.MORE_EQUAL, typeof(int), typeof(double), typeof(bool)),
        new(BoundBinaryType.MORE_EQUAL, TokenType.MORE_EQUAL, typeof(int), typeof(int), typeof(bool)),
    };

    public static BoundBinaryOperator? GetBinaryOperator(Type left, TokenType op, Type right)
    {
        foreach (BoundBinaryOperator @operator in _operators)
        {
            if (left == @operator._lefttype && op == @operator._optype && right == @operator._righttype)
            {
                return @operator;
            }
        }

        return null;
    }
}