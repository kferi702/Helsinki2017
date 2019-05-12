using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helsinki2017
{
    class Program
    {
        public static List<Versenyzok> rovid = new List<Versenyzok>();
        public static List<Versenyzok> donto = new List<Versenyzok>();
        public static string keres;
        public static void beolv(List<Versenyzok> list_name, string f_neve)
        {
            FileStream f = new FileStream(@f_neve, FileMode.Open);
            StreamReader r = new StreamReader(f, Encoding.UTF8);
            r.ReadLine();
            while (!r.EndOfStream)
                list_name.Add(new Versenyzok(r.ReadLine().ToString()));
            r.Close();
            f.Close();
        }
        public static void f3()
        {
            bool van = false;
            foreach (Versenyzok d in donto)
                if (d.orszag == "HUN")
                {
                    van = true;
                    break;
                }
            if (van)
                Console.WriteLine("4. feladat\n\tA magyar versenyző bejutott a kűrtbe!");
            else
                Console.WriteLine("4. feladat\n\tA magyar versenyző NEM jutott be a kűrtbe!");
        }
        public static double OsszPontszam(string nev)
        {
            double sum = 0;
            foreach (Versenyzok r in rovid)
                if (r.nev == nev)
                    sum += (double)r.ossz;
            foreach (Versenyzok d in donto)
                if (d.nev == nev)
                    sum += (double)d.ossz;
            return sum;
        }
        public static void f5()
        {
            Console.Write("5. feladat\n\tKérem a versenyző nevét:");
            keres = Console.ReadLine();
            foreach (Versenyzok r in rovid)
                if (r.nev == keres)
                    return;
            Console.WriteLine("\tIlyen nevű induló nem volt!");
            keres = "";
        }
        public static void f6()
        {
            if (keres != "") Console.WriteLine("6. feladat\n\tA versenyző összpontszáma: " + OsszPontszam(keres));

        }
        public static void f7()
        {
            Console.WriteLine("7. feladat");
            Dictionary<string, int> dontoben = new Dictionary<string, int>();
            foreach (Versenyzok d in donto)
            {
                if (dontoben.Keys.Contains(d.orszag))
                    dontoben[d.orszag]++;
                else
                    dontoben.Add(d.orszag, 1);
            }
            foreach (var d in dontoben) if (d.Value > 1) Console.WriteLine(d.Key + ": " + d.Value + " versenyző");
        }
        public static void f8()
        {
            FileStream f = new FileStream("vegeredmeny.csv", FileMode.Create);
            StreamWriter w = new StreamWriter(f, Encoding.Default);

            Dictionary<string, double> osszes = new Dictionary<string, double>();
            foreach (Versenyzok r in rovid)
            {
                osszes.Add((r.nev + ";" + r.orszag + ";"),OsszPontszam(r.nev));
            }
            int i = 1;
            foreach (var o in osszes.OrderByDescending(x=>x.Value))
            {
                w.WriteLine(i + ";" +o.Key+";"+o.Value);
                i++;
            }
            w.Close();
            f.Close();
        }
        static void Main(string[] args)
        {
            beolv(rovid, "rovidprogram.csv");
            beolv(donto, "donto.csv");
            Console.WriteLine("2. feladat\n\tA rövidprogramban " + rovid.Count + " induló volt");
            f3();
            f5();
            f6();
            f7();
            f8();
            Console.ReadKey();
        }
    }
}
