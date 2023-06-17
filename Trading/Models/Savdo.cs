namespace Trading.Models
{
    public class Savdo
    {
        public int Id { get; set; }

        public SaleObject SaleObject { get; set; }

        public bool SotibOlindi { get; set; }

        public int XaridNarxi { get; set; }

        public int XaridSoni { get; set; }

        public int UmumiyHarajat { get; set; }

        public int UmumiyTushum { get; set; }

        public int Foyda { get; set; }

        public bool isOpen { get; set; }

        public Trader Trader { get; set; }
    }
}
