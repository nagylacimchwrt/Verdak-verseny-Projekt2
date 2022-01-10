﻿using System;
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
            celErtek = 890;
            szelepkupa = new szelepKupa();

            idozito = new DispatcherTimer();
            idozito.Interval = TimeSpan.FromMilliseconds(50);
            idozito.Start();
            idozito.Tick += Tick;
            ujFutamGomb.IsEnabled = false;
            ujBajnoksagGomb.IsEnabled = false;

            verseny = new Verseny();
            verda1 = new Verda("verda1", mcQueen, verseny, eredmenyekCimke);
            verda2 = new Verda("verda2", Matuka, verseny, eredmenyekCimke);
            verda3 = new Verda("verda3", docHudson, verseny, eredmenyekCimke);
            verseny.verdak.Add(verda1);
            verseny.verdak.Add(verda2);
            verseny.verdak.Add(verda3);
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
                idozito.Stop();
                foreach (Verda item in verseny.verdak)
                {
                    if (item.pontSzam == 0)
                    {
                        return;
                    }
                }
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

            public Verda(string name, Rectangle tgl, Verseny verseny, Label cimke)
            {
                this.verseny = verseny;
                Random rnd = new Random();
                this.tgl = tgl;
                this.name = name;
                speed = rnd.Next(1, 10);
                start = tgl.Margin;
                this.cimke = cimke;
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
                            pontSzam += 5;
                        }
                        else if(verseny.sorrend[1] == this)
                        {
                            masodikHelyekSzama++;
                            cimke.Content = "2";
                            pontSzam += 3;
                        }
                        else if(verseny.sorrend[2] == this)
                        {
                            harmadikHelyekSzama++;
                            cimke.Content = "3";
                            pontSzam += 1;
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

        }
    }
}
