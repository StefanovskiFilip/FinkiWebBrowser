using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINKI_WebBrowser_v1._0
{
    public class Istorija
    {
        string adresa { get; set; }
        string naslov { get; set; }
        int brojac { get; set; }
        List<Istorija> istorija;
        public Istorija(int brojac,string adresa,string naslov)
        {
            this.adresa = adresa;
            this.naslov = naslov;
            istorija = new List<Istorija>();
            this.brojac = brojac;
            
        }
        public override string ToString()
        {
            return string.Format("{0}.   {1:HH:mm:ss tt}                   -                   {2}", brojac.ToString(), DateTime.Now, adresa);
        }
        public void Dodadi(Istorija i)
        {
            istorija.Add(i);
        }
    }
}
