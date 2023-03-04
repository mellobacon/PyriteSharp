namespace PyriteSharp.source.Syntax.Parser.Expressions;

public class AssignmentExpression : Expression
{
    public Token Variable;
    public Token Op;
    public Expression Expression;
    public AssignmentExpression(Token v, Token op, Expression expression)
    {
        Variable = v;
        Op = op;
        Expression = expression;
    }

    public override TokenType Type => TokenType.ASSIGNMENT_EXPRESSION;
    public override IEnumerable<Node> GetChildren()
    {
        yield return Variable;
        yield return Op;
        yield return Expression;
    }
}