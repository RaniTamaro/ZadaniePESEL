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

        [Test]
        [TestCase("87062500111", 35)]
        [TestCase("05271200111", 17)]
        [TestCase("99923100111", 122)]
        public void Age(string pesel, int result)
        {
            Assert.AreEqual(_peselService.Age(pesel), result);
        }
    }
}