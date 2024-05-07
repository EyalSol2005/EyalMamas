using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    public class NumericalExpressionEnglish
    {
        private long number;

        static Func<int, string>[] englishUnits = { _ => "", _ => "One", _ => "Two", _ => "Three", _ => "Four", _ => "Five", _ => "Six", _ => "Seven", _ => "Eight", _ => "Nine" };
        static Func<int, string>[] englishTeens = { _ => "Ten", _ => "Eleven", _ => "Twelve", _ => "Thirteen", _ => "Fourteen", _ => "Fifteen", _ => "Sixteen", _ => "Seventeen", _ => "Eighteen", _ => "Nineteen" };
        static Func<int, string>[] englishTens = { _ => "", _ => "Ten", _ => "Twenty", _ => "Thirty", _ => "Forty", _ => "Fifty", _ => "Sixty", _ => "Seventy", _ => "Eighty", _ => "Ninety" };
        static string[] englishThousands = { "", "Thousand", "Million", "Billion", "Trillion" };

        public NumericalExpressionEnglish(long number)
        {
            this.number = number;
        }

        public override string ToString()
        {
            return new NumericalExpression(this.number, englishUnits, englishTeens, englishTens, englishThousands).ToString();
        }

        public static int SumLetters(long number)
        {
            return NumericalExpression.SumLetters(number, englishUnits, englishTeens, englishTens, englishThousands);
        }
    }
}
