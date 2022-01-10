using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Nagy_Laszlo_11B_Projekt2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Verda verda1;
        Verda verda2;
        Verda verda3;
        Verseny verseny;
        DispatcherTimer idozito;
        szelepKupa szelepkupa;
        Random rnd = new Random();
        bool mehet = false;
        double celErtek;

        public MainWindow()
        {
            InitializeComponent();
            celErtek = 890;
            szelepkupa = new szelepKupa();

            idozito = new DispatcherTimer();
            idozito.Interval = TimeSpan.FromMilliseconds(30);
            idozito.Start();
            idozito.Tick += Tick;
            ujFutamGomb.IsEnabled = false;
            ujBajnoksagGomb.IsEnabled = false;

            verseny = new Verseny();
            verda1 = new Verda("verda1", mcQueen, verseny, eredmenyekCimke, palya1, elsoPalyaHelyCimke);
            verda2 = new Verda("verda2", Matuka, verseny, eredmenyekCimke, palya2, masodikPalyaHelyCimke);
            verda3 = new Verda("verda3", docHudson, verseny, eredmenyekCimke, palya3, harmadikPalyaHelyCimke);
            verseny.verdak.Add(verda1);
            verseny.verdak.Add(verda2);
            verseny.verdak.Add(verda3);
            szelepkupa.pontozasSorrend.Add(verda1);
            szelepkupa.pontozasSorrend.Add(verda2);
            szelepkupa.pontozasSorrend.Add(verda3);
            foreach (var item in verseny.verdak)
            {
                item.cimke.Visibility = Visibility.Hidden;
            }
        }
        private void startGomb_Click(object sender, RoutedEventArgs e)
        {
            mehet = true;
            startGomb.IsEnabled = false;
            ujBajnoksagGomb.IsEnabled = false;
            ujFutamGomb.IsEnabled = false;
        }

        private void Tick (object sender, EventArgs e)
        {
            if (!mehet) return;
            int rnd1 = rnd.Next(1, 3);
            int rnd2 = rnd.Next(1, 3);
            int rnd3 = rnd.Next(1, 3);

            verda1.Mozgas(celErtek, rnd1);
            verda2.Mozgas(celErtek, rnd2);
            verda3.Mozgas(celErtek, rnd3);

            if (verseny.verdak.Count == verseny.sorrend.Count)
            {
                foreach (Verda item in verseny.verdak)
                {
                    if (item.pontSzam == 0)
                    {
                        return;
                    }
                }
                szelepkupa.UjKupa();

                eredmenyekCimke.Content = $"Hely        Név         1.      2.      3.          Pont" + " \n "+
                    $"1.        {szelepkupa.pontozasSorrend[0].name}        { szelepkupa.pontozasSorrend[0].elsoHelyekSzama}       { szelepkupa.pontozasSorrend[0].masodikHelyekSzama}       { szelepkupa.pontozasSorrend[0].harmadikHelyekSzama}           { szelepkupa.pontozasSorrend[0].pontSzam}" + " \n " +   
                    $"2.        {szelepkupa.pontozasSorrend[1].name}        { szelepkupa.pontozasSorrend[1].elsoHelyekSzama}       { szelepkupa.pontozasSorrend[1].masodikHelyekSzama}       { szelepkupa.pontozasSorrend[1].harmadikHelyekSzama}           { szelepkupa.pontozasSorrend[1].pontSzam}" + " \n " +   
                    $"3.        {szelepkupa.pontozasSorrend[2].name}        { szelepkupa.pontozasSorrend[2].elsoHelyekSzama}       { szelepkupa.pontozasSorrend[2].masodikHelyekSzama}       { szelepkupa.pontozasSorrend[2].harmadikHelyekSzama}           { szelepkupa.pontozasSorrend[2].pontSzam}";

                ujFutamGomb.IsEnabled = true;
                ujBajnoksagGomb.IsEnabled = true;
            }
        }

        public class Verda
        {
            public string name;
            public int pontSzam = 0;
            public Rectangle tgl;
            public int elsoHelyekSzama = 0;
            public int masodikHelyekSzama = 0;
            public int harmadikHelyekSzama = 0;
            Verseny verseny;
            public Thickness start;
            public float speed;
            public float value = 5f;
            public Label cimke;
            public Rectangle palya;
            public Label helyezesek;

            public Verda(string name, Rectangle tgl, Verseny verseny, Label cimke, Rectangle palya, Label helyezesek)
            {
                this.verseny = verseny;
                Random rnd = new Random();
                this.tgl = tgl;
                this.name = name;
                speed = rnd.Next(1, 10);
                start = tgl.Margin;
                this.cimke = cimke;
                this.palya = palya;
                this.helyezesek = helyezesek;
            }

            public void Mozgas(double celErtek, int rnd)
            {
                if (tgl.Margin.Left >= celErtek)
                {
                    tgl.Margin = new Thickness(celErtek, tgl.Margin.Top, 0, 0);
                    if(!verseny.sorrend.Contains(this))
                    {
                        verseny.sorrend.Add(this);

                        if (verseny.sorrend[0] == this)
                        {
                            elsoHelyekSzama++;
                            cimke.Content = "1";
                            pontSzam += 3;
                            Color szin = Color.FromRgb(225, 223, 0);
                            SolidColorBrush szinezes = new SolidColorBrush(szin);
                            palya.Fill = szinezes;
                        }
                        else if(verseny.sorrend[1] == this)
                        {
                            masodikHelyekSzama++;
                            cimke.Content = "2";
                            pontSzam += 2;
                            Color szin = Color.FromRgb(211, 211, 211);
                            SolidColorBrush szinezes = new SolidColorBrush(szin);
                            palya.Fill = szinezes;
                        }
                        else if(verseny.sorrend[2] == this)
                        {
                            harmadikHelyekSzama++;
                            cimke.Content = "3";
                            pontSzam += 1;
                            Color szin = Color.FromRgb(185, 122, 68);
                            SolidColorBrush szinezes = new SolidColorBrush(szin);
                            palya.Fill = szinezes;
                        }
                        cimke.Visibility = Visibility.Visible;
                    }
                    return;
                }
                
                Thickness th = new Thickness(value, tgl.Margin.Top, 0, 0);
                tgl.Margin = th;

                value += 5 * rnd;
            }
        }

        public class Verseny
        {
            public List<Verda> verdak = new List<Verda>();
            public List<Verda> sorrend = new List<Verda>();
        }

        public class szelepKupa
        {
            public List<Verda> pontozasSorrend = new List<Verda>();

            public void UjKupa()
            {
                var vMozgas = false;
                do
                {
                    vMozgas = false;
                    for (int i = 0; i < pontozasSorrend.Count() - 1; i++)
                    {
                        if (pontozasSorrend[i].pontSzam < pontozasSorrend[i + 1].pontSzam)
                        {
                            var ertek = pontozasSorrend[i];
                            pontozasSorrend[i] = pontozasSorrend[i + 1];
                            pontozasSorrend[i + 1] = ertek;
                            vMozgas = true;
                        }
                    }
                } while (vMozgas);
            }


        }

        private void ujFutamGomb_Click(object sender, RoutedEventArgs e)
        {
            mehet = false;
            ujFutamGomb.IsEnabled = false;
            startGomb.IsEnabled = true;

            verseny.sorrend.Clear();
            foreach (Verda x in verseny.verdak)
            {
                x.tgl.Margin = x.start;
                x.value = 5f;
                Color szin = Color.FromRgb(107, 142, 35);
                SolidColorBrush szinezes = new SolidColorBrush(szin);
                x.palya.Fill = szinezes;
                x.cimke.Visibility = Visibility.Hidden;
            }
        }

        private void ujBajnoksagGomb_Click(object sender, RoutedEventArgs e)
        {
            string eredmeny = " A bajnokság eredményei" + " \n " + " \n " + eredmenyekCimke.Content.ToString();
            MessageBox.Show(eredmeny);
            foreach (Verda y in szelepkupa.pontozasSorrend)
            {
                y.elsoHelyekSzama = 0;
                y.masodikHelyekSzama = 0;
                y.harmadikHelyekSzama = 0;
                y.pontSzam = 0;
                eredmenyekCimke.Content = " ";

                mehet = false;
                ujFutamGomb.IsEnabled = false;
                startGomb.IsEnabled = true;

                verseny.sorrend.Clear();
                foreach (Verda c in verseny.verdak)
                {
                    c.tgl.Margin = c.start;
                    c.value = 5f;
                    Color szin = Color.FromRgb(107, 142, 35);
                    SolidColorBrush szinezes = new SolidColorBrush(szin);
                    c.palya.Fill = szinezes;
                    c.cimke.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
