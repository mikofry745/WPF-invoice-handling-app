using ObsługaFaktur.Models;

namespace ObsługaFaktur.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly Product _product;

        public ProductViewModel(Product product)
        {
            _product = product;
        }

        public string Name => _product.Name;
        public double VatPercent => _product.VatPercent;
        public string Unit => _product.Unit.ToString();
        public decimal NetPrice => _product.NetPrice;
        public decimal GrossPrice => _product.GrossPrice;
        public string Comment => _product.Comment;
    }
}
