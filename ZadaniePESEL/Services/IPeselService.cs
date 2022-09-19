namespace ZadaniePESEL.Services
{
    public interface IPeselService
    {
        public DateTime BithDate(string pesel);

        public int Age(string pesel);

        public double Promotion(string pesel);
    }
}
