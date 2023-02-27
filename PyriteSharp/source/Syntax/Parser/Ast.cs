using PyriteSharp.source.Syntax.Parser.Expressions;

namespace PyriteSharp.source.Syntax.Parser;

public class Ast
{
    public readonly Expression Root;
    public Ast(Expression expression)
    {
        Root = expression;
    }

    public static Ast Parse(string text)
    {
        Parser parser = new Parser(text);
        return parser.Parse();
    }

    public void PrintTree(Node node, string indent = "", bool islast = true)
    {
        string marker = islast ? "└──" : "├──";
        Console.Write(indent);
        Console.Write(marker);
        
        switch (node.Type)
        {
            case TokenType.BINARY_EXPRESSION: 
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(node.Type);
                Console.ResetColor();
                break;
            case TokenType.LITERAL_EXPRESSION:
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(node.Type);
                Console.ResetColor();
                break;
            case TokenType.STAR:
            case TokenType.SLASH:
            case TokenType.PLUS:
            case TokenType.MINUS:
            case TokenType.MOD: 
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(node.Type);
                Console.ResetColor();
                break;
            default:
                Console.ResetColor();
                Console.Write(node.Type);
                break;
        }

        // Print numbers / token text
        if (node is Token token)
        {
            if (token.Value is not null)
            {
                Console.Write(" ");
                Console.Write(token.Value);
            }
            else
            {
                Console.Write(" ");
                Console.Write(token.Text);
            }
        }

        Console.WriteLine();
        indent += islast ? "   " : "│  ";
        Node? last = node.GetChildren().LastOrDefault();
        foreach (Node child in node.GetChildren())
        {
            PrintTree(child, indent, child == last);
        }
    }
}