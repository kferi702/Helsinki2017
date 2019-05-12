using System.Collections.Generic;

namespace Helsinki2017
{
    internal class Versenyzok
    {
        public string nev { get; set; }
        public string orszag { get; set; }
        public double tech { get; set; }
        public double komp { get; set; }
        public int hiba { get; set; }
        public double ossz { get; set; }

        public Versenyzok(string sor)
        {
            string[] t = sor.Split(';');
            this.nev = t[0];
            this.orszag = t[1];
            this.tech = double.Parse(t[2].Replace('.',','));
            this.komp = double.Parse(t[3].Replace('.', ','));
            this.hiba = int.Parse(t[4]);
            this.ossz = tech + komp - hiba;
        }


        public override string ToString()
        {
            return nev+";"+orszag+";"+tech+";"+komp+";"+hiba+";"+ossz;
        }
    }
}