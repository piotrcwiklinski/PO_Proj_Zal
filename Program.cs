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
            for (int i = 0; i < jednostki.Count; )
            {
                if (jednostki[i].Nazwa == nazwaJednostki)
                {
                    czyIstniejeTakaJednostka = true;
                    jednostki[i].DodajWykladowce(w);
                    break;
                } else
                {
                    i++;
                }
            }
            return czyIstniejeTakaJednostka;
        }

        public void InfoStudenci(bool infoOceny)
        {
            foreach(Student s in studenci)
            {
                s.WypiszInfo();
                if(infoOceny)s.InfoOceny();
            }
        }

        public void InfoJednostki(bool infoWykladowcy)
        {
            int lp = 1;
            for (int i = 0; i < jednostki.Count; i++)
            {
                Console.WriteLine("\n\nJEDNOSTKA NR. " + lp + ":");
                jednostki[i].WypiszInfo();
                if (infoWykladowcy) jednostki[i].InfoWykladowcy();
                lp++;
            }

        }

        public void InfoPrzedmioty()
        {
            int lp = 1;
            Console.WriteLine("\n\nLista przedmiotów wykładanych na tym wydziale: ");
            foreach(Przedmiot przedmiot in przedmioty)
            {
                Console.WriteLine(lp + ") " + przedmiot.Nazwa);
                lp++;
            }

        }

        public bool DodajOcene(int nrIndeksu, string nazwaPrzedmiotu, double ocena, string data)
        {
            bool result = false;
            bool przedmiotZnaleziony = false;
            foreach(Przedmiot przedmiot in przedmioty)
            {
                if(przedmiot.Nazwa == nazwaPrzedmiotu)
                {
                    przedmiotZnaleziony = true;
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
            if (!przedmiotZnaleziony) Console.WriteLine("Nie udało się znaleźć przedmiotu o wskazanej nazwie...");
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
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool PrzeniesWykladowce(Wykladowca w, string obecnaJednostka, string nowaJednostka)
        {
            bool result = false;
            foreach(Jednostka j in jednostki)
            {
                if(j.Nazwa == obecnaJednostka && j.wykladowcy.Contains(w))
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
                }
            }
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
            Console.WriteLine("");
            Console.Write(imie + " " + nazwisko + "; Data Urodzenia: " + dataUrodzenia + "; "); 
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
            Console.WriteLine("\nOCENY KOŃCOWE Z DANYCH PRZEDMIOTÓW: ");
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
        public List<Wykladowca> wykladowcy = new List<Wykladowca>();

        public string Nazwa { get { return nazwa; } }

            public Jednostka(string nazwa, string adres)
        {
            this.nazwa = nazwa;
            this.adres = adres;
        }

        public void DodajWykladowce(Wykladowca w)
        {
            wykladowcy.Add(w);
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
            Console.Write("LISTA WYKŁADOWCÓW:");
            foreach (Wykladowca w in wykladowcy)
            {
                w.WypiszInfo();
            }

        }

        public void WypiszInfo()
        {
            Console.WriteLine("NAZWA JEDNOSTKI: " + nazwa + ";");
            Console.WriteLine("ADRES: " + adres + ";");

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
            Wydzial informatyka = new Wydzial();

            informatyka.DodajJednostke("WSB Gdańsk", "al. Grunwaldzka XX 80-XXX Gdańsk");
            informatyka.DodajJednostke("WSB Gdynia", "jakiś adres w Gdyni...");

            Student s1 = new Student("Adam", "Andrus", "01.01.2001", "Informatyka", "Programowanie", 2, 1, 12345 );
            Student s2 = new Student("Bartosz", "Bobas", "02.02.2002", "Cyberbezpieczeństwo", "Hakowanie", 1, 2, 54321);
            Student s3 = new Student("Czesław", "Czeczen", "03.03.2003", "Robotyka", "Nanoboty", 3, 3, 67890);

            Wykladowca w1 = new Wykladowca("Dawid", "Dębowiak", "04.04.1984", "Magister Inżynier", "Adiunkt");
            Wykladowca w2 = new Wykladowca("Ernest", "Eustachewicz", "05.05.1975", "Doktor", "Dziekan");
            Wykladowca w3 = new Wykladowca("Filip", "Frankowski", "06.06.1978", "Inżynier", "Wykładowca");

            Przedmiot matematyka = new Przedmiot("Matematyka", "Informatyka", "Programowanie", 2, 36);
            Przedmiot progObiektowe = new Przedmiot("Programowanie Obiektowe", "Informatyka", "Programowanie", 3, 36);

            informatyka.DodajPrzedmiot(matematyka);
            informatyka.DodajPrzedmiot(progObiektowe);

            informatyka.DodajStudenta(s1);
            informatyka.DodajStudenta(s2);
            informatyka.DodajStudenta(s3);

            if(informatyka.DodajWykladowce(w1, "WSB Gdańsk"))
            {
               Console.WriteLine("Dodawanie wykładowcy " + w1.ImieNazwisko + " zakończone powodzeniem.");
            } else
            {
                Console.WriteLine("Nie udało się dodać wykładowcy do wskazanej jednostki.");
            }

            if(informatyka.DodajWykladowce(w2, "WSB Gdynia"))
            {
              Console.WriteLine("Dodawanie wykładowcy " + w2.ImieNazwisko + " zakończone powodzeniem.");
            }
            else
            {
                Console.WriteLine("Nie udało się dodać wykładowcy do wskazanej jednostki.");
            }

            if (informatyka.DodajWykladowce(w3, "WSB Gdańsk"))
            {
                Console.WriteLine("Dodawanie wykładowcy " + w3.ImieNazwisko + " zakończone powodzeniem.");
            }
            else
            {
                Console.WriteLine("Nie udało się dodać wykładowcy do wskazanej jednostki.");
            }

            informatyka.InfoStudenci(false);
            informatyka.InfoJednostki(true);

            informatyka.InfoPrzedmioty();

            if(informatyka.DodajOcene(12345 , "Matematyka", 4.5 , "26.05.2022"))
            { }
             else
            {
                Console.WriteLine("Dodawanie oceny nie powiodło się...");
            }

            if (informatyka.DodajOcene(12345, "Religia", 3.5, "21.05.2022"))
            { }
            else
            {
                Console.WriteLine("Dodawanie oceny nie powiodło się...");
            }

            if (informatyka.DodajOcene(09876, "Matematyka", 4.5, "26.05.2022"))
            { }
            else
            {
                Console.WriteLine("Dodawanie oceny nie powiodło się...");
            }
            
            if (informatyka.UsunStudenta(09876)) 
            {
                Console.WriteLine("Udało się usunąć wskazanego studenta.");
            } else
            {
                Console.WriteLine("Nie udało się usunąć wskazanego studenta...");
            }
            
            
            if (informatyka.UsunStudenta(67890))
            {
                Console.WriteLine("Udało się usunąć wskazanego studenta.");
            }
            else
            {
                Console.WriteLine("Nie udało się usunąć wskazanego studenta...");
            }

            informatyka.InfoStudenci(false);
            

            Console.WriteLine("");

            if(informatyka.PrzeniesWykladowce(w1, "WSB Gdynia" , "WSB Gdańsk"))
            {
                Console.WriteLine("Przenoszenie wykładowcy pomiędzy jednostkami zakończone powodzeniem.");
            } else
            {
                Console.WriteLine("Nie udało się przenieść wykładowcy pomiędzy jednostkami...");
            }

            if (informatyka.PrzeniesWykladowce(w2, "WSB Gdynia", "WSB Gdańsk"))
            {
                Console.WriteLine("Przenoszenie wykładowcy pomiędzy jednostkami zakończone powodzeniem.");
            }
            else
            {
                Console.WriteLine("Nie udało się przenieść wykładowcy pomiędzy jednostkami...");
            }

            if (informatyka.PrzeniesWykladowce(w2, "WSB Gdańsk", "WSB Gdynia"))
            {
                Console.WriteLine("Przenoszenie wykładowcy pomiędzy jednostkami zakończone powodzeniem.");
            }
            else
            {
                Console.WriteLine("Nie udało się przenieść wykładowcy pomiędzy jednostkami...");
            }

            informatyka.InfoStudenci(true);




        }
    }
}
