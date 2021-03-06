using Microsoft.VisualStudio.TestTools.UnitTesting;
using FractionData;
using System;

namespace FractionUnitTest
{
    [TestClass]
    public class Tests
    {
        Random r = new Random();
        const int repetitionsNumber = 100;

        private int drawInteger(int? maximumAbsoluteValue = null)
        {
            if (!maximumAbsoluteValue.HasValue)
                return r.Next(int.MinValue, int.MaxValue);
            else
            {
                maximumAbsoluteValue = Math.Abs(maximumAbsoluteValue.Value);
                return r.Next(-maximumAbsoluteValue.Value, maximumAbsoluteValue.Value);
            }
        }
        private int drawIntegerOtherThanZero(int? maximumAbsoluteValue = null)
        {
            int value;
            do
            {
                value = drawInteger(maximumAbsoluteValue);
            }
            while (value == 0);
            return value;
        }
        private (int numerator, int denominator, int divider) drawCommonDivisor(int number, int number2)
        {
            var limit = (int)(Math.Sqrt(int.MaxValue / 2) - 1);
            while (number != number2)
            {
                if (number > number2)
                    number -= number2;
                else
                    number2 -= number;
            }
            var divider = number;
            var numerator = number;
            var denominator = number2;
            return (numerator, denominator, divider);
        }

        [TestMethod]
        public void TestSimplifyMethod_Random()
        {
            for (int i = 0; i < repetitionsNumber; i++)
            {
                Fraction fraction = new Fraction(drawInteger(), drawIntegerOtherThanZero());
                Fraction copy = fraction;

                fraction.Simplify();

                Assert.IsTrue(fraction.Denominator > 0);
                Assert.AreEqual(copy.ToDouble(), fraction.ToDouble());
            }
        }

        [TestMethod]
        public void SortTest()
        {
            Fraction[] table = new Fraction[100];
            for (int i = 0; i < table.Length; i++)
            {
                table[i] = new Fraction(drawInteger(), drawIntegerOtherThanZero());
            }
            
            Array.Sort(table);
            var tableIsSorted = true;
            for (int i = 0; i < table.Length-1; i++)
            {
                if (table[i] > table[i + 1])
                    tableIsSorted = false;
            }

            Assert.IsTrue(tableIsSorted);
        }

        [TestMethod]
        public void TestConversionToDouble()
        {
            for (int i = 0; i < repetitionsNumber; i++)
            {
                var numerator = drawInteger();
                var denominator = drawIntegerOtherThanZero();
                Fraction fraction = new Fraction(numerator, denominator);
                
                var d = fraction.ToDouble();

                Assert.AreEqual(numerator / (double)denominator, d);              
            }
        }

        [TestMethod]
        public void TestConversionFromInt()
        {
            for (int i = 0; i < repetitionsNumber; i++)
            {
                var numerator = drawInteger();
                Fraction fraction = numerator;

                Assert.AreEqual(numerator, fraction.Numerator);
                Assert.AreEqual(1, fraction.Denominator);
            }
        }
        
        
        [TestMethod]
        public void TestOfOperators_Random()
        {
            var limit = (int)(Math.Sqrt(int.MaxValue / 2) - 1);
            const double accuracy = 1E-10;
            for (int i = 0; i < repetitionsNumber; i++)
            {
                Fraction a = new Fraction(drawInteger(limit), drawIntegerOtherThanZero(limit));
                Fraction b = new Fraction(drawInteger(limit), drawIntegerOtherThanZero(limit));

                var total = (a + b).ToDouble();
                var difference = (a - b).ToDouble();
                var product = (a * b).ToDouble();
                var quotient = (a / b).ToDouble();

                Assert.AreEqual(a.ToDouble() + b.ToDouble(), total, accuracy);
                Assert.AreEqual(a.ToDouble() - b.ToDouble(), difference, accuracy);
                Assert.AreEqual(a.ToDouble() * b.ToDouble(), product, accuracy);
                Assert.AreEqual(a.ToDouble() / b.ToDouble(), quotient, accuracy);
            }
        }

        [TestMethod]
        public void TestOfConstructorAndProperties()
        {
            var numerator = 1;
            var denominator = 2;

            Fraction fraction = new Fraction(numerator, denominator);

            Assert.AreEqual(numerator, fraction.Numerator, "Incompatibility in the numerator");
            Assert.AreEqual(denominator, fraction.Denominator, "Incompatibility in the denominator");
        }

        [TestMethod]
        public void TestOfConstructor()
        {
            var numerator = 1;
            var denominator = 2;
            Fraction fraction = new Fraction(numerator, denominator);
            PrivateObject po = new PrivateObject(fraction);

            var fraction_numerator = fraction.Numerator;
            var fraction_denominator = (int)po.GetField("denominator");

            Assert.AreEqual(numerator, fraction_numerator, "Incompatibility in the numerator");
            Assert.AreEqual(denominator, fraction_denominator, "Incompatibility in the denominator");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestOfConstructorException()
        {
            Fraction fraction = new Fraction(1, 0);
        }

        [TestMethod]
        public void TestOfHalfStaticField()
        {
            Fraction fraction = Fraction.Half;
            
            Assert.AreEqual(1, fraction.Numerator);
            Assert.AreEqual(2, fraction.Denominator);
        }

        [TestMethod]
        public void TestSimplifyMethod()
        {
            Fraction fraction = new Fraction(4, -2);
            
            fraction.Simplify();
            
            Assert.AreEqual(-2, fraction.Numerator);
            Assert.AreEqual(1, fraction.Denominator);
        }

        [TestMethod]
        public void TestOfOperators()
        {
            Fraction a = Fraction.Half;
            Fraction b = Fraction.Quarter;

            Assert.AreEqual(new Fraction(3, 4), a + b, "Failure while adding");
            Assert.AreEqual(Fraction.Quarter, a - b, "Failure while subtracting");
            Assert.AreEqual(new Fraction(1, 8), a * b, "Failure while multiplication");
            Assert.AreEqual(new Fraction(2), a / b, "Failure while dividing");
        }
        #region Conversion tests to numeric types
        [TestMethod]
        public void TestOfConversionOnDecimal()
        {
            Fraction fraction = Fraction.Half;
            
            Assert.AreEqual(0.5M, Convert.ToDecimal(fraction));
        }
        [TestMethod]
        public void TestOfConversionOnInt16()
        {
            Fraction fraction = Fraction.One;
            
            Assert.AreEqual(1, Convert.ToInt16(fraction));
        }

        [TestMethod]
        public void TestOfConversionOnInt32()
        {
            Fraction fraction = Fraction.One;
            
            Assert.AreEqual(1, Convert.ToInt32(fraction));
        }

        [TestMethod]
        public void TestOfConversionOnInt64()
        {
            Fraction fraction = Fraction.Half;
            
            Assert.AreEqual(0L, Convert.ToInt64(fraction));
        }

        [TestMethod]
        public void TestOfConversionOnByte()
        {
            Fraction fraction = Fraction.One;
            
            Assert.AreEqual(1, Convert.ToByte(fraction));
        }

        [TestMethod]
        public void TestOfConversionOnDobule()
        {
            Fraction fraction = Fraction.Half;
            
            Assert.AreEqual(0.5, Convert.ToDouble(fraction));
        }

        [TestMethod]
        public void TestOfConversionOnSbyte()
        {
            Fraction fraction = Fraction.One;
            
            Assert.AreEqual(1, Convert.ToSByte(fraction));
        }

        [TestMethod]
        public void TestOfConversionOnFloat()
        {
            Fraction fraction = Fraction.Half;
            
            Assert.AreEqual(0.5f, Convert.ToSingle(fraction));
        }
        [TestMethod]
        public void TestOfConversionOnUInt16()
        {
            Fraction fraction = Fraction.Half;
            
            Assert.AreEqual(0, Convert.ToInt16(fraction));
        }

        [TestMethod]
        public void TestOfConversionOnUInt32()
        {
            Fraction fraction = Fraction.Half;
            
            Assert.AreEqual(0, Convert.ToInt32(fraction));
        }

        [TestMethod]
        public void TestOfConversionOnUInt64()
        {
            Fraction fraction = Fraction.Half;
            
            Assert.AreEqual(0, Convert.ToInt64(fraction));
        }
        #endregion

        #region ExceptionTests

        [ExpectedException(typeof(NotImplementedException))]
        [TestMethod]
        public void TestOfConversionOnBool()
        {
            Fraction fraction = Fraction.One;
            
            Convert.ToBoolean(fraction);
        }

        [ExpectedException(typeof(NotImplementedException))]
        [TestMethod]
        public void TestOfConversionOnChar()
        {
            Fraction fraction = Fraction.One;
            
            Convert.ToChar(fraction);
        }

        [ExpectedException(typeof(NotImplementedException))]
        [TestMethod]
        public void TestOfConversionOnDateTime()
        {
            Fraction fraction = Fraction.One;
            
            Convert.ToDateTime(fraction);
        }

        [ExpectedException(typeof(NotImplementedException))]
        [TestMethod]
        public void TestOfConversionOnString()
        {
            Fraction fraction = Fraction.One;
            
            Convert.ToString(fraction);
        }
        #endregion
    }
}
