using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Grenaders.Editor.CodeGeneration.MyCodeGenerators
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null:
                    throw new ArgumentNullException(nameof(input));
                case "":
                    throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default:
                    return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }

        public static string ToPascalSnake(this string input)
        {
            switch (input)
            {
                case null:
                    throw new ArgumentNullException(nameof(input));
                case "":
                    throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default:
                    var inputStringsArray = input.Split(' ').ToList();
                    var allSnakeWords = inputStringsArray.Select(s => "_" + s.FirstCharToUpper());
                    return string.Join("", allSnakeWords);
            }
        }
    }
}