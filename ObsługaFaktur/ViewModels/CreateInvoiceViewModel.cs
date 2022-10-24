using ObsługaFaktur.Commands;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ObsługaFaktur.ViewModels
{
    public class CreateInvoiceViewModel : ViewModelBase
    {
        private readonly Business _business;
        private readonly InvoiceDataStore _invoiceDataStore;
        private readonly ObservableCollection<ProductViewModel> _products;
        private readonly ObservableCollection<CustomerViewModel> _customers;


        public CreateInvoiceViewModel(INavigationService cancelNavigationService,
            Business business, InvoiceDataStore invoiceDataStore)
        {
            _business = business;
            _invoiceDataStore = invoiceDataStore;
            _products = new ObservableCollection<ProductViewModel>();
            _customers = new ObservableCollection<CustomerViewModel>();

            _invoiceDataStore.ProductsLoaded += OnProductsLoaded;
            _invoiceDataStore.CustomersLoaded += OnCustomersLoaded;

            SubmitCommand = new CreateInvoiceCommand(this, business, cancelNavigationService);
            CancelCommand = new NavigateCommand(cancelNavigationService);
            LoadProductsCommand = new LoadProductsToInvoiceCommand(business, invoiceDataStore);
            LoadCustomersCommand = new LoadCustomersToInvoiceCommand(business, invoiceDataStore);

            InvoiceTypes = Enum.GetValues(typeof(InvoiceType));
            ProductUnits = Enum.GetValues(typeof(Unit));
            PaymentMethods = Enum.GetValues(typeof(PaymentMethod));

            LoadProductsCommand.Execute(null);
            LoadCustomersCommand.Execute(null);
        }

        public Visibility NewInvoiceVisibilty { get; set; } = Visibility.Visible;
        public Visibility CustomerListVisibilty { get; set; } = Visibility.Hidden;
        public Visibility ProductListVisibilty { get; set; } = Visibility.Hidden;

        public Array InvoiceTypes { get; }
        public Array ProductUnits { get; }
        public Array PaymentMethods { get; }

        public IEnumerable<ProductViewModel> ProductList
        {
            get
            {
                if (SearchedProductName != "" && SearchedProductName != null)
                {
                    return _products.Where(p => p.Name.Contains(SearchedProductName));
                }

                return _products;
            }
  
        } 

        public IEnumerable<CustomerViewModel> CustomerList 
        {
            get
            {
                if (SearchedCustomerName != "" && SearchedCustomerName != null)
                {
                    return _customers.Where(c => c.Name.Contains(SearchedCustomerName));
                }

                return _customers;
            }
        } 
       

        public CustomerViewModel SelectedCustomer { get; set; }
        public ProductViewModel SelectedProduct { get; set; }

        public string SearchedCustomerName { get; set; }
        public string SearchedProductName { get; set; }

        /// <summary>
        /// Issuer = true / Recipient = false
        /// </summary>
        public bool IsIssuer { get; set; }

        public bool NewProductNullArgumentsErrorVisibility { get; set; } = false;


        #region InvoiceData
        //Invoice
        public InvoiceType Type { get; set; }
        public string Number { get; set; }
        public DateTime DateOfIssue { get; set; } = DateTime.Today;
        public DateTime PaymentTerm { get; set; } = DateTime.Today.AddDays(14);
        public string PlaceOfIssue { get; set; }
        public string NameOfTheIssuer { get; set; }
        public string Remarks { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public PaymentMethod PaymentMethod { get; set; }

        private string bankName;
        public string BankName
        {
            get
            {
                if (IssuerBankName != "")
                {
                    bankName = IssuerBankName;
                }

                return bankName;
            }
            set => bankName = value;
        }

        private string iban;
        public string Iban
        {
            get
            {
                if (IssuerIban != "")
                {
                    iban = IssuerIban;
                }

                return iban;
            }
            set => iban = value;
        }

        //Issuer
        public string IssuerName { get; set; }
        public string IssuerStreet { get; set; }
        public string IssuerTown { get; set; }
        public string IssuerZipCode { get; set; }
        public string IssuerCountry { get; set; } = "";
        public string IssuerPhoneNumber { get; set; } = "";
        public string IssuerEmailAddress { get; set; }
        public string IssuerBankName { get; set; } = "";
        public string IssuerIban { get; set; } = "";
        public string IssuerNip { get; set; }

        //Recipient
        public string RecipientName { get; set; }
        public string RecipientStreet { get; set; }
        public string RecipientTown { get; set; }
        public string RecipientZipCode { get; set; }
        public string RecipientCountry { get; set; } = "";
        public string RecipientPhoneNumber { get; set; } = "";
        public string RecipientEmailAddress { get; set; }
        public string RecipientBankName { get; set; } = "";
        public string RecipientIban { get; set; } = "";
        public string RecipientNip { get; set; }

        //Invoice Items
        public InvoiceItemViewModel NewItem { get; set; } = new InvoiceItemViewModel();
        public ObservableCollection<InvoiceItemViewModel> InvoiceItems { get; set; } =
            new ObservableCollection<InvoiceItemViewModel>();
        #endregion


        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand LoadProductsCommand { get; }
        public ICommand LoadCustomersCommand { get; }

        private void OnProductsLoaded(IEnumerable<Product> products)
        {
            foreach (Product product in products)
            {
                ProductViewModel productViewModel = new ProductViewModel(product);
                _products.Add(productViewModel);
            }
        }

        private void OnCustomersLoaded(IEnumerable<Customer> customers)
        {
            foreach (Customer customer in customers)
            {
                CustomerViewModel customerViewModel = new CustomerViewModel(customer); 
                _customers.Add(customerViewModel);
            }
        }

        private void ShowCustomerList(bool show)
        {
            CustomerListVisibilty = show ? Visibility.Visible : Visibility.Hidden;
            NewInvoiceVisibilty = show ? Visibility.Hidden : Visibility.Visible;
        }

        public void OnShowRecipientList()
        {
            IsIssuer = false;
            ShowCustomerList(true);
        }

        public void OnShowIssuerList()
        {
            IsIssuer = true;
            ShowCustomerList(true);
        }

        public void OnHideCustomerList()
        {
            SearchedCustomerName = "";
            ShowCustomerList(false);
        }

        private void ShowProductList(bool show)
        {
            ProductListVisibilty = show ? Visibility.Visible : Visibility.Hidden;
            NewInvoiceVisibilty = show ? Visibility.Hidden : Visibility.Visible;
        }

        public void OnShowProductList()
        {
            ShowProductList(true);
        }

        public void OnHideProductList()
        {
            SearchedProductName = "";
            ShowProductList(false);
        }

        public void OnAddRecipientFromList()
        {
            if (IsIssuer == false)
            {
                RecipientName = SelectedCustomer.Name;
                RecipientTown = SelectedCustomer.Town;
                RecipientZipCode = SelectedCustomer.ZipCode;
                RecipientStreet = SelectedCustomer.Street;
                RecipientIban = SelectedCustomer.Iban;
                RecipientEmailAddress = SelectedCustomer.EmailAddress;
                RecipientNip = SelectedCustomer.Nip;
                RecipientPhoneNumber = SelectedCustomer.PhoneNumber;
                RecipientCountry = SelectedCustomer.Country;
                RecipientBankName = SelectedCustomer.BankName;
            }
            else if (IsIssuer == true)
            {
                IssuerName = SelectedCustomer.Name;
                IssuerTown = SelectedCustomer.Town;
                IssuerZipCode = SelectedCustomer.ZipCode;
                IssuerStreet = SelectedCustomer.Street;
                IssuerIban = SelectedCustomer.Iban;
                IssuerEmailAddress = SelectedCustomer.EmailAddress;
                IssuerNip = SelectedCustomer.Nip;
                IssuerPhoneNumber = SelectedCustomer.PhoneNumber;
                IssuerCountry = SelectedCustomer.Country;
                IssuerBankName = SelectedCustomer.BankName;
            }

            ShowCustomerList(false);
        }

        public void OnSelectProductFromList()
        {
            NewItem = new InvoiceItemViewModel()
            {
                Name = SelectedProduct.Name,
                VatPercent = SelectedProduct.VatPercent,
                Unit = SelectedProduct.Unit,
                NetPrice = SelectedProduct.NetPrice,
                Comment = SelectedProduct.Comment
            };

            ShowProductList(false);
        }

        public void OnAddItemToInvoiceItems()
        {
            NewItem.Comment ??= "";

            if (string.IsNullOrEmpty(NewItem.Name) || string.IsNullOrEmpty(NewItem.VatPercent.ToString(CultureInfo.InvariantCulture))
                                                   || string.IsNullOrEmpty(NewItem.Quantity.ToString(CultureInfo.InvariantCulture))
                                                   || string.IsNullOrEmpty(NewItem.NetPrice.ToString(CultureInfo.InvariantCulture))
                                                   || string.IsNullOrEmpty(NewItem.GrossPrice.ToString(CultureInfo.InvariantCulture)))
            {
                NewProductNullArgumentsErrorVisibility = true;
            }
            else
            {
                NewProductNullArgumentsErrorVisibility = false;
                InvoiceItemViewModel item = new InvoiceItemViewModel(NewItem.Name, NewItem.VatPercent,
                    NewItem.Unit, NewItem.Quantity, NewItem.NetPrice, "");

                InvoiceItems.Add(item);
                TotalPrice += NewItem.GrossPrice;
                ClearNewProduct();
            }
        }

        private void ClearNewProduct()
        {
            NewItem.Name = "";
            NewItem.VatPercent = 0;
            NewItem.Unit = Unit.Gram.ToString();
            NewItem.NetPrice = 0;
            NewItem.Comment = "";
            NewItem.Quantity = 0;
        }
    }
}
