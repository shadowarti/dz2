using System;

namespace dz2
{
    class Threat
    {
        public Digit[] chance_d { get; set; } //ймовірність загрози
        public String name { get; set; } //назва загрози
        public Digit rank { get; set; }  //ранг загрози
        public Threat(String name1, Digit[] d)
        {
            name = name1;
            chance_d = new Digit[d.Length];
            for (int i = 0; i < chance_d.Length; i++)
            {
                chance_d[i] = d[i];
            }
        }
    }
}
