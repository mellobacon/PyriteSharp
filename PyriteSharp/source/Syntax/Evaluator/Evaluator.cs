using PyriteSharp.source.Syntax.Binder;

namespace PyriteSharp.source.Syntax.Evaluator;

public class Evaluator
{
    private readonly Dictionary<string, object?> _variables;
    private object? value;
    private readonly BoundStatement _root;

    public Evaluator(BoundStatement expression, Dictionary<string, object?> variables)
    {
        _root = expression;
        _variables = variables;
    }

    public object? Evaluate()
    {
        EvaluateStatement(_root);
        return value;
    }

    private void EvaluateStatement(BoundStatement statement)
    {
        switch (statement)
        {
            case BoundBlockStatement b:
                foreach (var s in b.Statements)
                {
                    EvaluateStatement(s);
                }
                break;
            case BoundExpressionStatement e:
                value = EvaluateExpression(e.Expression);
                break;
        }
    }

    private object? EvaluateExpression(BoundExpression expression)
    {
        switch (expression.BoundType)
        {
            case BoundType.STATEMENT:
                return EvaluateExpression(expression);
            case BoundType.ASSIGNMENT:
                return EvaluateAssignmentExpression((BoundAssignmentExpression)expression);
            case BoundType.BINARY:
                return EvaluateBinaryExpression((BoundBinaryExpression)expression);
            case BoundType.LITERAL:
                return EvaluateLiteral((BoundLiteralExpression)expression);
        }

        return null;
    }

    private object? EvaluateAssignmentExpression(BoundAssignmentExpression expression)
    {
        if (_variables[expression.Variable.Name] == null)
        {
            object? v = EvaluateExpression(expression.Expression);

            dynamic? variableValue = v switch
            {
                double d => d,
                float f => f,
                int i => i,
                bool b => b,
                string s => s,
                _ => v
            };

            dynamic? variable = _variables[expression.Variable.Name] switch
            {
                double d => d,
                float f => f,
                int i => i,
                bool b => b,
                string s => s,
                _ => v
            };
            if (expression.IsCompound)
            {
                switch (expression.Op.Tokentype)
                {
                    case TokenType.PLUS_EQUAL:
                        variable += variableValue;
                        break;
                    case TokenType.MINUS_EQUAL:
                        variable -= variableValue;
                        break;
                    case TokenType.SLASH_EQUAL:
                        variable /= variableValue;
                        break;
                    case TokenType.STAR_EQUAL:
                        variable *= variableValue;
                        break;
                    case TokenType.MOD_EQUAL:
                        variable %= variableValue;
                        break;
                }
                
            }
            _variables[expression.Variable.Name] = variable;
            return _variables[expression.Variable.Name];
        }

        return null;
    }

    private object? EvaluateBinaryExpression(BoundBinaryExpression expression)
    {
        object? left = EvaluateExpression(expression.Left);
        object? right = EvaluateExpression(expression.Right);
        if (left is null || right is null)
        {
            return null;
        }

        dynamic? leftvalue = left switch
        {
            double d => d,
            float f => f,
            int i => i,
            bool b => b,
            string s => s,
            _ => null
        };

        dynamic? rightvalue = right switch
        {
            double d => d,
            float f => f,
            int i => i,
            bool b => b,
            string s => s,
            _ => null
        };

        string MultiplyString(string x, int y)
        {
            string v = x;
            for (int i = 1; i < y; i++)
            {
                v += x;
            }

            return v;
        }
        return expression.Op?.BinaryType switch
        {
            BoundBinaryType.ADDITION => leftvalue + rightvalue,
            BoundBinaryType.SUBTRACTION => leftvalue - rightvalue,
            BoundBinaryType.DIVISION => leftvalue / rightvalue,
            BoundBinaryType.MULTIPLICATION => leftvalue * rightvalue,
            BoundBinaryType.STRING_MULTIPLICATION => MultiplyString(leftvalue, rightvalue),
            BoundBinaryType.MOD => leftvalue % rightvalue,
            BoundBinaryType.BITSHIFT_LEFT => leftvalue << rightvalue,
            BoundBinaryType.BITSHIFT_RIGHT => leftvalue >> rightvalue,
            BoundBinaryType.BITWISE_OR => leftvalue | rightvalue,
            BoundBinaryType.BITWISE_AND => leftvalue & rightvalue,
            BoundBinaryType.BITWISE_EXCLUSIVE_OR => leftvalue ^ rightvalue,
            BoundBinaryType.LOGICAL_OR => leftvalue || rightvalue,
            BoundBinaryType.LOGICAL_AND => leftvalue && rightvalue,
            BoundBinaryType.LOGICAL_EQUALS => leftvalue == rightvalue,
            BoundBinaryType.LOGICAL_NOT_EQUALS => leftvalue != rightvalue,
            BoundBinaryType.LESS_THAN => leftvalue < rightvalue,
            BoundBinaryType.MORE_THAN => leftvalue > rightvalue,
            BoundBinaryType.MORE_EQUAL => leftvalue >= rightvalue,
            BoundBinaryType.LESS_EQUAL => leftvalue <= rightvalue,
            _ => null
        };
    }
    private object? EvaluateLiteral(BoundLiteralExpression expression)
    {
        object? v = expression.Value;
        
        // evaluate with no quotations
        if (v is string s)
        {
            v = s.Replace("\"", "");
        }

        return v;
    }
}