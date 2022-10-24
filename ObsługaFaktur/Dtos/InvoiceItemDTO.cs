using System;
using System.ComponentModel.DataAnnotations;

namespace ObsługaFaktur.Dtos
{
    public class InvoiceItemDTO
    {
        public int  Id { get; set; }
        public string Name { get; set; }
        public double VatPercent { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public decimal NetPrice { get; set; }
        public string Comment { get; set; }

        public int InvoiceDtoId { get; set; }
        public virtual InvoiceDTO InvoiceDto { get; set; }
    }
}
