using System;
using FractionData;

namespace FractionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Fraction.Info());

            Fraction a = Fraction.Half;
            Fraction b = Fraction.Quarter;
            Console.WriteLine((a + b).ToString());
            Console.WriteLine((a - b).ToString());
            Console.WriteLine((a * b).ToString());
            Console.WriteLine((a / b).ToString());

            var r = (double)Fraction.Half;
            Console.WriteLine(r.ToString());
            Fraction c = 2;
            Console.WriteLine(c.ToString());

            Fraction[] table = new Fraction[10];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = new Fraction(1, i + 1);
            }            
            Console.WriteLine("Before sorting");
            foreach (Fraction u in table)
            {
                Console.WriteLine(u.ToString() + "=" + u.ToDouble().ToString());
            }           
            Array.Sort(table);
            Console.WriteLine("After sorting");
            foreach (Fraction u in table)
            {
                Console.WriteLine(u.ToString() + "=" + u.ToDouble().ToString());
            }

            Fraction extension = new Fraction(3,4);
            (var intPartOfFraction, var numerator, var denominator) = Extension.Extend(extension);
            Console.WriteLine($"The whole part of the fraction {extension} = {intPartOfFraction}\nNumerator = {numerator}\nDenominator = {denominator}");          
        }
    }
}
