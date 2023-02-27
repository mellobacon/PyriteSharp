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
            ("1.00", 1.0d),
            ("5 - 2", 3),
            ("5 - 2.5", 2.5d),
            ("6 % 2", 0),
            ("6 % 2.3", 1.4000000000000004d),
            ("6 % 2.3f", 1.4000001f),
            ("1 + 2 + 3", 6),
            ("1 + 2 * 3", 7),
            ("(1 + 2) * 3", 9),
            ("1 / 2", 0),
            ("1.0 / 2.0", 0.5d),
            ("1.0 / 2", 0.5d),
            ("0b_1111_0001 << 8", 0b1111000100000000),
            ("0b01 << 1", 0b10),
            ("0b010 >> 1", 0b001)
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
        Compilation compilation = new Compilation(tree.Root);
         Assert.Equal(value, compilation.Evaluate());
    }
}