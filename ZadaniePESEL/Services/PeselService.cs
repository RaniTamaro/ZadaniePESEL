namespace ZadaniePESEL.Services
{
    /// <summary>
    /// Klasa, w której umieszczone są wszystkie funkcje służące do otrzymania daty urodzenia oraz płci z numeru PESEL.
    /// </summary>
    public class PeselService : IPeselService
    {
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

        public double Promotion(string pesel)
        {
            var birthDate = BithDate(pesel);
            var birthMonth = birthDate.Month;
            var promotionMonth = new List<int> { 1, 10, 11, 12 };
            var today = DateTime.Today;

            if (today.Equals(birthDate))
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
    }
}
