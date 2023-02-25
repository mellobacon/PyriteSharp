using PyriteSharp.source.Syntax.Evaluator;
using PyriteSharp.source.Syntax.Parser;

Console.WriteLine("PyriteSharp v0.0.0");
while (true)
{
    Console.Write("> ");
    string? input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
    {
        break;
    }

    Ast tree = Ast.Parse(input);
    tree.PrintTree(tree.root);
    
    Compilation compilation = new Compilation(tree.root);
    Console.WriteLine(compilation.Evaluate());
}