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

            //Ustalenie, czy osoba miała już urodziny
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

            //Ustalenie roku urodzenia
            if (birthMonth > 20)
            {
                birthMonth -= 20;
                birthYear += 2000;
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
            else if (birthMonth > 80)
            {
                birthMonth -= 80;
                birthYear += 1800;
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
    }
}
