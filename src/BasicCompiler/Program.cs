namespace BasicCompiler
{
    using System;
    using System.IO;
    using BasicCompiler.Core;

    public class Program
    {
        private static void Main(string[] args)
        {
            string text = File.ReadAllText(args[0]);

            // Expected format of code:
            // '(add (subtract 4 2) 2)'

            var lexed = Lexer.Lex(text);

            /*
            var parsed = Parse(lexed);
            var transformed = Transform(parsed);
            var output = GenerateCode(transformed);

            Console.WriteLine(output);
            */
        }
    }
}
