namespace ZadaniePESEL.Services
{
    public interface IPeselService
    {
        public DateTime BithDate(string pesel);

        public int Age(string pesel);

        public double Promotion(string pesel);

        public bool IsMale(string pesel);

        public string Wishes(string pesel, string name, string surname);

        public bool PeselValidation(string pesel);
    }
}
