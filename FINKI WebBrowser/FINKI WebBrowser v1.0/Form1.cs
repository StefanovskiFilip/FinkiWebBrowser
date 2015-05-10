using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FINKI_WebBrowser_v1._0
{

    public partial class Form1 : Form
    {
       public WebBrowser web = new WebBrowser();
        
        int i = 0;
        int brojacIstorija = 0;
        List<Istorija> istorija = new List<Istorija>();
        List<IstorijaAdresar> adresarIstorija = new List<IstorijaAdresar>();
        List<Omileni> omileni = new List<Omileni>();
        public Form1()
        {
            InitializeComponent();
            this.Text = "ФИНКИ Веб-Прелистувач";
        }
        private void prebarajKopce_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void prebarajKopce_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;

        }
        private void Nazad_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void Nazad_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            web = new WebBrowser();
            web.ScriptErrorsSuppressed = true;
            web.Dock = DockStyle.Fill;
            web.Visible = true;
            web.Navigate("http://www.finki.ukim.mk");
            web.DocumentCompleted += web_DocumentCompleted;
            tsPoleAdresa.Text = "http://www.finki.ukim.mk";
            tabControl1.TabPages.Add(this.web.DocumentTitle);
            tabControl1.SelectTab(i);
            tabControl1.SelectedTab.Controls.Add(web);
            i++;
        }
        void web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            tabControl1.SelectedTab.Text = ((WebBrowser)tabControl1.SelectedTab.Controls[0]).DocumentTitle;
        }
        private void историјаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("Историја на Прегледувачот");
            tabControl1.SelectTab(i);
            ListBox listaNaIstorija = new ListBox();
            listaNaIstorija.Dock = DockStyle.Fill;
            listaNaIstorija.Visible = true;

            tabControl1.SelectedTab.Controls.Add(listaNaIstorija);
            listaNaIstorija.Items.Clear();
            foreach (Istorija a in istorija)
            {
                listaNaIstorija.Items.Add(a);
            }
            i++;
        }
        public void DodajVoIstorija()
        {
            brojacIstorija++;
            string adresa = tsPoleAdresa.Text;
            web.DocumentCompleted+=web_DocumentCompleted;
            string naslov = this.web.DocumentTitle; 
            if (adresa != "")
            {
                istorija.Add(new Istorija(brojacIstorija,adresa,naslov));
                adresarIstorija.Add(new IstorijaAdresar(adresa));
            }
            tsIstorija.Items.Clear();
            foreach (Istorija i in istorija)
            {
                tsIstorija.Items.Add(i);
            }
        }
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedTab.Text != "Историја на Прегледувачот") && (tabControl1.SelectedTab.Text != "Листа на Омилени Веб-Страни"))
            {
                web.ProgressChanged += web_ProgressChanged;

                if (tsPoleAdresa.Text != "")
                {
                    ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(tsPoleAdresa.Text);
                    if (!tsPoleAdresa.Items.Contains(tsPoleAdresa.Text))
                    {
                        tsPoleAdresa.Items.Add(tsPoleAdresa.Text);
                    }
                    tabControl1.SelectedTab.Text = "";
                    tabControl1.SelectedTab.Text = this.web.DocumentTitle;
                    DodajVoIstorija();
                }
            }
        }
        void web_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            try
            {
                progressBar1.Value = Convert.ToInt32(e.CurrentProgress);
                progressBar1.Maximum = Convert.ToInt32(e.MaximumProgress);
            }
            catch (Exception)
            {
                
            }
        }
        
        private void tsNazad_Click(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedTab.Text != "Историја на Прегледувачот") && (tabControl1.SelectedTab.Text != "Листа на Омилени Веб-Страни"))
            {
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).GoBack();
                tsPoleAdresa.Text = "";
            }
        }
        private void tsNapred_Click(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedTab.Text != "Историја на Прегледувачот") && (tabControl1.SelectedTab.Text != "Листа на Омилени Веб-Страни"))
            {
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).GoForward();
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            web = new WebBrowser();
            web.ScriptErrorsSuppressed = true;
            web.Dock = DockStyle.Fill;
            web.Visible = true;
            tsPoleAdresa.Text = "";
            tabControl1.TabPages.Add("Нов Таб");
            web.DocumentCompleted += web_DocumentCompleted;
            tabControl1.SelectTab(i);
            tabControl1.SelectedTab.Controls.Add(web);
            i++;
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count - 1 > 0)
            {
                tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
                i--;
            }
        }
        private void tsPoleAdresa_KeyDown(object sender, KeyEventArgs e)
        {
            if ((tabControl1.SelectedTab.Text != "Историја на Прегледувачот") && (tabControl1.SelectedTab.Text != "Листа на Омилени Веб-Страни"))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    toolStripButton1_Click_1(null, null);
                    e.Handled = true;
                    tabControl1.SelectedTab.Text = this.web.DocumentTitle;
                    ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(tsPoleAdresa.Text);
                }
            }
        }
        private void новТабToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(null, null);
        }
        private void tsRefres_Click(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedTab.Text != "Историја на Прегледувачот") && (tabControl1.SelectedTab.Text != "Листа на Омилени Веб-Страни"))
            {
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(tsPoleAdresa.Text);
                tabControl1.SelectedTab.Text = this.web.DocumentTitle;
            }
        }
        private void tsPocetna_Click(object sender, EventArgs e)
        {
            if ((tabControl1.SelectedTab.Text != "Историја на Прегледувачот") && (tabControl1.SelectedTab.Text != "Листа на Омилени Веб-Страни"))
            {
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate("http://www.finki.ukim.mk");
                tsPoleAdresa.Text = "http://www.finki.ukim.mk";
            }
        }
        private void Nazad_MouseEnter_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void Nazad_MouseLeave_1(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void Napred_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;

        }
        private void Napred_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;

        }
        private void tsRefres_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;

        }
        private void tsRefres_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void tsPocetna_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void tsPocetna_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void tsPoleAdresa_Click(object sender, EventArgs e)
        {
            tsPoleAdresa.Items.Clear();
            foreach (IstorijaAdresar a in adresarIstorija)
            {
                tsPoleAdresa.Items.Add(a);
            }
            tsPoleAdresa.SelectAll();
        }
        private void toolStripButton1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void toolStripButton1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void toolStripButton3_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void toolStripButton3_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void toolStripButton4_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void toolStripButton4_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void toolStripDropDownButton1_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void toolStripDropDownButton1_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void прозорецToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }
        private void новТабToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click(null, null);
        }
        private void избришиТековенToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton4_Click(null, null);
        }


        private void излезиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
             if ((tabControl1.SelectedTab.Text != "Историја на Прегледувачот")&&(tabControl1.SelectedTab.Text != "Листа на Омилени Веб-Страни"))
             {
                 bool sePovtoruva = false;
                 string adresa = tsPoleAdresa.Text;
                 if (adresa != "")
                 {

                     foreach (Omileni o in omileni)
                     {
                         if (o.adresa == adresa)
                         {
                             System.Windows.Forms.MessageBox.Show("Веќе ја имате зачувано страната!", "Грешка при зачувување на Адресата", MessageBoxButtons.OK);
                             sePovtoruva = true;
                         }
                     }
                     if (!sePovtoruva)
                     {
                         omileni.Add(new Omileni(adresa));
                     }
                 }
             }
            
        }

        private void омилениСтраниToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Add("Листа на Омилени Веб-Страни");
            tabControl1.SelectTab(i);
            ListBox listaomileni = new ListBox();
            listaomileni.Dock = DockStyle.Fill;
            listaomileni.Visible = true;
            tabControl1.SelectedTab.Controls.Add(listaomileni);
            listaomileni.Items.Clear();
            foreach (Omileni a in omileni)
            {
                listaomileni.Items.Add(a);
            }
            i++;
        }
        private void ninjaModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 ninjaMode = new Form1();
            ninjaMode.toolStripDropDownButton1.Visible = false;
            ninjaMode.toolStripButton2.Visible = false;
            ninjaMode.Text = "Нинџа Мод";

            if (System.Windows.Forms.MessageBox.Show("Преминавте на Ninja Mode.Историјата нема да ви биде зачувана!", "Ninja Mode", MessageBoxButtons.OK) == System.Windows.Forms.DialogResult.OK)
            {
                ninjaMode.Show();
            }
        }
        private void новToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            f.ShowDialog();
        }
        private void toolStripButton2_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }
        private void toolStripButton2_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
        private void избришиЦелаИсторијаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count - 1 > 0)
            {
                tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
                i--;
            }
            istorija.Clear();
            историјаToolStripMenuItem_Click(null, null);
        }
        private void tsPoleAdresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tsPoleAdresa.SelectedIndex != -1)
            {
                string adresa = tsPoleAdresa.SelectedItem.ToString();
                ((WebBrowser)tabControl1.SelectedTab.Controls[0]).Navigate(adresa);
                tabControl1.SelectedTab.Text = this.web.DocumentTitle;
            }
        }
        private void избришиЛистаОмилениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.TabPages.Count - 1 > 0)
            {
                tabControl1.TabPages.RemoveAt(tabControl1.SelectedIndex);
                tabControl1.SelectTab(tabControl1.TabPages.Count - 1);
                i--;
            }
            omileni.Clear();
            омилениСтраниToolStripMenuItem_Click(null, null);
        } 
    }
}


















































































































































