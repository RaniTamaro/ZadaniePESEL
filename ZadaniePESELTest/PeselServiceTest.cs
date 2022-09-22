using System;
using ZadaniePESEL.Services;

namespace ZadaniePESELTest
{
    public class Tests
    {
        private IPeselService _peselService;
        private DateTime firstData = new DateTime(1987, 6, 25);

        [SetUp]
        public void Setup()
        {
            _peselService = new PeselService();
        }

        /// <summary>
        /// Test sprawdzaj¹cy poprawnoœæ daty wyliczonej z numeru PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        /// <param name="date">Data w formacice string, która zostanie zmieniona na typ DateTime. S³u¿y do porównania wyniku.</param>
        [Test]
        [TestCase("87062500111", "25/06/1987 0 AM")]
        [TestCase("05271200111", "12/07/2005 0 AM")]
        [TestCase("99923100111", "31/12/1899 0 AM")]
        public void BirthDateTest(string pesel, string date)
        {
            var result = DateTime.Parse(date);
            var birthDate = _peselService.BithDate(pesel);

            Assert.That(birthDate, Is.EqualTo(result));
        }

        /// <summary>
        /// Test sprawdzaj¹cy poprawnoœæ wyliczenia wieku osoby z numeru PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        /// <param name="result">Wiek, w jakim jest osoba o podanym numerze PESEL na dzieñ 25.09.2022</param>
        [Test]
        [TestCase("87062500111", 35)]
        [TestCase("05271200111", 17)]
        [TestCase("99923100111", 122)]
        public void AgeTest(string pesel, int result)
        {
            //Zmiana rezultatu dla lat przysz³ych
            var currentlyDate = DateTime.Now;

            if (currentlyDate > new DateTime(2022, 09, 25))
            {
                var birthDay = _peselService.BithDate(pesel);

                if (birthDay.Month >= currentlyDate.Month && birthDay.Day >= currentlyDate.Day)
                {
                    result = currentlyDate.Year - birthDay.Year;
                }
                else
                {
                    result = currentlyDate.Year - birthDay.Year - 1;
                }
            }

            Assert.That(_peselService.Age(pesel), Is.EqualTo(result));
        }

        /// <summary>
        /// Test sprawdzaj¹cy poprawnoœæ danych dotycz¹cych promocji na postawie numeru PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        [Test]
        [TestCase("87062500111")]
        [TestCase("05271200111")]
        [TestCase("99923100111")]
        public void PromotionTest(string pesel)
        {
            var currentDate = DateTime.Now.Date;
            var birthDay = _peselService.BithDate(pesel);
            var promotionMonth = new List<int> { 1, 10, 11, 12 };
            var promotionResult = _peselService.Promotion(pesel);

            //Promocja wyliczana jest na podstawie aktualnej daty
            if (currentDate.Month == birthDay.Month && currentDate.Day == birthDay.Day)
            {
                Assert.That(promotionResult, Is.EqualTo(0.1));
            }
            else if (currentDate.Month == birthDay.Month)
            {
                Assert.That(promotionResult, Is.EqualTo(0.05));
            }
            else if (promotionMonth.Contains(birthDay.Month))
            {
                Assert.That(promotionResult, Is.EqualTo(0.025));
            }
            else
            {
                Assert.That(promotionResult, Is.EqualTo(0));
            }
        }

        /// <summary>
        /// Test sprawdzaj¹cy poprawnoœæ odczytania p³ci z numeru PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        /// <param name="result">Oczekiwany rezultat: True - dla mê¿czyzny, False - dla kobiety.</param>
        [Test]
        [TestCase("87062500511", true)]
        [TestCase("05271200141", false)]
        [TestCase("99923100181", false)]
        public void IsMaleTest(string pesel, bool result)
        {
            Assert.That(_peselService.IsMale(pesel), Is.EqualTo(result));
        }

        /// <summary>
        /// Test sprawdzaj¹cy poprawnoœæ numeru PESEL.
        /// </summary>
        /// <param name="pesel">Numer PESEL w formie stringa.</param>
        /// <param name="result">Oczekiwany rezultat: True - jeœli jest poprawny, False - w przeciwnym wypadku.</param>
        [Test]
        [TestCase("87062500512", true)]
        [TestCase("05271200148", true)]
        [TestCase("99923100181", false)]
        [TestCase("999231001", false)]
        public void PeselValidationTest(string pesel, bool result)
        {
            Assert.That(_peselService.PeselValidation(pesel), Is.EqualTo(result));
        }
    }
}