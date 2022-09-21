using Microsoft.AspNetCore.Mvc;

namespace ZadaniePESEL.Services
{
    /// <summary>
    /// Klasa, w której umieszczone są wszystkie funkcje służące do otrzymania daty urodzenia oraz płci z numeru PESEL.
    /// </summary>
    public class PeselService : IPeselService
    {
        /// <summary>
        /// Służy do obliczenia wieku osoby o podanym numerze PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        /// <returns>Ilość lat osoby o podanym numerze PESEL.</returns>
        public int Age(string pesel)
        {
            var birthDate = BithDate(pesel);
            var today = DateTime.Today;
            var years = today.Year - birthDate.Year;

            //Ustalenie, czy osoba miała już urodziny, jeśli nie zmniejszamy wiek o jeden.
            if (birthDate.Month >= today.Month && birthDate.Day > today.Day)
            {
                years--;
            }

            return years;
        }

        /// <summary>
        /// Wydobywa datę urodzenia z numeru PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        /// <returns>Datę urodzenia osoby o podanym numerze PESEL.</returns>
        public DateTime BithDate(string pesel)
        {
            //Wydobycie daty z peselu
            var birthYear = int.Parse(pesel.Substring(0, 2));
            var birthMonth = int.Parse(pesel.Substring(2, 2));
            var birthDay = int.Parse(pesel.Substring(4, 2));

            //Ustalenie roku urodzenia na podstawie numeru misiąca.
            if (birthMonth > 80)
            {
                birthMonth -= 80;
                birthYear += 1800;
            }
            else if (birthMonth > 40)
            {
                birthMonth -= 40;
                birthYear += 2100;
            }
            else if (birthMonth > 60)
            {
                birthMonth -= 60;
                birthYear += 2200;
            }
            else if (birthMonth > 20)
            {
                birthMonth -= 20;
                birthYear += 2000;
            }
            else
            {
                birthYear += 1900;
            }

            return new DateTime(birthYear, birthMonth, birthDay);
        }

        /// <summary>
        /// Służy do obliczenia zniżki przysługującej osobie o danym numerze PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        /// <returns>Zniżkę w formacie double dla osoby o podanym numerze PESEL.</returns>
        public double Promotion(string pesel)
        {
            var birthDate = BithDate(pesel);
            var birthMonth = birthDate.Month;
            var promotionMonth = new List<int> { 1, 10, 11, 12 };
            var today = DateTime.Today;

            //Zwrócenie liczby odpowiadajacej za znikę.
            if (today.Day.Equals(birthDate.Day) && today.Month.Equals(birthMonth))
            {
                return 0.1;
            }
            else if (today.Month.Equals(birthMonth))
            {
                return 0.05;
            }
            else if (promotionMonth.Contains(birthMonth))
            {
                return 0.025;
            }

            return 0;
        }

        /// <summary>
        /// Służy do ustalenia płci osoby o danym numerze PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        /// <returns>True - jeśli osoba jest mężczyzną, false - jeśli osoba jest kobietą.</returns>
        public bool IsMale(string pesel)
        {
            var sexNumber = int.Parse(pesel[9].ToString());

            if (sexNumber % 2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Służy do wygenerowania życzeń dla klienta w zależności od jego płci.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        /// <param name="name">Imię osoby.</param>
        /// <param name="surname">Nazwisko osoby.</param>
        /// <returns>Życzenia z okazji urodzin klienta.</returns>
        public string Wishes(string pesel, string name, string surname)
        {
            var isMale = IsMale(pesel);
            var firstWord = "";

            if (isMale)
            {
                firstWord = "Kliencie";
            }
            else
            {
                firstWord = "Klientko";
            }

            return $"{firstWord} {name} {surname}! Życzymy Ci sto lat!";
        }

        /// <summary>
        /// Służy do sprawdzenia poprawności numeru PESEL. Sprawdza jego długość oraz liczbę kontorlną.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        /// <returns>True - jeśli numer PESEL jest poprawny, false - w przeciwnym wypadku.</returns>
        public bool PeselValidation(string pesel)
        {
            var multiplication = new List<int> { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            var sum = 0;

            //Sprawdzenie długości peselu
            if (pesel.Length != 11)
            {
                return false;
            }

            //Sprawdzenie poprawności liczby kontrolnej
            for (int i = 0; i < pesel.Length - 1; i++)
            {
                sum += int.Parse(pesel[i].ToString()) * multiplication.ElementAt(i);
            }

            var controlNumber = (10 - (sum % 10)) % 10;
            if (controlNumber != int.Parse(pesel[10].ToString()))
            {
                return false;
            }

            return true;
        }
    }
}
