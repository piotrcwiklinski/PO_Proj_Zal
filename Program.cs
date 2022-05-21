using System;

namespace Prog_obiektowe_WSB_Projekt
{
    interface IInfo
    {
        void WypiszInfo();
    }

    class Wydzial
    {
        public void DodajJednostke(string nazwa, string adres)
        {

        }

        public void DodajPrzedmiot(Przedmiot p)
        {

        }

        public void DodajStudenta(Student s)
        {

        }

        public bool DodajWykladowce(Wykladowca w, string nazwaJednostki)
        {
            return false;
        }

        public void InfoStudenci(bool infoOceny)
        {

        }

        public void InfoJednostki(bool infoWykladowcy)
        {

        }

        public void InfoPrzedmioty()
        {

        }

        public bool DodajOcene(int nrIndeksu, string nazwaPrzedmiotu, int ocena, string data)
        {
            return true;
        }

        public bool UsunStudenta(int nrIndeksu)
        {
            return false;
        }

        public bool PzeniesWykladowce(Wykladowca w, string obecnaJednostka, string nowaJednostka)
        {
            return false;
        }
    }

    class Osoba : IInfo
    {
        private string imie = "";
        private string nazwisko = "";
        private string dataUrodzenia = "";

        public Osoba(string imie, string nazwisko, string dataUrodzenia)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.dataUrodzenia = dataUrodzenia;
        }

        public virtual void WypiszInfo()
        {

        }
    }

    class Student : Osoba
    {
        private string kierunek = "";
        private string specjalnosc = "";
        private int rok = 0;
        private int grupa = 0;
        private int nrIndeksu = 0;

        public Student(string imie, string nazwisko, string dataUrodzenia, string kierunek, string specjalnosc, int rok, int grupa, int nrIndeksu) : base(imie, nazwisko, dataUrodzenia)
        {
            this.kierunek = kierunek;
            this.specjalnosc = specjalnosc;
            this.rok = rok;
            this.grupa = grupa;
            this.nrIndeksu = nrIndeksu;
        }

        public override void WypiszInfo()
        {

        }

        public void InfoOceny()
        {

        }

        public bool DodajOcene(string nazwaPrzedmiotu, double ocena, string data)
        {
            return false;
        }
    }

    class Wykladowca : Osoba
    {
        private string tytulNaukowy = "";
        private string stanowisko = "";

        public Wykladowca(string imie, string nazwisko, string dataUrodzenia, string tytulNaukowy, string stanowisko) : base(imie, nazwisko, dataUrodzenia)
        {
            this.tytulNaukowy = tytulNaukowy;
            this.stanowisko = stanowisko;
        }

        public override void WypiszInfo()
        {
            base.WypiszInfo();
        }
    }

    class Jednostka : IInfo
    {
        private string nazwa = "";
        private string adres = "";

        public Jednostka(string nazwa, string adres)
        {
            this.nazwa = nazwa;
            this.adres = adres;
        }

        public void DodajWykladowce(Wykladowca w)
        {

        }

        public bool UsunWykladowce(Wykladowca w)
        {
            return false;
        }

        public bool UsunWykladowce(string imie, string nazwisko)
        {
            return false;
        }

        public void InfoWykladowcy()
        {

        }

        public void WypiszInfo()
        {

        }
    }

    class Przedmiot
    {
        private string nazwa = "";
        private string kierunek = "";
        private string specjalnosc = "";
        private int semestr = 0;
        private int ileGodzin = 0;

        public Przedmiot(string nazwa, string kierunek, string specjalnosc, int semestr, int ileGodzin)
        {
            this.nazwa = nazwa;
            this.kierunek = kierunek;
            this.specjalnosc = specjalnosc;
            this.semestr = semestr;
            this.ileGodzin = ileGodzin;
        }
    }

    class OcenaKoncowa : IInfo
    {
        private double wartosc = 0.0;
        private string data = "";

        public OcenaKoncowa(double wartosc, string data, Przedmiot p)
        {
            this.wartosc = wartosc;
            this.data = data;
        }

        public void WypiszInfo()
        {

        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
