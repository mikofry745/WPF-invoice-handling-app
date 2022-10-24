using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ObsługaFaktur.DbContext;
using ObsługaFaktur.Models;
using ObsługaFaktur.Services.ConflictValidators.ProductConflictValidators;
using ObsługaFaktur.Services.Navigation;
using ObsługaFaktur.Services.Navigation.Creators.ProductCreators;
using ObsługaFaktur.Services.Navigation.Providers.ProductProviders;
using ObsługaFaktur.Stores;
using ObsługaFaktur.ViewModels;
using System;
using System.Windows;
using ObsługaFaktur.Services.ConflictValidators.CustomerConflictValidators;
using ObsługaFaktur.Services.ConflictValidators.InvoiceConflictValidators;
using ObsługaFaktur.Services.Creators.CustomerCreators;
using ObsługaFaktur.Services.Creators.InvoiceCreators;
using ObsługaFaktur.Services.Providers.CustomerProviders;
using ObsługaFaktur.Services.Providers.InvoiceProviders;

namespace ObsługaFaktur
{
    public partial class App : Application
    {
        private const string CONNECTION_STRING = "Data Source=ObslugaFaktur.db";

        private readonly Business _business;
        private readonly IServiceProvider _serviceProvider;
        private readonly ObslugaFakturDbContextFactory _obslugaFakturDbContextFactory;

        public App()
        {
            _obslugaFakturDbContextFactory = new ObslugaFakturDbContextFactory(CONNECTION_STRING);

            IProductProvider productProvider = new DatabaseProductProvider(_obslugaFakturDbContextFactory);
            IProductCreator productCreator = new DatabaseProductCreator(_obslugaFakturDbContextFactory);
            IProductConflictValidator productConflictValidator = new DatabaseProductConflictValidator(_obslugaFakturDbContextFactory);

            ICustomerCreator customerCreator = new DatabaseCustomerCreator(_obslugaFakturDbContextFactory);
            ICustomerConflictValidator customerConflictValidator = new DatabaseCustomerConflictValidator(_obslugaFakturDbContextFactory);
            ICustomerProvider customerProvider = new DatabaseCustomerProvider(_obslugaFakturDbContextFactory);

            IInvoiceProvider invoiceProvider = new DatabaseInvoiceProvider(_obslugaFakturDbContextFactory);
            IInvoiceConflictValidator invoiceConflictValidator = new DatabaseInvoiceConflictValidator(_obslugaFakturDbContextFactory);
            IInvoiceCreator invoiceCreator = new DatabaseInvoiceCreator(_obslugaFakturDbContextFactory);

            ProductList products = new ProductList(productProvider, productCreator, productConflictValidator);
            CustomerList customers = new CustomerList(customerCreator, customerProvider, customerConflictValidator);
            InvoiceList invoices = new InvoiceList(invoiceProvider, invoiceConflictValidator, invoiceCreator);

            _business = new Business("Mik", products, customers, invoices);

            IServiceCollection services = new ServiceCollection();

            //STORES
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<ModalNavigationStore>();
            services.AddSingleton<ProductStore>();
            services.AddSingleton<CustomerStore>();
            services.AddSingleton<InvoiceDataStore>();

            //SERVICES
            services.AddSingleton<CloseModalNavigationService>();
            services.AddSingleton<CompositeNavigationService>();
            //ustawianie pierwszej widoczne strony
            services.AddSingleton<INavigationService>(CreateInvoiceListingNavigationService);

            //VIEWMODELS

            //Listing Views
            services.AddTransient<ProductListingViewModel>(s => new ProductListingViewModel(
                CreateAddProductNavigationService(s), _business, 
                _serviceProvider.GetRequiredService<ProductStore>()));

            services.AddTransient<CustomerListingViewModel>(s => new CustomerListingViewModel(
                CreateAddCustomerNavigationService(s), _business,
                _serviceProvider.GetRequiredService<CustomerStore>()));

            services.AddTransient<InvoiceListingViewModel>(s => new InvoiceListingViewModel(
                CreateAddInvoiceNavigationService(s), _business));

            //Create Views
            services.AddTransient<CreateProductViewModel>(s => new CreateProductViewModel(
                CreateSubmitProductNavigationService(s), _business, _serviceProvider.GetRequiredService<ProductStore>()));

            services.AddTransient<CreateCustomerViewModel>(s => new CreateCustomerViewModel(
                CreateSubmitCustomerNavigationService(s), _business, _serviceProvider.GetRequiredService<CustomerStore>()));

            services.AddTransient<CreateInvoiceViewModel>(s => new CreateInvoiceViewModel(
                CreateInvoiceListingNavigationService(s), _business, _serviceProvider.GetRequiredService<InvoiceDataStore>()));

           
            //NAVBAR VIEWMODEL
            services.AddTransient<NavigationBarViewModel>(CreateNavigationBarViewModel);

            //MAIN WINDOW
            services.AddSingleton<MainWindowViewModel>();

            //VIEWS
            services.AddSingleton<MainWindow>(s => new MainWindow()
            {
                DataContext = s.GetRequiredService<MainWindowViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            using (ObslugaFakturDbContext dbContext = _obslugaFakturDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            INavigationService initialNavigationService = _serviceProvider.GetRequiredService<INavigationService>();
            initialNavigationService.Navigate();

            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private INavigationService CreateSubmitCustomerNavigationService(IServiceProvider serviceProvider)
        {
            return new CompositeNavigationService(
                serviceProvider.GetRequiredService<CloseModalNavigationService>(),
                CreateCustomerListingNavigationService(serviceProvider));
        }

        private INavigationService CreateProductListingNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<ProductListingViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<NavigationBarViewModel>,
                serviceProvider.GetRequiredService<ProductListingViewModel>);
        }

        private INavigationService CreateAddProductNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<CreateProductViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                serviceProvider.GetRequiredService<CreateProductViewModel>);
        }

        private INavigationService CreateSubmitProductNavigationService(IServiceProvider serviceProvider)
        {
            return new CompositeNavigationService(
                serviceProvider.GetRequiredService<CloseModalNavigationService>(),
                CreateProductListingNavigationService(serviceProvider));
        }

        private INavigationService CreateCustomerListingNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<CustomerListingViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<NavigationBarViewModel>,
                serviceProvider.GetRequiredService<CustomerListingViewModel>);
        }
        private INavigationService CreateAddCustomerNavigationService(IServiceProvider serviceProvider)
        {
            return new ModalNavigationService<CreateCustomerViewModel>(
                serviceProvider.GetRequiredService<ModalNavigationStore>(),
                serviceProvider.GetRequiredService<CreateCustomerViewModel>);
        }

        private INavigationService CreateInvoiceListingNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<InvoiceListingViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<NavigationBarViewModel>,
                serviceProvider.GetRequiredService<InvoiceListingViewModel>);
        }

        private INavigationService CreateAddInvoiceNavigationService(IServiceProvider serviceProvider)
        {
            return new LayoutNavigationService<CreateInvoiceViewModel>(
                serviceProvider.GetRequiredService<NavigationStore>(),
                serviceProvider.GetRequiredService<NavigationBarViewModel>,
                serviceProvider.GetRequiredService<CreateInvoiceViewModel>);
        }

        private NavigationBarViewModel CreateNavigationBarViewModel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewModel(
                CreateProductListingNavigationService(serviceProvider),
                CreateCustomerListingNavigationService(serviceProvider),
                CreateInvoiceListingNavigationService(serviceProvider));
        }
    }
}