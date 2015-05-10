using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINKI_WebBrowser_v1._0
{
    class IstorijaAdresar
    {
        string adresa { get; set; }
        List<IstorijaAdresar> adresarIstorija;
        public IstorijaAdresar(string adresa)
        {
            this.adresa = adresa;
            adresarIstorija = new List<IstorijaAdresar>();
        }
        public override string ToString()
        {
            return string.Format("{0}",adresa);
        }
        public void Dodadi(IstorijaAdresar i)
        {
            adresarIstorija.Add(i);
        }
    }
}
