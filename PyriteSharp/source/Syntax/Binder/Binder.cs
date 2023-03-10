using PyriteSharp.source.Syntax.Parser.Expressions;

namespace PyriteSharp.source.Syntax.Binder;

public class Binder
{
    private Scope _scope;

    private Binder(Scope scope)
    {
        _scope = scope;
    }
    public static GlobalScope BindScope(Statement expression)
    {
        Scope scope = new Scope();
        Binder binder = new Binder(scope);
        BoundStatement e = binder.BindStatement(expression);
        return new GlobalScope(e, binder._scope);
    }

    private BoundStatement BindStatement(Statement expression)
    {
        return expression.Type switch
        {
            TokenType.STATEMENT => BindBlockStatement((BlockStatement)expression),
            TokenType.EXPRESSION_STATEMENT => BindExpressionStatement((ExpressionStatement)expression),
            _ => throw new Exception($"Unexpected syntax [{expression.Type}] (Binder)")
        };
    }

    private BoundStatement BindBlockStatement(BlockStatement expression)
    {
        _scope = new Scope();
        List<BoundStatement> statements = new();
        foreach (Statement e in expression.Expression)
        {
            statements.Add(BindStatement(e));
        }

        return new BoundBlockStatement(statements);
    }
    private BoundStatement BindExpressionStatement(ExpressionStatement expression)
    {
        var e = BindExpression(expression.Statement);
        return new BoundExpressionStatement(e);
    }
    
    private BoundExpression BindExpression(Expression expression)
    {
        return expression.Type switch
        {
            TokenType.ASSIGNMENT_EXPRESSION => BindAssignmentExpression((AssignmentExpression)expression),
            TokenType.GROUPED_EXPRESSION => BindGroupedExpression((GroupedExpression)expression),
            TokenType.BINARY_EXPRESSION => BindBinaryExpression((BinaryExpression)expression),
            TokenType.LITERAL_EXPRESSION => BindLiteralExpression((LiteralExpression)expression),
            _ => throw new Exception($"Unexpected syntax [{expression.Type}] (Binder)")
        };
    }

    private BoundExpression BindAssignmentExpression(AssignmentExpression expression)
    {
        BoundExpression e = BindExpression(expression.Expression);
        Variable variable = new Variable()
        {
            Name = expression.Variable.Text,
            Type = expression.GetType()
        };

        _scope.AddVariable(variable);
        Token op = expression.Op;
        return new BoundAssignmentExpression(variable, op, e, expression.HasCompound);
    }

    private BoundExpression BindGroupedExpression(GroupedExpression expression)
    {
        return BindExpression(expression.Expression);
    }

    private BoundBinaryExpression BindBinaryExpression(BinaryExpression expression)
    {
        BoundExpression left = BindExpression(expression.Left);
        BoundExpression right = BindExpression(expression.Right);
        BoundBinaryOperator? op = BoundBinaryOperator.GetBinaryOperator(left.ValueType, expression.Op.Type, right.ValueType);
        return new BoundBinaryExpression(left, op, right);
    }

    private static BoundLiteralExpression BindLiteralExpression(LiteralExpression expression)
    {
        object value = expression.Value ?? 0;
        return new BoundLiteralExpression(value);
    }
}