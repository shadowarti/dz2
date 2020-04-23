using System;

namespace dz2
{
    class Program
    {
        static void Main(string[] args)
        {
            //назви ресурсів
            String[] rec = new String[] { "Навчальнi матерiали",
                                          "Резервнi копiї",
                                          "Файли користувачiв",
                                          "Логи" };
            //назви загроз
            String[] thr = new String[] {"Неправильне налаштування прав доступу",
                                         "Випадкове видалення користувачем",
                                         "Збiй електропостачання",
                                         "Зараження ПК шкiдливим ПЗ",
                                         "Збiй у роботi ПЗ",
                                         "Пошкодження мережевих кабелiв",
                                         "Видалення файлу вiртуальної машини",
                                         "Збiй годинника на серверi",
                                         "Передчасний вихiд з ладу жорсткого диску"};

            Digit[] rec_ch = new Digit[] { new Digit(0.3), new Digit(0.38), new Digit(0.2), new Digit(0.12) }; //ймовірність атаки на ресурс

            Digit[][] thr_ch = new Digit[9][];
            //ймовірності з таблиці
            thr_ch[0] = new Digit[] { new Digit(0.1),        new Digit(0.05),           new Digit(0),             new Digit(0.07) };
            thr_ch[1] = new Digit[] { new Digit(0),          new Digit(0),              new Digit(0.1),           new Digit(0) };
            thr_ch[2] = new Digit[] { new Digit(0.2),        new Digit(0.06, 0, 0.01),  new Digit(0.3),           new Digit(0.03) };
            thr_ch[3] = new Digit[] { new Digit(0.08),       new Digit(0.04),           new Digit(0.09, 0, 0.01), new Digit(0.02) };
            thr_ch[4] = new Digit[] { new Digit(0),          new Digit(0.05),           new Digit(0.1),           new Digit(0.02) };
            thr_ch[5] = new Digit[] { new Digit(0.15, 0.01), new Digit(0.07),           new Digit(0),             new Digit(0.04) };
            thr_ch[6] = new Digit[] { new Digit(0),          new Digit(0),              new Digit(0.04),          new Digit(0) };
            thr_ch[7] = new Digit[] { new Digit(0),          new Digit(0.04),           new Digit(0),             new Digit(0.03) };
            thr_ch[8] = new Digit[] { new Digit(0.05),       new Digit(0.03),           new Digit(0.1),           new Digit(0.02) };

            Resource[] rec_list = new Resource[rec.Length];
            for (int i=0; i<rec_list.Length;i++)
            {
                rec_list[i] = new Resource(rec[i], rec_ch[i]);
            }

            Threat[] thr_list = new Threat[thr.Length];
            for(int i=0; i<thr_list.Length;i++)
            {
                thr_list[i] = new Threat(thr[i],thr_ch[i]);
            }

            Digit result = new Digit(0);

            for (int i=0; i<rec_list.Length; i++)
            {
                Digit b = new Digit(1);
                for(int j=0; j<thr_list.Length; j++)
                {
                    b *= (new Digit(1) - thr_list[j].chance_d[i]);
                }
                Digit a = new Digit(1);
                a -= b;
                result += (rec_list[i].attack_ch * a); //ймовірність атаки вцілому
             }

            for(int x=0; x <thr_list.Length;x++)
            {
                Digit s = new Digit(0);

                for (int i = 0; i < rec_list.Length; i++)
                {
                    Digit b = new Digit(1);
                    for (int j = 0; j < thr_list.Length; j++)
                    {
                        if (j != x) //не враховуємо певну загрозу
                            b *= (new Digit(1) - thr_list[j].chance_d[i]);
                    }
                    Digit a = new Digit(1);
                    a -= b;
                    s += (rec_list[i].attack_ch * a);
                }             
                thr_list[x].rank = result - s; //визначення рангу загрози
            }

            Console.WriteLine("Ймовiрнiсть атаки складає "+ String.Format("{0:#,0.000}", result.m) 
                + "  a: " + String.Format("{0:#,0.000}", result.a) 
                + "  b: " + String.Format("{0:#,0.000}", result.b));
            
            Array.Sort(thr_list,
            delegate (Threat x, Threat y) { return x.rank.m.CompareTo(y.rank.m); }); //сортуємо в порядку зростання

            Array.Reverse(thr_list); //робимо реверс, тоді матимемо ранги в порядку спадання

            for (int i=0; i<thr_list.Length;i++)
            {
                int k = i + 1;
                Console.WriteLine(k + "-й ранг загрози   " + 
                    String.Format("{0:#,0.000}", thr_list[i].rank.m) + "  a: " + 
                    String.Format("{0:#,0.000}", thr_list[i].rank.a) + "  b: " +
                    String.Format("{0:#,0.000}", thr_list[i].rank.b)+
                    "   <<" + thr_list[i].name + ">> ");
            }
            Console.ReadKey();
        }
    }
}
