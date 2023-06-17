namespace Trading.Models
{
    public class Trader
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int Balansi { get; set; }

        public IList<Savdo> Savdolar { get; set; }

        public Trader()
        {
            Savdolar = new List<Savdo>();
        }
    }
}
