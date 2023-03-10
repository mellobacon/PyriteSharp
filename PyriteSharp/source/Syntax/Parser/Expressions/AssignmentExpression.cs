namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class AssignmentExpression : Expression
{
    public readonly Token Variable;
    public readonly Token Op;
    public readonly Expression Expression;

    public readonly bool HasCompound;
    public AssignmentExpression(Token v, Token op, Expression expression, bool compound = false)
    {
        Variable = v;
        Op = op;
        Expression = expression;
        HasCompound = compound;
    }

    public override TokenType Type => TokenType.ASSIGNMENT_EXPRESSION;
    public override IEnumerable<Node> GetChildren()
    {
        yield return Variable;
        yield return Op;
        yield return Expression;
    }
}