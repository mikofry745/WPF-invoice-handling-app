namespace ObsługaFaktur.Models
{
    public class Product
    {
        public Product()
        {
        }

        public Product(string name, double vatPercent,
            Unit unit, decimal netPrice, string comment)
        {
            Name = name;
            VatPercent = vatPercent;
            Unit = unit;
            NetPrice = netPrice;
            Comment = comment;
        }

        public string Name { get; set; }
        public double VatPercent { get; set; }
        public Unit Unit { get; set; }
        public decimal NetPrice { get; set; }
        public string Comment { get; set; }

        public decimal GrossPrice
        {
            get => NetPrice * VatFactor;
            set => NetPrice = value / VatFactor;
        }

        private decimal VatFactor => (1.0m + (decimal)VatPercent / 100m);

        public bool Conflicts(Product product)
        {
            if (product.Name != Name)
            {
                return false;
            }

            return true;
        }
    }
}
