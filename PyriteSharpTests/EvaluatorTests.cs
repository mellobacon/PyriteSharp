﻿using System.Collections.Generic;
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
            ("1.00", 1.0f),
            ("5 - 2", 3),
            ("5 - 2.5", 2.5f),
            ("6 % 2", 0),
            ("6 % 2.3", 1.4f),
            ("1 + 2 + 3", 6),
            ("1 + 2 * 3", 7),
            ("(1 + 2) * 3", 9),
            ("1 / 2", 0),
            ("1.0 / 2.0", 0.5f),
            ("1.0 / 2", 0.5f),
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