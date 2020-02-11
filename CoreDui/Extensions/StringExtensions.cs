using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreDui.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToLower(this string input)
        {
            switch(input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToLower() + input.Substring(1);
            }
        }
    }

}
