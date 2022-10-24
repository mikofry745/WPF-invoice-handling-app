using System.Globalization;
using System.IO;
using ObsługaFaktur.ViewModels;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace ObsługaFaktur.Services
{
    public class PdfInvoiceSaver : IInvoiceSaver
    {
        private readonly string directory = "InvoicePdfs";

        public void Save(InvoiceViewModel invoiceViewModel)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont defaultFont = new XFont("Verdana", 9);
            XFont defaultMediumFont = new XFont("Verdana", 12, XFontStyle.Bold);
            XFont titleFont = new XFont("Arial", 20, XFontStyle.Bold);

            #region Basic Information
            gfx.DrawString($"Faktura {invoiceViewModel.Type} #{invoiceViewModel.Number}", titleFont, XBrushes.Black,
                new XRect(35, 35, page.Width, 40),
                XStringFormats.TopLeft);

            gfx.DrawString($"Data wystawienia: {invoiceViewModel.DateOfIssue.ToString("MM/dd/yyyy")}", defaultFont,
                XBrushes.Black, new XRect(35, 90, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"Miejsce wystawienia: {invoiceViewModel.PlaceOfIssue}", defaultFont,
                XBrushes.Black, new XRect(35, 110, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"Osoba wystawiająca: {invoiceViewModel.NameOfTheIssuer}", defaultFont,
                XBrushes.Black, new XRect(35, 130, page.Width, 20), XStringFormats.TopLeft);
            #endregion

            #region Issuer
            gfx.DrawString($"Sprzedawca", defaultMediumFont,
                XBrushes.Black, new XRect(35, 180, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawLine(XPens.Black, new XPoint(35, 205), new XPoint(250, 205));

            gfx.DrawString($"{invoiceViewModel.Issuer.Name}", defaultFont,
                XBrushes.Black, new XRect(35, 215, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"{invoiceViewModel.Issuer.Town} {invoiceViewModel.Issuer.ZipCode}", defaultFont,
                XBrushes.Black, new XRect(35, 235, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"{invoiceViewModel.PlaceOfIssue}", defaultFont,
                XBrushes.Black, new XRect(35, 255, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"{invoiceViewModel.Issuer.Iban}", defaultFont,
                XBrushes.Black, new XRect(35, 275, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"{invoiceViewModel.Issuer.EmailAddress}", defaultFont,
                XBrushes.Black, new XRect(35, 295, page.Width, 20), XStringFormats.TopLeft);
            #endregion

            #region Recipient
            gfx.DrawString($"Nabywca", defaultMediumFont,
                XBrushes.Black, new XRect(69, 180, page.Width, 20), XStringFormats.TopCenter);

            gfx.DrawLine(XPens.Black, new XPoint(335, 205), new XPoint(560, 205));

            gfx.DrawString($"{invoiceViewModel.Recipient.Name}", defaultFont,
                XBrushes.Black, new XRect(335, 215, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"{invoiceViewModel.Recipient.Town} {invoiceViewModel.Recipient.ZipCode}", defaultFont, XBrushes.Black, new XRect(335, 235, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"{invoiceViewModel.Recipient.Street}", defaultFont,
                XBrushes.Black, new XRect(335, 255, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"{invoiceViewModel.Recipient.Iban}", defaultFont,
                XBrushes.Black, new XRect(335, 275, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"{invoiceViewModel.Recipient.EmailAddress}", defaultFont,
                XBrushes.Black, new XRect(335, 295, page.Width, 20), XStringFormats.TopLeft);
            #endregion

            #region Product List
            gfx.DrawLine(XPens.Black, new XPoint(35, 362), new XPoint(560, 362));

            gfx.DrawString($"Nazwa", defaultFont,
                XBrushes.Black, new XRect(35, 370, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"Ilość", defaultFont,
                XBrushes.Black, new XRect(170, 370, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"Jednostka", defaultFont,
                XBrushes.Black, new XRect(230, 370, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"Vat", defaultFont,
                XBrushes.Black, new XRect(310, 370, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"Cena netto", defaultFont,
                XBrushes.Black, new XRect(370, 370, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"Cena brutto", defaultFont,
                XBrushes.Black, new XRect(460, 370, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawLine(XPens.Black, new XPoint(35, 388), new XPoint(560, 388));

            int yOffset = 0;

            foreach (var item in invoiceViewModel.Items)
            {
                yOffset += 20;

                gfx.DrawString(item.Name, defaultFont,
                    XBrushes.Black, new XRect(35, 375 + yOffset, page.Width, 20), XStringFormats.TopLeft);

                gfx.DrawString(item.Quantity.ToString(), defaultFont,
                    XBrushes.Black, new XRect(170, 375 + yOffset, page.Width, 20), XStringFormats.TopLeft);

                gfx.DrawString(item.Unit, defaultFont,
                    XBrushes.Black, new XRect(230, 375 + yOffset, page.Width, 20), XStringFormats.TopLeft);

                gfx.DrawString($"{item.VatPercent}%", defaultFont,
                    XBrushes.Black, new XRect(310, 375 + yOffset, page.Width, 20), XStringFormats.TopLeft);

                gfx.DrawString(item.NetPrice.ToString("C", CultureInfo.CurrentCulture), defaultFont,
                    XBrushes.Black, new XRect(370, 375 + yOffset, page.Width, 20), XStringFormats.TopLeft);

                gfx.DrawString(item.GrossPrice.ToString("C", CultureInfo.CurrentCulture), defaultFont,
                    XBrushes.Black, new XRect(460, 375 + yOffset, page.Width, 20), XStringFormats.TopLeft);

            }
            #endregion

            #region Payment Information
            yOffset += 40;

            gfx.DrawLine(XPens.Black, new XPoint(35, 385 + yOffset), new XPoint(250, 385 + yOffset));

            gfx.DrawString($"Termin płatności: {invoiceViewModel.PaymentTerm.ToString("dd.MM.yyyy")}", defaultFont,
                XBrushes.Black, new XRect(35, 395 + yOffset, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"Forma płatności: {invoiceViewModel.PaymentMethod}", defaultFont,
                XBrushes.Black, new XRect(35, 415 + yOffset, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"Nazwa banku: {invoiceViewModel.BankName}", defaultFont,
                XBrushes.Black, new XRect(35, 435 + yOffset, page.Width, 20), XStringFormats.TopLeft);

            gfx.DrawString($"IBAN: {invoiceViewModel.Iban}", defaultFont,
                XBrushes.Black, new XRect(35, 455 + yOffset, page.Width, 20), XStringFormats.TopLeft);



            gfx.DrawLine(XPens.Black, new XPoint(335, 385 + yOffset), new XPoint(560, 385 + yOffset));

            gfx.DrawString($"Do zapłaty: {invoiceViewModel.TotalPrice} zł", defaultMediumFont,
                XBrushes.Black, new XRect(335, 395 + yOffset, page.Width, 20), XStringFormats.TopLeft);
            #endregion

            #region Remarks
            gfx.DrawString($"{invoiceViewModel.Remarks}", defaultFont,
                XBrushes.Black, new XRect(35, 0, page.Width, page.Height - 35), XStringFormats.BottomLeft);
            #endregion
            
            //Choose the location where pdfs will be saved
            string solutionLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            //string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!Directory.Exists(Path.Combine(solutionLocation, directory)))
            {
                Directory.CreateDirectory(Path.Combine(solutionLocation, directory));
            }

            //if (!Directory.Exists(Path.Combine(executableLocation, directory)))
            //{
            //    Directory.CreateDirectory(Path.Combine(executableLocation, directory));
            //}

            string path = Path.Combine(solutionLocation, directory, "INV#" + invoiceViewModel.Number + ".pdf");
            //string path = Path.Combine(executableLocation, directory, "INV#" + invoiceViewModel.Number + ".pdf");

            document.Save(path);
        }
    }
}
