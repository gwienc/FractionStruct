using System;

namespace FractionData
{
    public static class Extension
    {
        public static (int intPartOfFraction, int numerator, int denominator) Extend(this Fraction fraction)
        {
            int intPartOfFraction;
            if (fraction.Numerator<fraction.Denominator)
            {
                return (intPartOfFraction = 0,(int)fraction.Numerator,(int)fraction.Denominator);
            }
            else
            {
                intPartOfFraction = fraction.Numerator / fraction.Denominator;
                fraction.Numerator = fraction.Numerator % fraction.Denominator;
                return (intPartOfFraction, (int)fraction.Numerator,(int)fraction.Denominator);
            }
        }
    }
   public struct Fraction: IComparable<Fraction>,IConvertible
    {
        private int denominator;
        public Fraction(int numerator, int denominator = 1): this()
        {
            Numerator = numerator;
            Denominator = denominator;
        }

        #region Properties
        public int Numerator { get; set; }
        public int Denominator
        {
            get
            {
                return denominator;
            }
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException("Mianownik musi być większy od zera");
                }
                else
                {
                    denominator = value;
                }
            }
        }
        #endregion

        public static readonly Fraction Zero = new Fraction(0);
        public static readonly Fraction One = new Fraction(1);
        public static readonly Fraction Half = new Fraction(1, 2);
        public static readonly Fraction Quarter = new Fraction(1, 4);

        public static string Info()
        {
            return "Struktura Ułamek";
        }

        public override  string ToString()
        {
            return Numerator.ToString() + "/" + Denominator.ToString();
        }

        public  double ToDouble()
        {
            return Numerator / (double)Denominator;
        }

        public void Simplify()
        {
            int a = Numerator;
            int b = Denominator;
            int c;

            while (b != 0)
            {
                c = a % b;
                a = b;
                b = c;
            }
            Numerator /= a;
            Denominator /= a;

            if ((Numerator < 0 && denominator > 0) || (Numerator >= 0 && denominator < 0))
            {
                Numerator = -Math.Abs(Numerator);
                Denominator = Math.Abs(Denominator);
            }
            else
            {
                Numerator = Math.Abs(Numerator);
                Denominator = Math.Abs(Denominator);
            }
        }
    

        #region Arithmetic operators
        public static Fraction operator -(Fraction u)
        {
            return new Fraction(-u.Numerator, u.Denominator);
        }
        public static Fraction operator +(Fraction u)
        {
            return u;
        }
        public static Fraction operator +(Fraction u1, Fraction u2)
        {
            Fraction result = new Fraction(u1.Numerator * u2.Denominator + u2.Numerator * u1.Denominator, u1.Denominator * u2.Denominator);
            result.Simplify();
            return result;
        }
        public static Fraction operator -(Fraction u1, Fraction u2)
        {
            Fraction result = new Fraction(u1.Numerator * u2.Denominator - u2.Numerator * u1.Denominator, u1.Denominator * u2.Denominator);
            result.Simplify();
            return result;
        }
        public static Fraction operator *(Fraction u1, Fraction u2)
        {
            Fraction result = new Fraction(u1.Numerator * u2.Numerator, u1.Denominator * u2.Denominator);
            result.Simplify();
            return result;
        }
        public static Fraction operator /(Fraction u1, Fraction u2)
        {
            Fraction result = new Fraction(u1.Numerator * u2.Denominator, u1.Denominator * u2.Numerator);
            result.Simplify();
            return result;
        }
        #endregion

        #region Comparison operators
        public static bool operator ==(Fraction u1, Fraction u2)
        {
            return (u1.ToDouble() == u2.ToDouble());
        }
        public static bool operator !=(Fraction u1, Fraction u2)
        {
            return !(u1 == u2);
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Fraction)) return false;
            Fraction u = (Fraction)obj;
            return (this == u);   
        }
        public override int GetHashCode()
        {
            return Numerator ^ Denominator;
        }

        public int CompareTo(Fraction u)
        {
            double difference = this.ToDouble() - u.ToDouble();
            return Math.Sign(difference);
        }

        public static bool operator >(Fraction u1, Fraction u2)
        {
            return (u1.ToDouble() > u2.ToDouble());
        }
        public static bool operator <(Fraction u1, Fraction u2)
        {
            return (u1.ToDouble() < u2.ToDouble());
        }
        public static bool operator >=(Fraction u1, Fraction u2)
        {
            return (u1.ToDouble() >= u2.ToDouble());
        }
        public static bool operator <=(Fraction u1, Fraction u2)
        {
            return (u1.ToDouble() <= u2.ToDouble());
        }
        #endregion
        
        #region Conversions
        public static explicit operator double(Fraction u)
        {
            return u.ToDouble();
        }
        public static implicit operator Fraction(int n)
        {
            return new Fraction(n);
        }
        public static Fraction DodajOvf(Fraction u1, Fraction u2)
        {
            Fraction result = new Fraction(checked(u1.Numerator * u2.Denominator + u2.Numerator * u1.Denominator), checked(u1.Denominator * u2.Denominator));
            result.Simplify();
            return result;
        }
       
        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            return (byte)(Numerator / Denominator);
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return (Numerator / (decimal)Denominator);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return (Numerator / (double)Denominator);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return (short)(Numerator / Denominator);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return (int)(Numerator / Denominator);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return (long)(Numerator / Denominator);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return (sbyte)(Numerator / Denominator);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return (Numerator / (float)Denominator);
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return (ushort)(Numerator / Denominator);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return (uint)(Numerator / Denominator);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return (ulong)(Numerator / Denominator);
        }
        #endregion
    }
}
