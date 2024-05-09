using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    public class NumericalExpression
    {
        private long number;
        private Func<int, string>[] units;
        private Func<int, string>[] teens;
        private Func<int, string>[] tens;
        private string[] thousands;

        public NumericalExpression(long number, Func<int, string>[] units, Func<int, string>[] teens, Func<int, string>[] tens, string[] thousands)
        {
            this.number = number;
            this.units = units;
            this.teens = teens;
            this.tens = tens;
            this.thousands = thousands;
        }

        public override string ToString()
        {
            if (this.number == 0)
                return "Zero";

            int chunkCount = 0;
            string result = "";

            while (this.number > 0) // looping through all the 3-4 digits chunks
            {
                if (this.number % 1000 != 0)
                {
                    string chunk = ConvertChunkToString((int)(this.number % 1000));
                    result = chunk + thousands[chunkCount] + " " + result; // adding current chunk to start
                }
                this.number /= 1000;
                chunkCount++;
            }

            return result.Trim();
        }

        /// <summary>
        /// The function converts the current chunk into a string representation of it
        /// </summary>
        /// <param name="chunk">Current chunk to convert</param>
        /// <returns>String representation of it</returns>
        private string ConvertChunkToString(int chunk)
        {
            string result = "";

            if (number >= 100) // chunk consists of hundreds
            {
                result += units[chunk / 100](chunk / 100) + " Hundred ";
                chunk %= 100;
            }

            if (number >= 20) // chunk consists of tens
            {
                result += tens[chunk / 10](chunk / 10) + " ";
                chunk %= 10;
            }

            if (number >= 10 && number < 20) // chunk consists of teens
            {
                result += teens[chunk - 10](chunk - 10) + " ";
                chunk = 0;
            }

            if (chunk > 0) // chunk consists of units
            {
                result += units[chunk](chunk) + " ";
            }

            return result;
        }

        public long GetValue()
        {
            return this.number;
        }

        /// <summary>
        /// The function sums up all the letters needed to represent all the numbers from 0 to the given number in their word represention
        /// </summary>
        /// <param name="number">Number to check</param>
        /// <param name="units">Function to handle units</param>
        /// <param name="teens">Function to handle teens</param>
        /// <param name="tens">Function to handle tens</param>
        /// <param name="thousands">Array of the thousands</param>
        /// <returns></returns>
        public static int SumLetters(long number, Func<int, string>[] units, Func<int, string>[] teens, Func<int, string>[] tens, string[] thousands)
        {
            int sum = 0;

            for (long i = 0; i <= number; i++)
            {
                sum += new NumericalExpression(i, units, teens, tens, thousands).ToString().Replace(" ", "").Length;
            }

            return sum;
        }

        // The programming concept here is called "Method Overloading"
        public static int SumLetters(NumericalExpression expression, Func<int, string>[] units, Func<int, string>[] teens, Func<int, string>[] tens, string[] thousands)
        {
            int sum = 0;

            for (long i = 0; i <= expression.GetValue(); i++)
            {
                sum += new NumericalExpression(i, units, teens, tens, thousands).ToString().Replace(" ", "").Length;
            }

            return sum;
        }
    }
}
