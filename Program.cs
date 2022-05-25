using System;
using System.Collections.Generic;

namespace Prog_obiektowe_WSB_Projekt
{
    interface IInfo
    {
        void WypiszInfo();
    }

    class Wydzial
    {
        private List<Jednostka> jednostki = new List<Jednostka>();
        public List<Przedmiot> przedmioty = new List<Przedmiot>();
        private List<Student> studenci = new List<Student>();

        public void DodajJednostke(string nazwa, string adres)
        {
            jednostki.Add(new Jednostka(nazwa, adres));
            Console.WriteLine("Dodano nową jednostkę o nazwie \"" + nazwa + "\";");

        }

        public void DodajPrzedmiot(Przedmiot p)
        {
            przedmioty.Add(p);
            Console.WriteLine("Dodano nowy przedmiot o nazwie \"" + p.Nazwa  + "\";");
        }

        public void DodajStudenta(Student s)
        {
            try
            {
                s.wydzialy.Add(this);
                studenci.Add(s);
                Console.WriteLine("Dodano nowego studenta, który nazywa się \"" + s.ImieNazwisko + "\";");
            }
            catch {
                Console.WriteLine("Nie udało się dodać studenta " + s.ImieNazwisko + " do tego wydziału..");
            }
        }

        public bool DodajWykladowce(Wykladowca w, string nazwaJednostki)
        {
            bool czyIstniejeTakaJednostka = false;
            foreach (Jednostka jednostka in jednostki)
            {
                if (jednostka.Nazwa == nazwaJednostki)
                {
                    czyIstniejeTakaJednostka = true;
                    jednostka.DodajWykladowce(w);
                }
            }
            return czyIstniejeTakaJednostka;
        }

        public void InfoStudenci(bool infoOceny)
        {
            int lp = 1;
            foreach(Student s in studenci)
            {
                Console.WriteLine(lp + ") ");
                s.WypiszInfo();
                if(infoOceny)s.InfoOceny();
                lp++;
            }
        }

        public void InfoJednostki(bool infoWykladowcy)
        {
            int lp = 1;
            foreach (Jednostka j in jednostki)
            {
                Console.WriteLine("JEDNOSTKA NR. " + lp + ":");
                j.WypiszInfo();
                if (infoWykladowcy) j.InfoWykladowcy();
                lp++;
            }

        }

        public void InfoPrzedmioty()
        {
            int lp = 1;
            Console.WriteLine("Lista przedmiotów wykładanych na tym wydziale: ");
            foreach(Przedmiot przedmiot in przedmioty)
            {
                Console.WriteLine(lp + ") " + przedmiot.Nazwa);
                lp++;
            }

        }

        public bool DodajOcene(int nrIndeksu, string nazwaPrzedmiotu, double ocena, string data)
        {
            bool result = false;
            foreach(Przedmiot przedmiot in przedmioty)
            {
                if(przedmiot.Nazwa == nazwaPrzedmiotu)
                {
                    foreach(Student s in studenci)
                    {
                        if(s.NrIndeksu == nrIndeksu)
                        {
                            result = s.DodajOcene(nazwaPrzedmiotu, ocena, data);
                        }
                    }
                    if (!result) Console.WriteLine("Mimo prawidłowej nazwy przedmiotu, nie udało się odnaleźć studenta o podanym numerz indeksu. Ocena nie zostanie dodana.");
                }
            }
            if (!result) Console.WriteLine("Nie udało się odnaleźć przedmiotu o takiej nazwie. Ocena nie zostanie dodana..");
            return result;
        }

        public bool UsunStudenta(int nrIndeksu)
        {
            bool result = false;
            foreach(Student student in studenci)
            {
                if(student.NrIndeksu == nrIndeksu)
                {
                    studenci.Remove(student);
                    student.wydzialy.Remove(this);
                    Console.WriteLine("Student o numerze indeksu " + nrIndeksu + " został prawidłowo usunięty z listy.");
                    result = true;
                }
            }
            if (!result) Console.WriteLine("Nie udało się odnaleźć na liście studenta o podanym numerze Indeksu..");
            return result;
        }

        public bool PzeniesWykladowce(Wykladowca w, string obecnaJednostka, string nowaJednostka)
        {
            bool result = false;
            foreach(Jednostka j in jednostki)
            {
                if(j.Nazwa == obecnaJednostka && j.Wykladowcy.Contains(w))
                {
                    j.UsunWykladowce(w);
                    foreach(Jednostka j2 in jednostki)
                    {
                        if(j2.Nazwa == nowaJednostka)
                        {
                            j2.DodajWykladowce(w);
                            result = true;
                        }
                    }
                    if (!result) Console.WriteLine("Coś poszło nie tak, nie udało się dodać Wykładowcy do nowej jednostki..");
                }
            }
            if (!result) Console.WriteLine("Podany Wykładowca nie pracuje w jednostce podanej jako \"obecna\". Brak możliwości przeniesienia..");
            return result;
        }
    }

    class Osoba : IInfo
    {
        protected string imie = "";
        protected string nazwisko = "";
        protected string dataUrodzenia = "";

        public Osoba(string imie, string nazwisko, string dataUrodzenia)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.dataUrodzenia = dataUrodzenia;
        }

        public virtual void WypiszInfo()
        {
            Console.WriteLine("Imię i Nazwisko:" + imie + " " + nazwisko + "; Data Urodzenia: " + dataUrodzenia + "; "); 
        }
    }

    class Student : Osoba
    {
        private string kierunek = "";
        private string specjalnosc = "";
        private int rok = 0;
        private int grupa = 0;
        private int nrIndeksu = 0;
        public List<Wydzial> wydzialy = new List<Wydzial>(2);
        public List<OcenaKoncowa> oceny = new List<OcenaKoncowa>();

        public Student(string imie, string nazwisko, string dataUrodzenia, string kierunek, string specjalnosc, int rok, int grupa, int nrIndeksu) : base(imie, nazwisko, dataUrodzenia)
        {
            this.kierunek = kierunek;
            this.specjalnosc = specjalnosc;
            this.rok = rok;
            this.grupa = grupa;
            this.nrIndeksu = nrIndeksu;
        }

        public string ImieNazwisko
        {
            get
            {
                return imie + " " + nazwisko;
            }
        }

        public int NrIndeksu { get { return nrIndeksu; }}

        public override void WypiszInfo()
        {
            base.WypiszInfo();
            Console.Write("Kierunek Studiów: " + kierunek + "; Specjalność: " + specjalnosc + "; Rok Studiów: " + rok + "; Grupa: " + grupa + "; Nr. Indeksu: " + nrIndeksu + "; ");
        }

        public void InfoOceny()
        {
            Console.WriteLine("OCENY KOŃCOWE Z DANYCH PRZEDMIOTÓW: ");
            foreach(OcenaKoncowa ocena in oceny)
            {
                ocena.WypiszInfo();
            }
        }

        public bool DodajOcene(string nazwaPrzedmiotu, double ocena, string data)
        {
            Przedmiot tempPrzedmiot = null;

            foreach(Wydzial wydzial in wydzialy)
            {
                foreach(Przedmiot przedmiot in wydzial.przedmioty)
                {
                    if(przedmiot.Nazwa == nazwaPrzedmiotu)
                    {
                        tempPrzedmiot = przedmiot;
                        break;
                    }
                } 
            }
            if (tempPrzedmiot != null)
            {
                oceny.Add(new OcenaKoncowa(ocena, data, tempPrzedmiot));
                Console.WriteLine("Ocena końcowa (" + ocena + ") z przedmiotu " + nazwaPrzedmiotu + " została pomyślnie dodana.");
                return true;
            } else
            {
                return false;
            }
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
        public string ImieNazwisko
        {
            get
            {
                return imie + " " + nazwisko;
            }
        }

        public string Imie
        {
            get
            {
                return imie;
            }
        }

        public string Nazwisko
        {
            get
            {
                return nazwisko;
            }
        }

        public override void WypiszInfo()
        {
            base.WypiszInfo();
            Console.Write("Tytuł naukowy: " + tytulNaukowy + "; Stanowisko: " + stanowisko + "; ");
        }
    }

    class Jednostka : IInfo
    {
        private string nazwa = "";
        private string adres = "";
        private List<Wykladowca> wykladowcy = new List<Wykladowca>();

        public string Nazwa { get { return nazwa; } }
        public List<Wykladowca> Wykladowcy { get { return wykladowcy; } }

            public Jednostka(string nazwa, string adres)
        {
            this.nazwa = nazwa;
            this.adres = adres;
        }

        public void DodajWykladowce(Wykladowca w)
        {
            wykladowcy.Add(w);
            Console.WriteLine("Dodawanie wykładowcy " + w.ImieNazwisko + " zakończone powodzeniem.");
        }

        public bool UsunWykladowce(Wykladowca w)
        {
            try
            {
                wykladowcy.Remove(w);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool UsunWykladowce(string imie, string nazwisko)
        {
            Wykladowca tempWykladowca = null;
            bool result = false;
            foreach(Wykladowca w in wykladowcy)
            {
                if(w.Imie == imie && w.Nazwisko == nazwisko ) tempWykladowca = w;
            }
            if(tempWykladowca != null)
            {
                try
                {
                    wykladowcy.Remove(tempWykladowca);
                    result = true;
                }
                catch { result = false; }
            }
            return result;
        }

        public void InfoWykladowcy()
        {
            foreach(Wykladowca w in wykladowcy)
            {
                w.WypiszInfo();
            }

        }

        public void WypiszInfo()
        {
            Console.WriteLine("INFORMACJE O JEDNOSTCE " + nazwa + ":");
            Console.WriteLine("NAZWA JEDNOSTKI: " + nazwa + ";");
            Console.WriteLine("ADRES: " + adres + ";");
            Console.WriteLine("Lista Wykładowców:");
            InfoWykladowcy();
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

        public string Nazwa 
        { get
            { return nazwa; } 
        }

    }

    class OcenaKoncowa : IInfo
    {
        private double wartosc = 0.0;
        private string data = "";
        private Przedmiot p;

        public OcenaKoncowa(double wartosc, string data, Przedmiot p)
        {
            this.wartosc = wartosc;
            this.data = data;
            this.p = p;
        }

        public void WypiszInfo()
        {
            Console.WriteLine("PRZEDMIOT: " + p.Nazwa + "; Ocena: " + wartosc + "; Data wystawienia: " + data + ";");

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
