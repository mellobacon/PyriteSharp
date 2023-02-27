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
        new(BoundBinaryType.MOD, TokenType.MOD, typeof(double), typeof(float), typeof(double))
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