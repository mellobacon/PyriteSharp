namespace PyriteSharp.source.Syntax.Binder;

public class BoundBlockStatement : BoundStatement
{
    public readonly List<BoundStatement> Statements;
    public BoundBlockStatement(List<BoundStatement> statements)
    {
        Statements = statements;
    }

    public override BoundType BoundType => BoundType.STATEMENT;
}