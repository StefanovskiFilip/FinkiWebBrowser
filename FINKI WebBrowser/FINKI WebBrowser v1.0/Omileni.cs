using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINKI_WebBrowser_v1._0
{
    public class Omileni
    {
        public string adresa { get; set; }
        List<Omileni> omileni;
        public Omileni(string adresa)
        {
            this.adresa = adresa;
            omileni = new List<Omileni>();
        }
        public override string ToString()
        {
            return string.Format("{0}", adresa);
        }
        public void Dodadi(Omileni i)
        {
            omileni.Add(i);
        }

    }
}
