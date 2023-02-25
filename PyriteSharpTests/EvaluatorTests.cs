using System.Collections.Generic;
using PyriteSharp.source.Syntax.Evaluator;
using PyriteSharp.source.Syntax.Parser;
using Xunit;

namespace PyriteSharpTests;

public class EvaluatorTests
{
    private static IEnumerable<object[]> GetEvaluations()
    {
        var evals = new (string text, object value)[]
        {
            ("100", 100),
            ("5 - 2", 3),
            ("6 % 2", 0),
            ("1 + 2 + 3", 6),
            ("1 + 2 * 3", 7),
            ("(1 + 2) * 3;", 9),
            ("1 / 2", 1),
            ("1 % 2", 1),
        };
        foreach ((string text, object value) in evals)
        {
            yield return new[] { text, value };
        }
    }

    [Theory]
    [MemberData(nameof(GetEvaluations))]
    private void EvaluatorCalcuatesCorrectly(string text, object value)
    {
        Ast tree = Ast.Parse(text);
        Compilation compilation = new Compilation(tree.root);
         Assert.Equal(value, compilation.Evaluate());
    }
}