namespace ObsługaFaktur.Models
{
    public class InvoiceItem
    {
        public InvoiceItem(Product product, double quantity)
        {
            Name = product.Name;
            VatPercent = product.VatPercent;
            Unit = product.Unit;
            NetPrice = product.NetPrice;
            Comment = product.Comment;
            Quantity = quantity;
        }

        public InvoiceItem(string name, double vatPercent, Unit unit, 
            double quantity, decimal unitNetPrice, string comment)
        {
            Name = name;
            VatPercent = vatPercent;
            Unit = unit;
            NetPrice = unitNetPrice;
            Comment = comment;
            Quantity = quantity;
        }

        public string Name { get; set; }
        public double VatPercent { get; set; }
        public Unit Unit { get; set; }
        public decimal NetPrice { get; set; }
        public string Comment { get; set; }
        public double Quantity { get; set; }

        public decimal GrossPrice
        {
            get => NetPrice * VatFactor * (decimal)Quantity;
            set => NetPrice = (value / VatFactor) * (decimal)Quantity;
        }

        private decimal VatFactor => (1.0m + (decimal)VatPercent / 100m);
    }
}
