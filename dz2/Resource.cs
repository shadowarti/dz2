using System;

namespace dz2
{
    class Resource
    {
        public Digit attack_ch { get; set; } //ймовірність вектора атаки на ресурс
        public String name { get; set; }
        public Resource(String name1, Digit chance)
        {
            attack_ch = chance;
            name = name1;
        }
    }
}
