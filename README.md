# Obsługa faktur

A WPF application in which we can quickly generate an invoice through a clear and easy-to-use form. It simplifies the work thanks to the fact that it contains lists of products and customers, whose elements previously created are reusable in the invoice form. In addition, the application allows you to generate an invoice in the form of a pdf file, which we can then send to the recipient of the invoice or print.

## How to launch
You will need .NET 6 and IDE(tested on visual studio 2022).

1. Clone this repository (master branch).
2. Open NuGet Package Manager Console.
3. Change the directory to the one where the project is located
```bash
cd ...\WPF-invoice-handling-app\ObsługaFaktur
```
4. Create a migration
```bash
dotnet ef migrations add InitialCreate
```
5. Update database
```bash
dotnet ef database update
```
6. Run the application

## NuGet Packages
1. Fody
2. EntityFrameworkCore(with sqlite support)
3. PdfSharp
4. Microsof.Xaml.Behaviors.Wpf
5. SimpleModal.WPF
6. MaterialDesignThemes