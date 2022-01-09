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
        int v = 100;
        double celErtek;

        public MainWindow()
        {
            InitializeComponent();
            celErtek = celVonal.Margin.Left;
            szelepkupa = new szelepKupa();

            idozito = new DispatcherTimer();
            idozito.Interval = TimeSpan.FromMilliseconds(100);
            idozito.Start();
            idozito.Tick += Tick;
            ujFutamGomb.IsEnabled = false;
            ujBajnoksagGomb.IsEnabled = false;

            verseny = new Verseny();
            verda1 = new Verda();
            verda2 = new Verda();
            verda3 = new Verda();

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
        }

        public class Verda
        {

        }

        public class Verseny
        {

        }

        public class szelepKupa
        {

        }

        public void Mozgas(object sender, EventArgs e)
        {

        }

    }
}
