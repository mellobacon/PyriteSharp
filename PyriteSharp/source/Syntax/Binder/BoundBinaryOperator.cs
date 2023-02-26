namespace PyriteSharp.source.Syntax.Binder;

public class BoundBinaryOperator
{
    public BoundBinaryType BinaryType;
    private TokenType _optype;
    private Type _lefttype;
    private Type _righttype;
    public Type ExpectedType;
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
        new BoundBinaryOperator(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(int), typeof(int), typeof(int)),
        new BoundBinaryOperator(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(float), typeof(float), typeof(float)),
        new BoundBinaryOperator(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(int), typeof(float), typeof(float)),
        new BoundBinaryOperator(BoundBinaryType.ADDITION, TokenType.PLUS, typeof(float), typeof(int), typeof(float)),
        
        new BoundBinaryOperator(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(int), typeof(int), typeof(int)),
        new BoundBinaryOperator(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(float), typeof(float), typeof(float)),
        new BoundBinaryOperator(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(float), typeof(int), typeof(float)),
        new BoundBinaryOperator(BoundBinaryType.SUBTRACTION, TokenType.MINUS, typeof(int), typeof(float), typeof(float)),
        
        new BoundBinaryOperator(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(int), typeof(int), typeof(int)),
        new BoundBinaryOperator(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(float), typeof(float), typeof(float)),
        new BoundBinaryOperator(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(float), typeof(int), typeof(float)),
        new BoundBinaryOperator(BoundBinaryType.MULTIPLICATION, TokenType.STAR, typeof(int), typeof(float), typeof(float)),
        
        new BoundBinaryOperator(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(int), typeof(int), typeof(int)),
        new BoundBinaryOperator(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(float), typeof(float), typeof(float)),
        new BoundBinaryOperator(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(float), typeof(int), typeof(float)),
        new BoundBinaryOperator(BoundBinaryType.DIVISION, TokenType.SLASH, typeof(int), typeof(float), typeof(float)),
        
        new BoundBinaryOperator(BoundBinaryType.MOD, TokenType.MOD, typeof(int), typeof(int), typeof(int)),
        new BoundBinaryOperator(BoundBinaryType.MOD, TokenType.MOD, typeof(float), typeof(int), typeof(int)),
        new BoundBinaryOperator(BoundBinaryType.MOD, TokenType.MOD, typeof(int), typeof(float), typeof(int)),
        new BoundBinaryOperator(BoundBinaryType.MOD, TokenType.MOD, typeof(float), typeof(float), typeof(int))
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