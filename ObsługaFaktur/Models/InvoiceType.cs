using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsługaFaktur.Models
{
    public enum InvoiceType
    {
        VAT,
        VATPR,
        Korekta,
        Zaliczkowa,
        Uproszczona,
        ProForma,
    }
}
