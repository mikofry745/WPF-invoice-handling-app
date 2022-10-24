using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsługaFaktur.Dtos
{
    public class ProductDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double VatPercent { get; set; }
        public string Unit { get; set; }
        public decimal NetPrice { get; set; }
        public string Comment { get; set; }
    }
}
