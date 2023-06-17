namespace Trading.Models
{
    public class TovarSavdo
    {
        public IList<SaleObject> Tovarlar { get; set; }

        public IList<Savdo> Savdolar { get; set; }
    }
}
