namespace BasicCompiler
{
    using System;
    using System.IO;
    using BasicCompiler.Core;

    public class Program
    {
        // TODO: .exe needs to be tested?
        private static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Usage: BasicCompiler <file>");
                return 1;
            }

            string text = File.ReadAllText(args[0]);

            // Expected format of code:
            // '(add (subtract 4 2) 2)'

            var lexed = Lexer.Lex(text);
            var parsed = Parser.Parse(lexed);
            var transformed = new ExpressionStatementTransform().Apply(parsed);
            var output = CodeGenerator.Stringify(transformed);

            // Sample output:
            // 'add(subtract(4, 2), 2)'

            Console.WriteLine(output);

            return 0;
        }
    }
}
