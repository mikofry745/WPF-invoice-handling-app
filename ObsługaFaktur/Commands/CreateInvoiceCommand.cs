using ObsługaFaktur.Exceptions;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ObsługaFaktur.Commands
{
    public class CreateInvoiceCommand : AsyncCommandBase
    {
        private readonly CreateInvoiceViewModel _createInvoiceViewModel;
        private readonly Business _business;

        public CreateInvoiceCommand(CreateInvoiceViewModel createInvoiceViewModel, Business business,
            INavigationService navigationBackService)
        {
            _createInvoiceViewModel = createInvoiceViewModel;
            _business = business;

            NavigateBackCommand = new NavigateCommand(navigationBackService);

            _createInvoiceViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public ICommand NavigateBackCommand { get; }

        public override async Task ExecuteAsync(object? parameter)
        {
            Customer issuer = new Customer(_createInvoiceViewModel.IssuerName, _createInvoiceViewModel.IssuerStreet,
                _createInvoiceViewModel.IssuerTown, _createInvoiceViewModel.IssuerZipCode,
                _createInvoiceViewModel.IssuerCountry,
                _createInvoiceViewModel.IssuerPhoneNumber, _createInvoiceViewModel.IssuerEmailAddress,
                _createInvoiceViewModel.IssuerBankName,
                _createInvoiceViewModel.IssuerIban, _createInvoiceViewModel.IssuerNip);

            Customer recipient = new Customer(_createInvoiceViewModel.RecipientName,
                _createInvoiceViewModel.RecipientStreet,
                _createInvoiceViewModel.RecipientTown, _createInvoiceViewModel.RecipientZipCode,
                _createInvoiceViewModel.RecipientCountry,
                _createInvoiceViewModel.RecipientPhoneNumber, _createInvoiceViewModel.RecipientEmailAddress,
                _createInvoiceViewModel.RecipientBankName,
                _createInvoiceViewModel.RecipientIban, _createInvoiceViewModel.RecipientNip);

            List<InvoiceItem> items = _createInvoiceViewModel.InvoiceItems.Select(i => new InvoiceItem(i.Name,
                i.VatPercent, (Unit)Enum.Parse(typeof(Unit), i.Unit), i.Quantity, i.NetPrice, i.Comment)).ToList();

            Invoice invoice = new Invoice(_createInvoiceViewModel.Type, _createInvoiceViewModel.Number,
                _createInvoiceViewModel.DateOfIssue, _createInvoiceViewModel.PaymentTerm, issuer,
                recipient, items, _createInvoiceViewModel.PlaceOfIssue,
                _createInvoiceViewModel.NameOfTheIssuer,_createInvoiceViewModel.PaymentMethod, _createInvoiceViewModel.BankName, _createInvoiceViewModel.Iban, _createInvoiceViewModel.Remarks, _createInvoiceViewModel.TotalPrice);

            try
            {
                if (String.IsNullOrEmpty(invoice.Number) 
                    || String.IsNullOrEmpty(invoice.DateOfIssue.ToString()) || String.IsNullOrEmpty(invoice.Type.ToString()) 
                    || String.IsNullOrEmpty(invoice.PaymentTerm.ToString()) || String.IsNullOrEmpty(invoice.NameOfTheIssuer)
                    || String.IsNullOrEmpty(invoice.PaymentMethod.ToString()) || String.IsNullOrEmpty(invoice.Iban) 
                    || String.IsNullOrEmpty(invoice.TotalPrice.ToString()) || String.IsNullOrEmpty(invoice.BankName)
                    || String.IsNullOrEmpty(invoice.Recipient.Nip) || String.IsNullOrEmpty(invoice.Recipient.EmailAddress)
                    || String.IsNullOrEmpty(invoice.Recipient.BankName) || String.IsNullOrEmpty(invoice.Recipient.Country)
                    || String.IsNullOrEmpty(invoice.Recipient.Iban) || String.IsNullOrEmpty(invoice.Recipient.Name)
                    || String.IsNullOrEmpty(invoice.Recipient.Town) || String.IsNullOrEmpty(invoice.Recipient.Street)
                    || String.IsNullOrEmpty(invoice.Recipient.PhoneNumber) || String.IsNullOrEmpty(invoice.Recipient.ZipCode)
                    || String.IsNullOrEmpty(invoice.Issuer.Nip) || String.IsNullOrEmpty(invoice.Issuer.EmailAddress)
                    || String.IsNullOrEmpty(invoice.Issuer.BankName) || String.IsNullOrEmpty(invoice.Issuer.Country)
                    || String.IsNullOrEmpty(invoice.Issuer.Iban) || String.IsNullOrEmpty(invoice.Issuer.Name)
                    || String.IsNullOrEmpty(invoice.Issuer.Town) || String.IsNullOrEmpty(invoice.Issuer.Street)
                    || String.IsNullOrEmpty(invoice.Issuer.PhoneNumber) || String.IsNullOrEmpty(invoice.Issuer.ZipCode))
                {
                    throw new ArgumentNullException();
                }

                if (!invoice.Recipient.PhoneNumber.All(char.IsDigit) || !invoice.Recipient.Nip.All(char.IsDigit)
                    || !invoice.Recipient.Iban.All(char.IsDigit) || !invoice.Issuer.PhoneNumber.All(char.IsDigit) 
                    || !invoice.Issuer.Nip.All(char.IsDigit) || !invoice.Issuer.Iban.All(char.IsDigit)
                    || !invoice.Number.All(char.IsDigit))
                {
                    throw new InvalidDataException();
                }

                if (String.IsNullOrEmpty(invoice.Remarks))
                {
                    invoice.Remarks = ""; 
                }

                await _business.AddInvoice(invoice);
                NavigateBackCommand.Execute(null);
            }
            catch (InvoiceConflictException)
            {
                MessageBox.Show("Faktura o tym numerze już istnieje", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Wypełnij wszystkie pola formularza", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Błędne dane.", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_createInvoiceViewModel.Number))
            {
                OnCanExecutedChanged();
            }
        }

      
    }
}
