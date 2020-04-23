namespace dz2
{
    class Digit  //клас нечіткого числа
    {
        public double m { get; set; }
        public double a { get; set; }
        public double b { get; set; }
        public Digit(double m1, double a1, double b1)
        {
            m = m1;
            a = a1;
            b = b1;
        }
        public Digit(double m1)
        {
            m = m1;
            a = 0;
            b = 0;
        }
        public Digit(double m1, double a1)
        {
            m = m1;
            a = a1;
            b = 0;
        }
        public Digit (Digit d2)
        {
            m = d2.m;
            a = d2.a;
            b = d2.b;
        }
        public static Digit operator +(Digit d1, Digit d2)
        {
            return new Digit(d1.m + d2.m, d1.a + d2.a, d1.b + d2.b);
        }
        public static Digit operator -(Digit d1, Digit d2)
        {
            return new Digit(d1.m - d2.m, d1.a + d2.a, d1.b + d2.b);
        }
        public static Digit operator *(Digit d1, Digit d2)
        {
            return new Digit(d1.m * d2.m, (d1.m * d2.a) + (d2.m * d1.a), (d1.m * d2.b) + (d2.m * d1.b));
        }
    }
}
